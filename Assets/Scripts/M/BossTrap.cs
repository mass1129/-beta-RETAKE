using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTrap : MonoBehaviour
{
    public GameObject trap;
    public GameObject trapWarningImage;

    void Start()
    {
        trap.SetActive(false);
        trapWarningImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���� 1
    //��� ������ �� ���� ������ ����
    IEnumerator IePattern1()
    {
        trapWarningImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        trapWarningImage.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        trap.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        trap.SetActive(false);
        
    }

    public void BossPattern1()
    {
        //��� �̹��� 0.5�� �����ְ� �� �� 0.2�� �� Collider On
        StartCoroutine("IePattern1");
    }

    void OnTriggerStay (Collider other)
    {
        //Collider �ȿ� �÷��̾ ������ �÷��̾� ü�� ��
        if (other.name == "Player")
        {
            print("������ ����");
            M_Player.instance.hp -= 7;
        }
    }
}
