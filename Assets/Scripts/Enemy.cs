using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public int health;
  public int damage;
  public float flashTime;
  public int score = 1;
  public GameObject bloodEffect;
  public GameObject floatPoint;
  private SpriteRenderer sr;
  private Color originalColor;

  // Start is called before the first frame update
  public void Start()
  {
    sr = GetComponent<SpriteRenderer>();
    originalColor = sr.color;
  }

  // Update is called once per frame
  public void Update()
  {
    if (health <= 0)
    {
      Invoke("DestroyDelay", flashTime);
    }
  }

  public void TakeDamage(int damage)
  {
    health -= damage;
    FlashColor(flashTime);
    var gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
    gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
    Instantiate(bloodEffect, transform.position, Quaternion.identity);
    SoundManager.PlayPickCoinClip();
    GameController.cameraShake.Shake();
    SetScore(score);
  }

  void FlashColor(float time)
  {
    sr.color = Color.red;
    Invoke("ResetColor", time);
  }

  void ResetColor()
  {
    sr.color = originalColor;
  }

  void DestroyDelay()
  {
    Destroy(gameObject);
  }

  void SetScore(int score)
  {
    var gameController = GameObject.Find("GameController").GetComponent<GameController>();
    gameController.SetScore(score);
  }
}
