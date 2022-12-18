using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BossBulletSpawner : MonoBehaviour
{
    public GameObject brokenSpawner;
    public bool isAbleAttack = false;

    public static M_BossBulletSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GetComponent<BoxCollider>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(isAbleAttack)
            GetComponent<BoxCollider>().enabled = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
            Die();
    }

    void Die()
    {
        M_Boss.instance.counter++;
        //print(M_Boss.instance.counter);
        Instantiate(brokenSpawner, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}
