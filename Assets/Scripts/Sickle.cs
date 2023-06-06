using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{
  public int damage;
  public float speed;
  public float rotateSpeed;
  public float tuning;

  private Rigidbody2D rb;
  private Transform playerTransform;
  private Transform sickleTransform;
  private Vector2 startSpeed;


  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.velocity = transform.right * speed;
    startSpeed = rb.velocity;
    playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    sickleTransform = GetComponent<Transform>();
  }

  // Update is called once per frame
  void Update()
  {
    transform.Rotate(0, 0, rotateSpeed);
    var y = Mathf.Lerp(transform.position.y, playerTransform.position.y, tuning);
    transform.position = new Vector3(transform.position.x, y, 0);
    rb.velocity = rb.velocity - startSpeed * Time.deltaTime;

    if (Mathf.Abs(transform.position.x - playerTransform.position.x) < 0.5f)
    {
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Enemy"))
    {
      other.GetComponent<Enemy>().TakeDamage(damage);
    }
  }
}
