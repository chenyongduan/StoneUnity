using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public GameObject FlyEnemyAI;
  public GameObject SkyEnemyAI;
  public float FlyEnemyCreateTime;
  public float SkyEnemyCreateTime;
  public Transform startTransform;
  public Transform endTransform;

  private float curFlyCreateTime;
  private float curSkyCreateTime;
  private int skyEnemyCount = 0;
  private GameObject[] skyEnemyList = new GameObject[2];

  void Start()
  {
    curFlyCreateTime = FlyEnemyCreateTime;
    curSkyCreateTime = SkyEnemyCreateTime;
    createFlyEnemy();
    createSkyEnemy();
  }

  void Update()
  {
    curFlyCreateTime -= Time.deltaTime;
    if (curFlyCreateTime < 0)
    {
      createFlyEnemy();
      curFlyCreateTime = FlyEnemyCreateTime;
    }

    curSkyCreateTime -= Time.deltaTime;
    if (curSkyCreateTime < 0)
    {
      createSkyEnemy();
      curSkyCreateTime = FlyEnemyCreateTime;
    }
  }

  void createFlyEnemy()
  {
    Instantiate(FlyEnemyAI, transform.position, Quaternion.identity);
  }

  void createSkyEnemy()
  {
    if (skyEnemyCount < 2)
    {
      Vector2 startPos = startTransform.position;
      Vector2 endPos = endTransform.position;
      float midX = (endPos.x - startPos.x) / 2 + startPos.x;
      float x = skyEnemyCount == 0 ? Random.Range(startPos.x, midX - 0.5f) : Random.Range(midX, endPos.x);
      Debug.Log(skyEnemyCount);
      Debug.Log("x=" + x);
      Vector2 randPos = new Vector2(x, Random.Range(startPos.y, endPos.y));
      Instantiate(SkyEnemyAI, randPos, Quaternion.identity);
      skyEnemyCount += 1;
    }
  }

  public void TriggerEnemyHumanDeath()
  {
    skyEnemyCount = Mathf.Max(skyEnemyCount - 1, 0);
  }
}
