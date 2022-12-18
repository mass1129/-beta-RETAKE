using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform Camera;
    [SerializeField] private Transform CameraRoot;

    [SerializeField] private float MouseSensitivity = 210;
    [SerializeField] private Transform etcRoot;
    [SerializeField] private float AnimBlendSpeed = 11f;
    

    public bool disabled = false;
    public GameObject avator;


    //getset���� ������ �Լ����� ������ �ְ� �ޱ� ���ϰ� �ϱ� ����/
    
    [System.NonSerialized] public float xRotation = 0f;

    [System.NonSerialized] public bool isRunning  = false;
    [System.NonSerialized] public bool isJumping  = false;
    [System.NonSerialized] public bool isGrounded  = false;
    [System.NonSerialized] public Vector3 jumpVelocity;
    //�Է°��� �ٸ� Ŭ�������� ��� �ؾ� �ϱ� ������
    [System.NonSerialized] public Vector3 inputVector = Vector3.zero;
    //������� �ٸ� Ŭ�������� ��� �ؾ� �ϱ� ������
    public Vector3 moveDir { get; private set; }


    [System.NonSerialized] public Animator _animator;
    private bool _hasAnimator;
    private int _xVelHash;
    private int _yVelHash;
    [System.NonSerialized] public Vector3 _currentVelocity;

    private K_JettAnimation jettAnimation;
    private CharacterController cc;
    private K_PlayerStates playerStates;
    private K_JettController jettController;

    private void Awake()
    {
        _hasAnimator = TryGetComponent<Animator>(out _animator);
        
    }
    void Start()
    {
        cc = GetComponent<CharacterController>();
        
        playerStates = GetComponent<K_PlayerStates>();
        
        //�÷��� ��ư�� �������� Ŀ���� ��״� ���
        Cursor.lockState = CursorLockMode.Locked;
        
        _xVelHash = Animator.StringToHash("X_Velocity");
        _yVelHash = Animator.StringToHash("Y_Velocity");
        jettController = GetComponent<K_JettController>();


    }

    // Update is called once per frame
    void Update()
    {

        CheckingGrounded();
        
            HandleMovement();
            HandleJump();
            
        
    }

    private void LateUpdate()
    {
      
            HandleMouseLook();
        
    }
    void HandleMovement()
    {
        if (!_hasAnimator) return;
        //������� �Է¿� �޴´�
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");
       
        //�½���Ʈ Ŭ���� �޸���.
        isRunning = Input.GetKey(KeyCode.LeftShift);
        //�Է¿� ���� ������ �����. ClampMagnitude ����� ���͸� ũ�Ⱑ �����Ǿ��ִ� maxLength�� �����Ѵ�.
        if (inputVector.z != 0 || inputVector.x != 0)
        {
            
             moveDir = Vector3.ClampMagnitude(transform.right * inputVector.x + transform.forward * inputVector.z, 1.0f);
            //�޸����� runSpeed�� �̵��Ѵ�.
           
            
                if (isRunning)
                {
                    _currentVelocity = inputVector * playerStates.runSpeed;
                    cc.Move(moveDir * playerStates.runSpeed * Time.deltaTime);
                }
                //�� �ܿ��� �ȴ� �ӵ��� �̵��Ѵ�. 
                else
                {
                    _currentVelocity = inputVector * playerStates.walkSpeed;
                    cc.Move(moveDir * playerStates.walkSpeed * Time.deltaTime);

                }
            float targetSpeed = isRunning ? playerStates.runSpeed : playerStates.walkSpeed;
             _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, inputVector.x * targetSpeed, AnimBlendSpeed*Time.fixedDeltaTime);
             _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, inputVector.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

        }
        //�Է��� ������
        else
        {
            moveDir = Vector3.zero;
            inputVector = Vector3.zero;
            
            
        }

        //_animator.SetFloat(_xVelHash, _currentVelocity.x);
        //_animator.SetFloat(_yVelHash, _currentVelocity.z);


    }

   
    void HandleJump()
    {   //�ٴڿ� ����ִ��� Ȯ���ϴ� �Լ��� ���� isGrounded ���� �޴´�.
        
        //spaceŰ�� ������ ������ �õ��Ѵ�.
        bool isTryingJump = Input.GetKeyDown(KeyCode.Space);
        
        //���� ����ְ� ������ �õ��ϸ�
        if (isTryingJump && isGrounded)
        {   
            //�����Ѵ�.
            isJumping = true;
        }

        else
        {   
            //�ƴϸ� �������Ѵ�.
            isJumping = false;

        }
        //���� �ְ� �����Ŀ��� 0���� ������
        if (isGrounded && jumpVelocity.y < 0f)
        {   
            //�����Ŀ��� -2�� �����. �����Ŀ��� ��� �����ϴ� ���� ���´�.
            jumpVelocity.y = -2f;
        }
        //�����ϸ�
        if (isJumping)
        {   
            //�����Ŀ��� ������ �������̿� �����ϰ� �����Ѵ�.
            jumpVelocity.y = Mathf.Sqrt(playerStates.jumpHeight * -2f * playerStates.gravity);

        }
        //�����Ŀ��� �߷��� ��Ӵ��ؼ� ���� �۾�����.
        
        jumpVelocity.y += playerStates.gravity * Time.deltaTime;

        cc.Move(jumpVelocity * Time.deltaTime);


    }
    [SerializeField] private Transform aimTarget;
    [SerializeField] private float aimDistance=1f;
    void HandleMouseLook()
    {   
        float mx = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        //[SerializeField] Camera playerCamera;
        //[SerializeField] private Transform CameraRoot;
        Camera.position = CameraRoot.position;
        
        xRotation -= my* MouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -70f, 50f);
        Camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        etcRoot.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up , mx* MouseSensitivity * Time.deltaTime);
        aimTarget.position = Camera.position + Camera.forward * aimDistance;
    }

    
    void CheckingGrounded()
    {   //�ٴڿ� ���� ���ο� ���� isGrounded�� �����Ѵ�.
        if (cc.collisionFlags == CollisionFlags.Below)
        {   
            //���� ����ִ�.
            isGrounded = true;
        }
        else
        {   
            //���� ������� �ʴ�.
            isGrounded = false;
        }
    }
}
