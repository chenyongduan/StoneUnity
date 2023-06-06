using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
  public GameObject failPage;

  public void showFailPage()
  {
    failPage.GetComponent<FailPage>().show();
  }
}
