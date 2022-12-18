using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BossPattern : MonoBehaviour
{
    float time;
    float currentTime = 1;
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (currentTime < time)
        {
            //print(currentTime);
            TimeCheck();
        }
    }

    void TimeCheck()
    {
        if (currentTime % 15 == 0)
        {
            //M_EnemySpawn.instance.Spawn(5);
        }
        else if (currentTime % 10 == 0)
        {
            //M_BossAtack.instance.BossPattern1();
        }
        else if (currentTime % 1 == 0)
        {
            //M_BulletSpawner.instance.BulletSpawn();
        }
        currentTime += 1;
    }
}
