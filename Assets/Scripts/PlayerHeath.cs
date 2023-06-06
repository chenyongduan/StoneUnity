using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeath : MonoBehaviour
{
  public int health;
  public GameObject bloodEffect;
  public GameObject floatPoint;

  // Start is called before the first frame update
  public void Start()
  {
  }

  // Update is called once per frame
  public void Update()
  {
    if (health <= 0)
    {
      Invoke("DestroyDelay", 1.0f);
    }
  }

  public void TakeDamage(int damage)
  {
    health -= damage;
    var gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
    gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
    Instantiate(bloodEffect, transform.position, Quaternion.identity, transform.parent);
    SoundManager.PlayThrowCoinClip();
    GameController.cameraShake.Shake();
  }

  void DestroyDelay()
  {
    GameObject canvas = GameObject.Find("Canvas");
    if (canvas)
    {
      canvas.GetComponent<Canvas>().showFailPage();
    }
    Destroy(gameObject);
  }
}
