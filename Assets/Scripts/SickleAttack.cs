using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleAttack : MonoBehaviour
{
  public GameObject Sickle;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.K))
    {
      Instantiate(Sickle, transform.position, transform.rotation);
    }
  }
}
