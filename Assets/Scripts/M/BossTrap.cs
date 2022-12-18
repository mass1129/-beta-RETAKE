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

    //패턴 1
    //경고 보여준 후 넓은 범위에 공격
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
        //경고 이미지 0.5초 보여주고 끈 후 0.2초 뒤 Collider On
        StartCoroutine("IePattern1");
    }

    void OnTriggerStay (Collider other)
    {
        //Collider 안에 플레이어가 있으면 플레이어 체력 닳
        if (other.name == "Player")
        {
            print("광역기 맞음");
            M_Player.instance.hp -= 7;
        }
    }
}
