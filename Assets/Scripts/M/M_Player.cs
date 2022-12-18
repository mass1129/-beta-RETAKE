using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Player : MonoBehaviour
{
    public float speed = 5;
    public int hp = 10;
    public GameObject playerBullet;
    public static M_Player instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(playerBullet);
            playerBullet.transform.position = transform.position;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //2D일 때는 평면이라 up을 사용했고 지금은 3D라 forward
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.Normalize();

        //카메라가 보는 방향을 앞으로 설정
        //'Camera'는 태그가 MainCamera인 객체를 가져올 수 있음
        dir = Camera.main.transform.TransformDirection(dir);

        //P = P0 + vt
        transform.position += dir * speed * Time.deltaTime;
    }

}
