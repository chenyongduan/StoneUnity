using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public int damage;
  public float speed;
  public float arrowDistance;

  private Rigidbody2D rg2d;
  private Vector3 startPos;
  private bool isFall;

  // Use this for initialization
  void Start()
  {
    rg2d = GetComponent<Rigidbody2D>();
    rg2d.velocity = transform.right * speed;
    startPos = transform.position;
  }

  void Update()
  {
    float distance = (transform.position - startPos).sqrMagnitude;
    if (distance > arrowDistance)
    {
      Destroy(gameObject);
    }

    if (isFall)
    {
      float fallSpeed = 5f;
      // 计算物体下一帧的位置
      Vector3 nextPosition = transform.position + Vector3.down * fallSpeed * Time.deltaTime;
      // 更新物体的位置属性
      transform.position = nextPosition;
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Enemy"))
    {
      //   Destroy(gameObject);
      rg2d.velocity = new Vector2(0, 0);
      isFall = true;
      other.GetComponent<Enemy>().TakeDamage(damage);
    }
  }
}
