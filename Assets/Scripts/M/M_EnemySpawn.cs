using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrafabs; //적 
    public int randomPosition; //소환 범위
    public static M_EnemySpawn instance;

    private void Awake()
    {
        instance = this;
    }

    public void Spawn(int count)
    {
        //count만큼 생성
        //범위는 x: +- randomPosition/2
        //       y: 0
        //       z: +- randomPosition/2
        for (int i = 0; i < count; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-randomPosition / 2, randomPosition / 2), 0, Random.Range(-randomPosition / 2, randomPosition / 2));
            Instantiate(enemyPrafabs, randomSpawnPosition, Quaternion.identity);
        }
    }
}
