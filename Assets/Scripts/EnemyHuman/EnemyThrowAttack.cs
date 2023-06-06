using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowAttack : MonoBehaviour
{
  public float intervalTime;
  public GameObject arrowGameObject;

  private bool canThrow;
  private float curIntervalTime;

  void Start()
  {
    ResetTime();
  }

  void Update()
  {
    if (!canThrow)
    {
      curIntervalTime -= Time.deltaTime;
    }
    canThrow = curIntervalTime < 0;
    if (canThrow)
    {
      var throwObj = transform.parent.Find("ThrowTransform");
      if (throwObj)
      {
        ResetTime();
        canThrow = false;
        Instantiate(arrowGameObject, throwObj.transform.position, Quaternion.identity);
      }
    }
  }

  private void ResetTime()
  {
    curIntervalTime = intervalTime;
  }
}
