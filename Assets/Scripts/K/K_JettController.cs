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


    //�뽬Ű�� ������ �� ����Ű�� �Է¹޾� �ش� �������� �뽬�ϰ� �ʹ�.
    //�� �� : �뽬 �� ��� �ð�,  ��Ÿ�� ��� �ð�. �뽬 �ϴ������� �Ǻ�, // �뽬 ��Ÿ��, �뽬 �ӵ�, �뽬 ���� �ð�,
    public bool isDashing { get; private set; } = false; //�뽬 �Ǻ�
    public bool isUpdrafting { get; private set; } = false;
    //public bool isUltimating { get; private set; } = false;


    private float dashAttempts; //�뽬 ��Ÿ�� ���ӽð�
    private float dashStartTime = 0f; //�뽬 ��� �ð�

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
        //���۽� �ٷ� ��� �����ϵ��� ����
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

    //�뽬
    private Animator animator;
    void HandleDash()
    {

        particleObject.transform.position = ParticleRoot.position;
        dashAttempts += Time.deltaTime;
        dashImg.fillAmount = dashAttempts/jettStates.maxDashAttempts;
        dashAttempts = Mathf.Clamp(dashAttempts, 0f, jettStates.maxDashAttempts);
        //EŰ�� ������ �뽬�ϱ⸦ �õ� �Ѵ�.
        bool isTryingDash = Input.GetKeyDown(KeyCode.E);
        //�뽬���� �ƴҶ� �뽬�� �õ��ϸ�
        if (isTryingDash && !isDashing)
        {
            //�뽬 ��Ÿ���� ������ �ʾҴٸ�
            if (dashAttempts >= jettStates.maxDashAttempts && pFire.comboPossible)
            {

                //�뽬�� �����Ѵ�.->isDashing = true;
                OnStartDash();
            }


        }
        //�뽬�� �ߴٸ� 
        if (isDashing)
        {
            //�뽬 ���ӽð����� �뽬�� �Ѵ�.
            //������ �뽬 �Ҷ� ������������ �뽬�ϰ�ʹ�.
            playerController.jumpVelocity.y = 0;
            //1. �뽬������ ����� �ð��� �뽬���ӽð����� ������
            if (Time.time - dashStartTime <= jettStates.dashDurationSeconds)
            {
                //2�뽬�� �Ѵ�.

                //2-1. ����Ű�� �ȴ����� �ִٸ�
                //if (playerController.moveDir.Equals(Vector3.zero))
               // {
                    //2-2�չ������� �뽬�Ѵ�.
                    
                    //cc.Move(transform.forward * jettStates.dashSpeed * Time.deltaTime);
                    //print("x");
               // }
                //2-3.Ư�� ��ư�� ������ �ִٸ�
               // else
               // {
                    //2-4 �ش�������� �뽬�Ѵ�.
                    
                    cc.Move(dashDir.normalized * jettStates.dashSpeed * Time.deltaTime);
                    
                //}
                

            }
            //3. �뽬 ������ ����ð��� �뽬 ���ӽð��� ������ �뽬�� �����Ѵ�
            else
            {
                OnEndDash();
            }
        }
    }







    //�뽬�� �����ϸ� '�뽬��'�̿��� �ϸ� �뽬���۽ð��� �����ϸ� ��Ÿ�� ī���͸� �����Ѵ�
    //�뽬���۽�
    void OnStartDash()
    {
        //'�뽬��'���� ����
        isDashing = true;

        //�뽬 ���۽ð��� ���� �ð����� �Ƚ�.
        dashStartTime = Time.time;

        //�뽬 ��Ÿ�� ī���� ����
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
    //�뽬 ����� "�뽬 ��"���� ����, �뽬 ���۽ð� 0���� �ʱ�ȭ
    void OnEndDash()
    {
        //�뽬�� ���� ����
        isDashing = false;
        //�뽬 ���۽ð� �ʱ�ȭ
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
        //��Ÿ��

        //��Ÿ���� ��� ���������� �ִ� ��Ÿ���� �Ѿ���ʵ���
        updraftAttempts += Time.deltaTime;
        updraftImg.fillAmount = updraftAttempts / jettStates.maxUpdraftAttempts;
        updraftAttempts = Mathf.Clamp(updraftAttempts, 0f, jettStates.maxUpdraftAttempts);
        

        //QŰ�� ������ ���� ��ų�� �õ��Ѵ�.(������ ������ �ٸ��� ���� �� ����־ ��밡��)
        bool isTryingToUpdraft = Input.GetKeyDown(KeyCode.Q);
        //���� ��ų ���� ������ ���� -> ������ �õ��ϰ� ��Ÿ�Ӹ� ���Ҵٸ�
        if (isTryingToUpdraft && updraftAttempts >= jettStates.maxUpdraftAttempts && pFire.comboPossible)
        {
            OnEndDash();
            //���۽� ���º��� ���� �Լ� ����
            OnStartUpdraft();
            //������ų ��ü�� ����
            Updraft();
            //��Ÿ�� ����
            
        }
        
        //���� ��ų ������ �ִ� ������ ����������(�����ð��� �ٵǾ�����)�����̹Ƿ�
        //��ų ������ ����ð��� �����ð��� �ʰ������� ��ų �����Ѵ�.
        if (Time.time - updraftStartTime > jettStates.updraftDurationSeconds)
        {
            
            OnEndUpdraft();
            
        }

       
    }

    //��ü�� ����
    void Updraft()
    {   //�����Ҷ� �����ϴ� Y���� ���� jumpVelocity�� ���� ����(����)�Ѵ�.
         playerController.jumpVelocity.y = Mathf.Sqrt(jettStates.updraftHeight * -2f * playerStates.gravity);
        
    }

    //������ų ���� ���� ����
    void OnStartUpdraft()
    {   
        //������ ������ų '�����'
        isUpdrafting = true;
        //�������� �ð� ����
        updraftStartTime = Time.time;
        
        updraftAttempts = 0;
        dashParticlePool[4].Play();
        animator.Play("UpDraft");
    }

    //���� ��ų ������ ��������
    void OnEndUpdraft()
    {   
        //���� ����� ������ų '��� ���ϴ� ��'
        isUpdrafting = false;
        updraftStartTime = 0;
        

    }

    
}
