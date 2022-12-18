using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Machine : MonoBehaviour
{
    float hp = 20;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddDamage()
    {
        hp--;
        if (hp <= 0)
        {
            M_Round1Manager.instance.BreakMachine();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
            AddDamage();
    }

    
}
