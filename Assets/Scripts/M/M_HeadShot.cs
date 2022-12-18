using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//적 머리에 Collider 넣어서 총알이 머리 지나가면 실행
public class M_HeadShot : MonoBehaviour
{
    //머리에 맞으면 적 한 번 더 공격한 걸로 판단
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            gameObject.GetComponentInParent<M_Enemy>().hp -= 10;
        }
    }
}
