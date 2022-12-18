using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾ ���� ����
//��򰡿� �ε����ų� 4�� ������ ����� 
//�Ѿ˿� �÷��̾ ������ ������ ����Ʈ, �Ҹ� ����
public class M_Bullet : MonoBehaviour
{
    public float speed = 5; //�ӵ�
    Transform target; //Ÿ��
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