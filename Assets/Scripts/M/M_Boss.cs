using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//���
//���
//�÷��̾� ����
//�÷��̾� ����
//����1. 3���� �Ӽ��� �̻���. ��Ÿ�� 2��
//����2. �������� �������� ������. ��Ÿ�� 10��
//����3. ��� ��ȯ. ������ �� �ٷ� �� ��, ��Ÿ�� 15��
//��ƼŬ �����鼭 �ױ�, ���� ������

public class M_Boss : MonoBehaviour
{
    public NavMeshAgent navBoss;
    float time;
    int currentTime = 1;
    public int counter;
    bool isPhaze2 = false;
    public enum State
    {
        Idle, //����
        Move, //����
        Attack, //����
        Phaze2, //2�� ����
        Die //���
    }

    public State state; //����
    GameObject target; //Ÿ��
    public float hp; //���� ���� ü��
    public float maxHp = 50; //���� ��ü ü��
    public Image bossHpImage; //ü�¹� �̹���
    public GameObject enemyPrefab;
    public GameObject bombPrefab;

    public Animator animator;

    //Attack1�Ӽ�
    public GameObject[] bossBulletPrefab = new GameObject[3]; //�Ѿ� ����
    public GameObject[] bossBulletPosition = new GameObject[3]; //�Ѿ� ��ȯ ��ġ
    public ParticleSystem[] bossBulletEffect = new ParticleSystem[3]; //�Ѿ� ��ȯ ����Ʈ

    public ParticleSystem dieParticle;
    public static M_Boss instance;
    public GameObject diePosition;
    float destroyTime;
    bool isDie = false;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        dieParticle.Stop();
        hp = maxHp; //���� ü���� �ִ� ü�°� ����
        //bossHpImage = GameManager.instance.bossHpImage;
        bossHpImage.fillAmount = 1; // ����ü�¹� �̹��� 100%��
        target = GameObject.FindWithTag("Player"); //Ÿ���� �÷��̾� �±׸� �� ������Ʈ
        navBoss = GetComponent<NavMeshAgent>(); //�÷��̾� ������ �׺���̼�
        state = State.Move;
    }

    void Update()
    {
        if (isDie)
        {
            destroyTime += Time.deltaTime;
            if (destroyTime >= 5)
                Destroy(gameObject);
            return;
        }
        if (hp <= 1 && !isDie)
        {
            
            navBoss.isStopped = true;
            navBoss.velocity = Vector3.zero;
            isDie = true;
            animator.SetTrigger("BossIdle");
            dieParticle.Stop();
            dieParticle.Play();
            //Instantiate(dieParticle);
            //dieParticle.transform.position = diePosition.transform.position;
            GameManager.instance.EndingScene();
            //���������� �Ѿ��
            //print("�� �Ѿ");
            //GameManager.instance.GameOver();
        }

        isPhaze2 = false;

        if (!target)
            return;
        if (isPhaze2 && counter == 3)
            StartCoroutine(IeIdle(3.0f));

        //isIdle�� ���� ��� ���ɿ뵵
        else if (state == State.Idle)
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
        else if (state == State.Phaze2)
        {
            UpdatePhaze2();
        }
        else if (state == State.Die)
        {
            UpdateDie();
        }
    }

    //����
    //���
    private void UpdateIdle()
    {
        navBoss.isStopped = true;
        navBoss.velocity = Vector3.zero;
        animator.SetTrigger("BossIdle");
    }

    //�ȱ�
    private void UpdateMove()
    {
        
        //Ÿ�ٰ��� �Ÿ��� 5���� ũ�� ����
        if (Vector3.Distance(transform.position, target.transform.position) >= 5.0f)
        {
            animator.SetTrigger("BossWalk");
            navBoss.isStopped = false;
            navBoss.destination = target.transform.position;
        }
        else //5���� ������ ����
        {
            animator.SetTrigger("BossIdle");
            navBoss.velocity = Vector3.zero;
            navBoss.isStopped = true;
        }
        //����ϰ� �ִ� �� �ƴϸ� �׻� ����
        UpdateAttack();
    }

    //����
    private void UpdateAttack()
    {
        //��Ÿ�� ����
        time += Time.deltaTime;
        if (currentTime < time)
        {
            if (currentTime % 15 == 0)
            {
                Pattern3();
            }
            else if (currentTime % 8 == 0)
            {
                Pattern2();
            }
            else if (currentTime % 1 == 0)
            {
                Pattern1();
            }
            currentTime += 1;
        }
    }

    IEnumerator IeIdle(float idleTime)
    {
        state = State.Idle;
        yield return new WaitForSeconds(idleTime);
        state = State.Move;
        if (isPhaze2 && counter == 3)
        {
            transform.Find("NormalBulletSpawner").gameObject.SetActive(true);
            transform.Find("SpeedBulletSpawner").gameObject.SetActive(true);
            transform.Find("PowerBulletSpawner").gameObject.SetActive(true);
            transform.Find("BossShield").gameObject.SetActive(true);
            animator.SetBool("BossBlock", true);
            counter = 0;
        }
    }

    void Pattern1()
    {
        //0, 1, 2 ������ ���� ���� 3���� �Ӽ����� �Ѿ� ��ȯ
        int randomCount = Random.Range(0, 3);
        Instantiate(bossBulletPrefab[randomCount],
                    bossBulletPosition[randomCount].transform.position,
                    bossBulletPosition[randomCount].transform.rotation);
        bossBulletPosition[randomCount].transform.LookAt(target.transform);
    }

    //���� ������
    void Pattern2()
    {
        randomSpawnRange = 50;
        StartCoroutine(IeIdle(1.5f));
        for (int i = 0; i < 15; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-randomSpawnRange / 2, randomSpawnRange / 2), 40, Random.Range(-randomSpawnRange / 2, randomSpawnRange / 2));
            Instantiate(bombPrefab, randomSpawnPosition, Quaternion.identity);
        }
        StartCoroutine(IeIdle(1.5f));
    }

    //��ȯ
    public int randomSpawnRange = 20; //���� ��ȯ ����
    void Pattern3()
    {
        StartCoroutine(IeIdle(1.5f));
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-randomSpawnRange / 2, randomSpawnRange / 2), 0, Random.Range(-randomSpawnRange / 2, randomSpawnRange / 2));
            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }

    //2������ ����
    private void UpdatePhaze2()
    {
        if (isPhaze2)
        {
            
        }

    }

    //����
    private void UpdateDie()
    {
    }

    //�ǰ�
    public void AddDamage()  
    {
        hp -= 1f;
        bossHpImage.fillAmount = hp / maxHp;
        
        //2������ �ƴϸ� �׳� ������ ����
        /*if (!isPhaze2)
            hp -= 1f;
        else if (isPhaze2 && counter == 3)
        {
            transform.Find("BossShield").gameObject.SetActive(false);
            //2�������鼭 ī���Ͱ� 3�̸� ���� ����
            hp -= 1f;
        }

        bossHpImage.fillAmount = hp / maxHp;
        //���� ������ 2������
        //2��: 2������ ����, ���� �ٲ�
        //�ͷ� �� �� �� ���ָ� ���� ���ݰ���
        *//*if (hp / maxHp <= 0.3f)
        {
            *//*if(!isPhaze2)
                transform.Find("BossShield").gameObject.SetActive(true);*//*
            isPhaze2 = true;
            //GetComponent<Renderer>().material.color = Color.red;
            transform.Find("NormalBulletSpawner").gameObject.GetComponent<M_BossBulletSpawner>().isAbleAttack = true;
            transform.Find("SpeedBulletSpawner").gameObject.GetComponent<M_BossBulletSpawner>().isAbleAttack = true;
            transform.Find("PowerBulletSpawner").gameObject.GetComponent<M_BossBulletSpawner>().isAbleAttack = true;
        }*/

    }

    /*public void EnemySpawn()
    {
        for (int i = 0; i <= 5; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(0, 0, 0);
            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }*/
}
