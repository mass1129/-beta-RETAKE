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

public class M_BossMove : MonoBehaviour
{
    NavMeshAgent navBoss;
    float time;
    int currentTime = 1;
    bool isIdle;
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

    //Attack1�Ӽ�
    public GameObject[] bossBulletPrefab = new GameObject[3]; //�Ѿ� ����
    public GameObject[] bossBulletPosition = new GameObject[3]; //�Ѿ� ��ȯ ��ġ
    public ParticleSystem[] bossBulletEffect = new ParticleSystem[3]; //�Ѿ� ��ȯ ����Ʈ

    void Start()
    {
        hp = maxHp; //���� ü���� �ִ� ü�°� ����
        bossHpImage.fillAmount = 1; // ����ü�¹� �̹��� 100%��
        target = GameObject.FindWithTag("Player"); //Ÿ���� �÷��̾� �±׸� �� ������Ʈ
        navBoss = GetComponent<NavMeshAgent>(); //�÷��̾� ������ �׺���̼�
        state = State.Move;
    }

    void Update()
    {
        //isIdle�� ���� ��� ��ɿ뵵
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
    }

    //�ȱ�
    private void UpdateMove()
    {
        //Ÿ�ٰ��� �Ÿ��� 5���� ũ�� ����
        if (Vector3.Distance(transform.position, target.transform.position) >= 5.0f)
        {
            navBoss.isStopped = false;
            navBoss.destination = target.transform.position;
        }
        else //5���� ������ ����

            navBoss.isStopped = true;
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
            if (currentTime % 5 == 0)
            {
                Pattern3();
                //EnemySpawn();
            }
            else if (currentTime % 3 == 0)
            {
                Pattern2();
                //M_BossAtack.instance.BossPattern1();
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
    }

    void Pattern1()
    {
        //0, 1, 2 ������ ���� ���� 3���� �Ӽ����� �Ѿ� ��ȯ
        int randomCount = Random.Range(0, 3);
        Instantiate(bossBulletPrefab[randomCount],
                    bossBulletPosition[randomCount].transform.position,
                    bossBulletPosition[randomCount].transform.rotation);
    }

    //���� ������
    void Pattern2()
    {
        StartCoroutine(IeIdle(1.5f));
    }

    //��ȯ
    public int randomSpawnRange = 20; //�ڴ� ��ȯ ����
    void Pattern3()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-randomSpawnRange / 2, randomSpawnRange / 2), 0, Random.Range(-randomSpawnRange / 2, randomSpawnRange / 2));
            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
        }
        StartCoroutine(IeIdle(1.5f));
    }

    //2������ ����
    private void UpdatePhaze2()
    {
        
    }

    //����
    private void UpdateDie()
    {
        Destroy(gameObject);
    }

    //�ǰ�
    public void AddDamage()
    {
        hp -= 1f;
        bossHpImage.fillAmount = hp / maxHp;
        if (hp <= 0)
            state = State.Die;
    }

    public void EnemySpawn()
    {
        for (int i = 0; i <= 5; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(0, 0, 0);
            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }
}
