using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    public static M_BulletSpawner instance;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void BulletSpawn()
    {
        Vector3 bulletPosition = new Vector3(transform.position.x - 0.3f, transform.position.y + 0.3f, transform.position.z);
        Instantiate(bullet, bulletPosition, transform.rotation);
    }
}
