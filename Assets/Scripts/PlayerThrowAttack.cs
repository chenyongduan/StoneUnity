using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowAttack : MonoBehaviour
{
  public float intervalTime;
  public GameObject sickleGameObject;
  public GameObject arrowGameObject;

  private float curIntervalTime;
  private bool canThrow = true;
  private bool useArrow = true;

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
    if (Input.GetMouseButtonDown(0) && canThrow)
    {
      // 鼠标左键按下，创建飞镖
      var throwObj = transform.parent.Find("ThrowTransform");
      if (throwObj)
      {
        ResetTime();
        canThrow = false;
        Instantiate(useArrow ? arrowGameObject : sickleGameObject, throwObj.transform.position, Quaternion.identity);
      }
    }
  }

  private void ResetTime()
  {
    curIntervalTime = intervalTime;
  }

  public void ChangeWeapon() {
    useArrow = !useArrow;
  }
}
