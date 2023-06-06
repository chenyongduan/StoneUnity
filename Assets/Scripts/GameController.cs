using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
  public static CameraShake cameraShake;
  public GameObject scoreText;
  private int score = 0;

  public void SetScore(int curScore)
  {
    score += curScore;
    scoreText.GetComponent<Text>().text = score.ToString();
  }
}
