using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{

  public int health;
  public int score = 1;
  public GameObject bloodEffect;
  public GameObject floatPoint;

  private bool isDeath = false;

  // Update is called once per frame
  public void Update()
  {
    if (health <= 0 && !isDeath)
    {
      isDeath = true;
      Invoke("DestroyDelay", 0.5f);
    }
  }

  public void TakeDamage(int damage)
  {
    health -= damage;
    var gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
    gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
    Instantiate(bloodEffect, transform.position, Quaternion.identity);
    SoundManager.PlayPickCoinClip();
    GameController.cameraShake.Shake();
  }

  void DestroyDelay()
  {
    SetScore(score);
    TriggerEnemyHumanDeath();
    Destroy(gameObject.transform.parent.gameObject);
  }

  void SetScore(int score)
  {
    var gameController = GameObject.Find("GameController").GetComponent<GameController>();
    gameController.SetScore(score);
  }

  void TriggerEnemyHumanDeath()
  {
    var enemyController = GameObject.Find("EnemyController").GetComponent<EnemyController>();
    enemyController.TriggerEnemyHumanDeath();
  }
}
