using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//기능
//대기
//플레이어 추적
//플레이어 공격
//공격1. 3가지 속성의 미사일. 쿨타임 2초
//공격2. 랜덤으로 떨어지는 광역기. 쿨타임 10초
//공격3. 잡몹 소환. 시작할 때 바로 한 번, 쿨타임 15초
//파티클 터지면서 죽기, 다음 씬으로

public class M_BossMove : MonoBehaviour
{
    NavMeshAgent navBoss;
    float time;
    int currentTime = 1;
    bool isIdle;
    public enum State
    {
        Idle, //정지
        Move, //추적
        Attack, //공격
        Phaze2, //2페 각성
        Die //사망
    }

    public State state; //상태
    GameObject target; //타겟
    public float hp; //보스 현재 체력
    public float maxHp = 50; //보스 전체 체력
    public Image bossHpImage; //체력바 이미지
    public GameObject enemyPrefab;

    //Attack1속성
    public GameObject[] bossBulletPrefab = new GameObject[3]; //총알 종류
    public GameObject[] bossBulletPosition = new GameObject[3]; //총알 소환 위치
    public ParticleSystem[] bossBulletEffect = new ParticleSystem[3]; //총알 소환 이펙트

    void Start()
    {
        hp = maxHp; //시작 체력은 최대 체력과 같음
        bossHpImage.fillAmount = 1; // 보스체력바 이미지 100%로
        target = GameObject.FindWithTag("Player"); //타겟은 플레이어 태그를 단 오브젝트
        navBoss = GetComponent<NavMeshAgent>(); //플레이어 추적용 네비게이션
        state = State.Move;
    }

    void Update()
    {
        //isIdle은 강제 대기 명령용도
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

    //상태
    //대기
    private void UpdateIdle()
    {
        navBoss.isStopped = true;
    }

    //걷기
    private void UpdateMove()
    {
        //타겟과의 거리가 5보다 크면 추적
        if (Vector3.Distance(transform.position, target.transform.position) >= 5.0f)
        {
            navBoss.isStopped = false;
            navBoss.destination = target.transform.position;
        }
        else //5보다 작으면 멈춤

            navBoss.isStopped = true;
        //대기하고 있는 것 아니면 항상 공격
        UpdateAttack();
    }

    //공격
    private void UpdateAttack()
    {
        //쿨타임 측정
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
        //0, 1, 2 랜덤한 수에 맞춰 3가지 속성으로 총알 소환
        int randomCount = Random.Range(0, 3);
        Instantiate(bossBulletPrefab[randomCount],
                    bossBulletPosition[randomCount].transform.position,
                    bossBulletPosition[randomCount].transform.rotation);
    }

    //랜덤 광역기
    void Pattern2()
    {
        StartCoroutine(IeIdle(1.5f));
    }

    //소환
    public int randomSpawnRange = 20; //핸덤 소환 범위
    void Pattern3()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-randomSpawnRange / 2, randomSpawnRange / 2), 0, Random.Range(-randomSpawnRange / 2, randomSpawnRange / 2));
            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
        }
        StartCoroutine(IeIdle(1.5f));
    }

    //2페이즈 각성
    private void UpdatePhaze2()
    {
        
    }

    //죽음
    private void UpdateDie()
    {
        Destroy(gameObject);
    }

    //피격
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
