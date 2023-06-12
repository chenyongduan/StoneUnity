using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public int speed;
    public int damage;
    public float gravity = 1;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private Vector3 startPos;
    private bool isDeath = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        startPos = transform.position;
        AutoAttack();
    }

    void AutoAttack()
    {
        var startPos = transform.position;
        var targetPos = playerTransform.position;
        // 鼠标移动，计算拖拽角度和力度，得出飞行角度和速度
        float distance = Vector2.Distance(targetPos, startPos);
        // Debug.Log(distance);
        var direction = (startPos - targetPos).normalized;
        if (targetPos.x > startPos.x)
        {
            direction = (targetPos - startPos).normalized;
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, -40);

        float speed = Random.Range(7.5f, 9f);
        rb.velocity = transform.right * -speed;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void Update()
    {
        RotateArrowHead();

        float throwDistance = (transform.position - startPos).sqrMagnitude;
        if (throwDistance > 300)
        {
            Destroy(gameObject);
        }
    }

    public void CollisionByWeapon()
    {
        isDeath = true;
        rb.velocity = new Vector2(rb.velocity.x / 4, -5.0f);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isDeath)
        {
            other.GetComponent<PlayerHeath>().TakeDamage(damage);
        }
    }
}
