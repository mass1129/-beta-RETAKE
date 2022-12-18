using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class M_Missile : MonoBehaviour
{
    public float speed = 3; //속도
    public float power;
    GameObject target; //타겟
    NavMeshAgent navMissile;
    float time;
    public ParticleSystem bossBulletParticle;
    public GameObject bossBulletObject;
    Vector3 dir;
    private void Start()
    {
        target = GameObject.FindWithTag("Player");
        navMissile = GetComponent<NavMeshAgent>();
         dir = target.transform.position - transform.position;
        dir.Normalize();
    }
    private void Update()
    {
        if (!target)
            return;
        transform.LookAt(target.transform);
        time += Time.deltaTime;
        transform.position += dir * speed * Time.deltaTime;
        //navMissile.destination = target.transform.position;
        if (time > 4)
        {
            Destroy(gameObject);
            //3초 뒤 폭발할 때 파티클
            //Instantiate(bossBulletParticle, transform.position, transform.rotation);
        }
    }

    //IEnumerator IeDestroy() 
    //{
    //    //yield return new WaitForSeconds(0.5f);
    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter(Collider other)
    {
        bossBulletObject.SetActive(false);
        if (other.gameObject.tag == ("Player"))
            K_PlayerHealth.Instance.HP--;
        if (other.gameObject.tag == ("Bullet"))
            Destroy(other.gameObject);
        Instantiate(bossBulletParticle, transform.position, transform.rotation);
        //StartCoroutine(IeDestroy());
        Destroy(gameObject);
    }
}
