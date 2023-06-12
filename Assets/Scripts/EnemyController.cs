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
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("EnemyHuman");
        if (gameObjects.Length < 2)
        {
            Vector2 startPos = startTransform.position;
            Vector2 endPos = endTransform.position;
            float midX = (endPos.x - startPos.x) / 2 + startPos.x;
            bool isLeft = true;
            foreach (GameObject item in gameObjects)
            {
                if (item.GetComponentInParent<Transform>().position.x > midX)
                {
                    isLeft = false;
                }
            }
            float x = !isLeft ? Random.Range(startPos.x, midX - 0.3f) : Random.Range(midX + 0.3f, endPos.x);
            Vector2 randPos = new Vector2(x, Random.Range(startPos.y, endPos.y));
            Instantiate(SkyEnemyAI, randPos, Quaternion.identity);
        }
    }
}
