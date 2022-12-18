using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class K_Enemy : MonoBehaviour
{
    public NavMeshAgent navEnemy;
    public GameObject bulletPrefab;
    public GameObject target; //???
    public float findDistance = 5; //???? ???
    public float stopDistance = 2;
    public Animator animator;
    float time = 0;
    public float coolTime = 1;

    public GameObject enemyCanvas;
    public float hp = 100; //?? ???
    public float curHp;
    public Image hpImage;
    public bool isDie = false;
    public Collider[] enemyCol;
    void Start()
    {
        curHp = hp;
        hpImage.fillAmount = 1;
        target = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (isDie)
            return;
        if (curHp <= 0)
        {
            isDie = true;
            StartCoroutine(IeDie());
        }

        enemyCanvas.transform.LookAt(target.transform.position);
        if (target)
        {
            transform.LookAt(target.transform.position);
        }
        time += Time.deltaTime;
        if (Vector3.Distance(transform.position, target.transform.position) <= stopDistance)
        {
            animator.SetTrigger("Idle");
            if (time > coolTime)
            {
                Attack();
                time = 0;
            }
        }
        else if (Vector3.Distance(transform.position, target.transform.position) <= findDistance)
        {
            Move();
            if (time > coolTime)
            {
                Attack();
                time = 0;
            }
        }
        else
        {

        }
    }

    IEnumerator IeDie()
    {   
      
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void Move()
    {
        animator.SetTrigger("Run");
        navEnemy.destination = target.transform.position;
    }

    void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        Vector3 bulletPosition = transform.position;
        bulletPosition.y += 1;
        bullet.transform.position = bulletPosition;
    }

    public void AddDamage(float amount)
    {

        curHp -= amount;
        hpImage.fillAmount = curHp / hp;
        print(curHp);
    }
}
