using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_CharacterSpawner : MonoBehaviour
{
    public GameObject[] player = new GameObject[2];
    public Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
    public static M_CharacterSpawner instance;

    private void Awake()
    {
        instance = this;
        PlayerSpawn();
    }

    public void PlayerSpawn()
    {
        Instantiate(player[PlayerPrefs.GetInt("Character") - 1], spawnPosition, Quaternion.identity);
        K_PlayerHealth.Instance.HP += 2;
    }

    void Start()
    {
        //Instantiate(player[PlayerPrefs.GetInt("Character") - 1], spawnPosition, Quaternion.identity);
    }
}
