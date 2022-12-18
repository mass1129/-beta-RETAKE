using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class K_JettController : MonoBehaviour
{
    // Start is called before the first frame update

    
    [SerializeField] private Transform ParticleRoot;
    [SerializeField] ParticleSystem[] dashParticlePool;
    [SerializeField] GameObject particleObject;


    private K_PlayerController playerController;
    private K_PlayerFire pFire;
    private K_PlayerStates playerStates;
    private CharacterController cc;
    private K_JettStates jettStates;


    //대쉬키를 누르면 한 방향키를 입력받아 해당 방향으로 대쉬하고 싶다.
    //필 속 : 대쉬 시 경과 시간,  쿨타임 경과 시간. 대쉬 하는중인지 판별, // 대쉬 쿨타임, 대쉬 속도, 대쉬 지속 시간,
    public bool isDashing { get; private set; } = false; //대쉬 판별
    public bool isUpdrafting { get; private set; } = false;
    //public bool isUltimating { get; private set; } = false;


    private float dashAttempts; //대쉬 쿨타임 지속시간
    private float dashStartTime = 0f; //대쉬 경과 시간

    private float updraftAttempts;
    private float updraftStartTime = 0.0f;

    //private float ultimateAttempts;
    //private float ultimateStartTime = 0.0f;

    public Image dashImg;
    public Image updraftImg;

    private Vector3 dashDir;


    void Start()
    {
        playerController = GetComponent<K_PlayerController>();
        playerStates = GetComponent<K_PlayerStates>();
        jettStates = GetComponent<K_JettStates>();
        pFire = GetComponent<K_PlayerFire>();
        cc = GetComponent<CharacterController>();
        //시작시 바로 사용 가능하도록 충전
        dashAttempts = jettStates.maxDashAttempts;
        updraftAttempts = jettStates.maxUpdraftAttempts;
        //ultimateAttempts = jettStates.maxUltimateAttempts;
        SetParticle();
        animator = playerController._animator;

    }

    void SetParticle()
    {
        dashParticlePool = new ParticleSystem[5];
        for (int i = 0; i < dashParticlePool.Length; i++)
        {
            dashParticlePool[i] = particleObject.transform.GetChild(i).GetComponent<ParticleSystem>();
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        HandleDash();
        HandleUpdraft();
        
    }

    //대쉬
    private Animator animator;
    void HandleDash()
    {

        particleObject.transform.position = ParticleRoot.position;
        dashAttempts += Time.deltaTime;
        dashImg.fillAmount = dashAttempts/jettStates.maxDashAttempts;
        dashAttempts = Mathf.Clamp(dashAttempts, 0f, jettStates.maxDashAttempts);
        //E키를 누르면 대쉬하기를 시도 한다.
        bool isTryingDash = Input.GetKeyDown(KeyCode.E);
        //대쉬중이 아닐때 대쉬를 시도하면
        if (isTryingDash && !isDashing)
        {
            //대쉬 쿨타임이 지나지 않았다면
            if (dashAttempts >= jettStates.maxDashAttempts && pFire.comboPossible)
            {

                //대쉬를 시작한다.->isDashing = true;
                OnStartDash();
            }


        }
        //대쉬를 했다면 
        if (isDashing)
        {
            //대쉬 지속시간동안 대쉬를 한다.
            //점프후 대쉬 할때 일직선상으로 대쉬하고싶다.
            playerController.jumpVelocity.y = 0;
            //1. 대쉬시작후 경과한 시간이 대쉬지속시간보다 작을때
            if (Time.time - dashStartTime <= jettStates.dashDurationSeconds)
            {
                //2대쉬를 한다.

                //2-1. 방향키를 안누르고 있다면
                //if (playerController.moveDir.Equals(Vector3.zero))
               // {
                    //2-2앞방향으로 대쉬한다.
                    
                    //cc.Move(transform.forward * jettStates.dashSpeed * Time.deltaTime);
                    //print("x");
               // }
                //2-3.특정 버튼을 누르고 있다면
               // else
               // {
                    //2-4 해당방향으로 대쉬한다.
                    
                    cc.Move(dashDir.normalized * jettStates.dashSpeed * Time.deltaTime);
                    
                //}
                

            }
            //3. 대쉬 시작후 경과시간이 대쉬 지속시간을 넘을때 대쉬를 종료한다
            else
            {
                OnEndDash();
            }
        }
    }







    //대쉬를 시작하면 '대쉬중'이여야 하며 대쉬시작시간을 선언하며 쿨타임 카운터를 시작한다
    //대쉬시작시
    void OnStartDash()
    {
        //'대쉬중'으로 변경
        isDashing = true;

        //대쉬 시작시간를 현재 시간으로 픽스.
        dashStartTime = Time.time;

        //대쉬 쿨타임 카운터 시작
        dashAttempts = 0;
        if (playerController.moveDir.Equals(Vector3.zero))
        {
            dashDir = transform.forward;
        }
        else
        {
            dashDir = playerController.moveDir;
        }
        
       
        PlayDashParticle();

    }
    //대쉬 종료시 "대쉬 중"상태 해제, 대쉬 시작시간 0으로 초기화
    void OnEndDash()
    {
        //대쉬중 상태 해제
        isDashing = false;
        //대쉬 시작시간 초기화
        dashStartTime = 0f;
        


    }

    void PlayDashParticle()
    {
        Vector3 inputVector = playerController.inputVector;

        if(inputVector.z >= 0&& Mathf.Abs(inputVector.x) <= inputVector.z)
        {
            dashParticlePool[0].Play();
            animator.Play("Dash");
            return;
        }
        if (inputVector.z < 0 && Mathf.Abs(inputVector.x) <= Mathf.Abs(inputVector.z))
        {
            dashParticlePool[1].Play();
            animator.Play("BackDash");
            return;
        }
        if (inputVector.x > 0)
        {
            dashParticlePool[2].Play();
            animator.Play("RightDash");
            return;
        }
        if (inputVector.x < 0)
        {
            dashParticlePool[3].Play();
            animator.Play("LeftDash");
            return;
        }
        dashParticlePool[0].Play();
    }

    
    void HandleUpdraft()
    {
        //쿨타임

        //쿨타임은 계속 증가하지만 최대 쿨타임을 넘어가지않도록
        updraftAttempts += Time.deltaTime;
        updraftImg.fillAmount = updraftAttempts / jettStates.maxUpdraftAttempts;
        updraftAttempts = Mathf.Clamp(updraftAttempts, 0f, jettStates.maxUpdraftAttempts);
        

        //Q키를 누르면 높점 스킬을 시도한다.(높점은 점프와 다르게 땅에 안 닿아있어도 사용가능)
        bool isTryingToUpdraft = Input.GetKeyDown(KeyCode.Q);
        //높점 스킬 시전 조건은 간단 -> 높점을 시도하고 쿨타임만 돌았다면
        if (isTryingToUpdraft && updraftAttempts >= jettStates.maxUpdraftAttempts && pFire.comboPossible)
        {
            OnEndDash();
            //시작시 상태변수 제어 함수 가동
            OnStartUpdraft();
            //높점스킬 구체적 구현
            Updraft();
            //쿨타임 적용
            
        }
        
        //높점 스킬 시전은 최대 지점에 도달했을때(시전시간이 다되었을때)까지이므로
        //스킬 시전후 경과시간이 시전시간을 초과했을때 스킬 종료한다.
        if (Time.time - updraftStartTime > jettStates.updraftDurationSeconds)
        {
            
            OnEndUpdraft();
            
        }

       
    }

    //구체적 구현
    void Updraft()
    {   //점프할때 관리하는 Y전용 벡터 jumpVelocity의 값을 변경(증가)한다.
         playerController.jumpVelocity.y = Mathf.Sqrt(jettStates.updraftHeight * -2f * playerStates.gravity);
        
    }

    //높점스킬 사용시 상태 제어
    void OnStartUpdraft()
    {   
        //시전시 높점스킬 '사용중'
        isUpdrafting = true;
        //시전시점 시간 저장
        updraftStartTime = Time.time;
        
        updraftAttempts = 0;
        dashParticlePool[4].Play();
        animator.Play("UpDraft");
    }

    //높점 스킬 끝날때 상태제어
    void OnEndUpdraft()
    {   
        //시전 종료시 높점스킬 '사용 안하는 중'
        isUpdrafting = false;
        updraftStartTime = 0;
        

    }

    
}
