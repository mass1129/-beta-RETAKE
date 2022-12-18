using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�� �Ӹ��� Collider �־ �Ѿ��� �Ӹ� �������� ����
public class M_HeadShot : MonoBehaviour
{
    //�Ӹ��� ������ �� �� �� �� ������ �ɷ� �Ǵ�
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
