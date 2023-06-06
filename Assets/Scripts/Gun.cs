using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
  public GameObject bullet;
  public Transform muzzleTransform;
  public Vector3 mousePos;
  public Vector2 gunDirection;
  public float minAngle;
  public float maxAngle;

  private Vector3 startMousePos;
  private Vector3 curMousePos;

  void Update()
  {
    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    if (Input.GetMouseButtonDown(0))
    {
      startMousePos = mousePos;
    }

    if (Input.GetMouseButton(0))
    {
      gunDirection = (mousePos - transform.position).normalized;
      float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
      transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(angle, minAngle, maxAngle));
    }

    if (Input.GetMouseButtonUp(0))
    {
      Instantiate(bullet, muzzleTransform.position, Quaternion.Euler(transform.eulerAngles));
    }
  }
}
