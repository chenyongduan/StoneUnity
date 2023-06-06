using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  public int damage;
  public float startTime;
  public float time;
  private Animator anim;
  private PolygonCollider2D myCollider;
  public GameObject Sickle;

  // Start is called before the first frame update
  void Start()
  {
    anim = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Animator>();
    myCollider = GetComponent<PolygonCollider2D>();
  }

  // Update is called once per frame
  void Update()
  {
    Attack();
  }

  void Attack()
  {
    if (Input.GetButtonDown("Attack"))
    {
      anim.SetTrigger("Attack");
      StartCoroutine(StartAttack());
    }
  }
  IEnumerator StartAttack()
  {
    yield return new WaitForSeconds(time);
    myCollider.enabled = true;
    StartCoroutine(DisableHitBox());
  }

  IEnumerator DisableHitBox()
  {
    yield return new WaitForSeconds(time);
    myCollider.enabled = false;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Enemy"))
    {
      other.GetComponent<Enemy>().TakeDamage(damage);
    }
  }
}
