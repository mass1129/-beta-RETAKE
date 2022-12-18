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
        //2D�� ���� ����̶� up�� ����߰� ������ 3D�� forward
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.Normalize();

        //ī�޶� ���� ������ ������ ����
        //'Camera'�� �±װ� MainCamera�� ��ü�� ������ �� ����
        dir = Camera.main.transform.TransformDirection(dir);

        //P = P0 + vt
        transform.position += dir * speed * Time.deltaTime;
    }

}
