using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FailPage : MonoBehaviour
{

  public void ResetGame()
  {
    hide();
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }

  public void show()
  {
    gameObject.SetActive(true);
    Time.timeScale = 0f;
  }

  public void hide()
  {
    gameObject.SetActive(false);
    Time.timeScale = 1.0f;
  }
}
