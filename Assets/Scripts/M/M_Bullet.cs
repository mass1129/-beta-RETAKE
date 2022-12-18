using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어를 향해 직진
//어딘가에 부딪히거나 4초 지나면 사라짐 
//총알에 플레이어가 맞으면 터지는 이펙트, 소리 나옴
public class M_Bullet : MonoBehaviour
{
    public float speed = 5; //속도
    Transform target; //타겟
    Vector3 dir;
    float time;
    public ParticleSystem attackParticle;
    public GameObject bulletObject;

    private void Start()
    {
        attackParticle.Play();
        target = GameObject.FindWithTag("Player").transform;
        dir = target.position - transform.position;
        dir.Normalize();
    }
    private void Update()
    {
        transform.LookAt(target.transform);
        time += Time.deltaTime;
        transform.position += dir * speed * Time.deltaTime;
        
        if (time > 4)
            Destroy(gameObject);
    }

    IEnumerator IeDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        bulletObject.SetActive(false);
        if (other.gameObject.tag == ("Player"))
            K_PlayerHealth.Instance.HP--;
        Instantiate(attackParticle, transform.position, transform.rotation);
        //attackParticle.transform.position = transform.position;
        
        StartCoroutine(IeDestroy());
    }
}