using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class M_Enemy : MonoBehaviour
{
    public enum State
    {
        Idle, //����
        Move, //����
        Attack, //����
        Die //���
    }
    public NavMeshAgent navEnemy;
    public GameObject bullet;
    public State state; //����
    public GameObject target; //Ÿ��
    public int hp = 10; //�� ü��
    public float findDistance = 5; //���� �Ÿ�
    public GameObject effFactory;
    public GameObject firePosition;
    public Animator animator;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
        navEnemy = GetComponent<NavMeshAgent>();
        state = State.Idle;
        //navEnemy.enabled = true;
        //animator = transform.Find("EnemyModel").gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (state != State.Idle)
            transform.LookAt(target.transform);
        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if (state == State.Move)
        {
            UpdateMove();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
        }
    }

    private void UpdateIdle()
    {
        animator.SetTrigger("EnemyIdle");
        //�÷��̾�� �Ÿ��� ��������� 
        if (Vector3.Distance(transform.position, target.transform.position) <= findDistance)
        {
            state = State.Move;
        }
    }

    public float speed = 1;
    public float attackDistance = 1.5f;
    private void UpdateMove()
    {
        animator.SetTrigger("EnemyRun");
        navEnemy.isStopped = false;
        navEnemy.destination = target.transform.position;
        //print(navEnemy.destination);
        if (Vector3.Distance(transform.position, target.transform.position) < attackDistance)
        {
            navEnemy.isStopped = true;
            state = State.Attack;
        }
    }

    float currentTime;
    float attackTime = 1; 
    private void UpdateAttack()
    {
        animator.SetTrigger("EnemyIdle");
        currentTime += Time.deltaTime;
        if (currentTime > attackTime)
        {
            print("����");
            StartCoroutine(IeAttack());
            currentTime = 0;
            if (Vector3.Distance(transform.position, target.transform.position) > attackDistance)
            {
                navEnemy.isStopped = false;
                state = State.Move;
            }
        }
    }

    //ü���� i��ŭ ����
    public void AddDamage()
    {
        if (hp <= 0)
        {
            print("�ִϸ��̼�");
            GameObject dieEffect = Instantiate(effFactory);
            dieEffect.transform.position = transform.position;
            StartCoroutine("IeDie");
        }
        else
            hp -= 5;
    }

    //���� ���
    IEnumerator IeAttack()
    {
        print("�Ѿ� �߻�");
        //Vector3 startPosition = new Vector3(transform.position.x + 2, transform.position.y + 8, transform.position.z - 6);
        Instantiate(bullet, firePosition.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator IeDie()
    {
        hp = 1000;
        animator.SetTrigger("EnemyDie");
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.layer == 7)
            hp -= 5;*/
        if (hp <= 0)
        {
            GameObject dieEffect = Instantiate(effFactory);
            dieEffect.transform.position = transform.position;
            navEnemy.isStopped = true;
            StartCoroutine("IeDie");
        }
    }
}