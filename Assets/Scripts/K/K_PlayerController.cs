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


    //getset쓰는 이유는 함수끼리 변수를 주고 받기 편하게 하기 위함/
    
    [System.NonSerialized] public float xRotation = 0f;

    [System.NonSerialized] public bool isRunning  = false;
    [System.NonSerialized] public bool isJumping  = false;
    [System.NonSerialized] public bool isGrounded  = false;
    [System.NonSerialized] public Vector3 jumpVelocity;
    //입력값을 다른 클래스에서 사용 해야 하기 때문에
    [System.NonSerialized] public Vector3 inputVector = Vector3.zero;
    //방향또한 다른 클래스에서 사용 해야 하기 때문에
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
        
        //플레이 버튼을 눌렀을때 커서를 잠그는 기능
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
        //사용자의 입력에 받는다
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");
       
        //좌쉬프트 클릭시 달린다.
        isRunning = Input.GetKey(KeyCode.LeftShift);
        //입력에 따라 방향을 만든다. ClampMagnitude 복사된 벡터를 크기가 고정되어있는 maxLength로 리턴한다.
        if (inputVector.z != 0 || inputVector.x != 0)
        {
            
             moveDir = Vector3.ClampMagnitude(transform.right * inputVector.x + transform.forward * inputVector.z, 1.0f);
            //달릴때는 runSpeed로 이동한다.
           
            
                if (isRunning)
                {
                    _currentVelocity = inputVector * playerStates.runSpeed;
                    cc.Move(moveDir * playerStates.runSpeed * Time.deltaTime);
                }
                //그 외에는 걷는 속도로 이동한다. 
                else
                {
                    _currentVelocity = inputVector * playerStates.walkSpeed;
                    cc.Move(moveDir * playerStates.walkSpeed * Time.deltaTime);

                }
            float targetSpeed = isRunning ? playerStates.runSpeed : playerStates.walkSpeed;
             _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, inputVector.x * targetSpeed, AnimBlendSpeed*Time.fixedDeltaTime);
             _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, inputVector.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

        }
        //입력이 없으면
        else
        {
            moveDir = Vector3.zero;
            inputVector = Vector3.zero;
            
            
        }

        //_animator.SetFloat(_xVelHash, _currentVelocity.x);
        //_animator.SetFloat(_yVelHash, _currentVelocity.z);


    }

   
    void HandleJump()
    {   //바닥에 닿아있는지 확인하는 함수를 통해 isGrounded 값을 받는다.
        
        //space키를 누르면 점프를 시도한다.
        bool isTryingJump = Input.GetKeyDown(KeyCode.Space);
        
        //땅에 닿아있고 점프를 시도하면
        if (isTryingJump && isGrounded)
        {   
            //점프한다.
            isJumping = true;
        }

        else
        {   
            //아니면 점프안한다.
            isJumping = false;

        }
        //땅에 있고 점프파워가 0보다 작으면
        if (isGrounded && jumpVelocity.y < 0f)
        {   
            //점프파워를 -2로 만든다. 점프파워가 계속 감소하는 것을 막는다.
            jumpVelocity.y = -2f;
        }
        //점프하면
        if (isJumping)
        {   
            //점프파워를 설정한 점프높이에 유사하게 설정한다.
            jumpVelocity.y = Mathf.Sqrt(playerStates.jumpHeight * -2f * playerStates.gravity);

        }
        //점프파워는 중력을 계속더해서 점점 작아진다.
        
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
    {   //바닥에 닿은 여부에 따라 isGrounded를 조절한다.
        if (cc.collisionFlags == CollisionFlags.Below)
        {   
            //땅에 닿아있다.
            isGrounded = true;
        }
        else
        {   
            //땅에 닿아있지 않다.
            isGrounded = false;
        }
    }
}
