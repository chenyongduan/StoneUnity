using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowThrow : MonoBehaviour
{
  public int damage;
  public float maxDistance;
  public float minSpeed;
  public float maxSpeed;
  public float minAngle;
  public float maxAngle;
  public float gravity = 1;
  public Transform trackPosition;
  public LineRenderer lineRenderer;
  public Vector3[] points = new Vector3[30];
  public float sliceDistance = 0.02f;

  private Transform playerTransform;
  private Rigidbody2D rb;
  private Vector2 startMousePos;
  private Vector2 currentMousePos;
  private Vector3 startPos;
  private float speed;
  private bool isShoot = false;
  private bool isDeath = false;

  void Start()
  {
    playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    rb = GetComponent<Rigidbody2D>();
    rb.gravityScale = gravity;
    startPos = transform.position;
    init();
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0) && !isShoot)
    {
      init();
    }

    if (Input.GetMouseButton(0) && !isShoot)
    {
      var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      // 鼠标移动，计算拖拽角度和力度，得出飞行角度和速度
      float distance = Mathf.Min(Vector2.Distance(mousePos, startMousePos), maxDistance);
      float minDistancePercent = 0.5f;
      float minValuePercent = 0.3f;
      float midDistance = maxDistance * minValuePercent;
      float midSpeed = minSpeed + (maxSpeed - minSpeed) * minValuePercent;
      if (distance / maxDistance < minDistancePercent)
      {
        speed = Map(distance, 0, midDistance, minSpeed, midSpeed);
      }
      else
      {
        speed = Map(distance, midDistance, maxDistance, midSpeed, maxSpeed);
      }
      var direction = (transform.position - mousePos).normalized;
      if (mousePos.x > transform.position.x)
      {
        direction = (mousePos - transform.position).normalized;
      }
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(angle, minAngle, maxAngle));
    }

    if (Input.GetMouseButtonUp(0) && !isShoot)
    {
      // 鼠标左键释放，发射飞镖
      rb.velocity = transform.right * Mathf.Abs(speed);
      rb.bodyType = RigidbodyType2D.Dynamic;
      isShoot = true;
    }

    if (isShoot)
    {
      lineRenderer.positionCount = 0;
      RotateArrowHead();
    }
    else
    {
      DrawTrack(Mathf.Abs(speed));
    }

    float throwDistance = (transform.position - startPos).sqrMagnitude;
    if (throwDistance > 300)
    {
      Destroy(gameObject);
    }
  }

  private void DrawTrack(float speed)
  {
    for (int i = 0; i < points.Length; i++)
    {
      points[i] = trackPosition.position + transform.right * speed * i * sliceDistance + Physics.gravity * gravity * (0.5f * (i * sliceDistance) * (i * sliceDistance));
    }
    lineRenderer.positionCount = points.Length;
    lineRenderer.SetPositions(points);
  }

  private void RotateArrowHead()
  {
    Vector3 velocity = rb.velocity;
    if (velocity != Vector3.zero)
    {
      Quaternion targetRotation = Quaternion.LookRotation(velocity, Vector3.up);
      rb.MoveRotation(targetRotation);
    }
  }

  void init()
  {
    startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    speed = minSpeed;
  }

  float Map(float value, float fromLow, float fromHigh, float toLow, float toHigh)
  {
    return (value - fromLow) / (fromHigh - fromLow) * (toHigh - toLow) + toLow;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (!isDeath)
    {
      if (other.gameObject.CompareTag("Enemy") && isShoot)
      {
        other.GetComponent<Enemy>().TakeDamage(damage);
      }
      else if (other.gameObject.CompareTag("EnemyHuman"))
      {
        other.GetComponent<Soldier>().TakeDamage(damage);
      }
      else if (other.gameObject.CompareTag("Platform"))
      {
        rb.velocity = new Vector2(rb.velocity.x / 2, -5.0f);
      }
      else if (other.gameObject.CompareTag("EnemyWeapon") && isShoot)
      {
        isDeath = true;
        rb.velocity = new Vector2(rb.velocity.x / 4, -5.0f);
        other.gameObject.GetComponent<EnemyArrow>().CollisionByWeapon();
      }
    }
  }
}
