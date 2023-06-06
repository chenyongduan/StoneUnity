using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatAI : Enemy
{
  public float speed;
  private GameObject player;
  private float curSpeed;

  public new void Start()
  {
    base.Start();
    curSpeed = speed;
    player = GameObject.FindGameObjectWithTag("Player");
    transform.position = GetRandomPos();
  }

  public new void Update()
  {
    base.Update();
    curSpeed += 0.2f;
    if (player)
    {
      transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
  }

  Vector2 GetRandomPos()
  {
    Vector2 randPos = new Vector2(Random.Range(10, 12), Random.Range(-3, 3));
    return randPos;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      other.GetComponent<PlayerHeath>().TakeDamage(damage);
      Destroy(transform.gameObject);
    }
  }
}
