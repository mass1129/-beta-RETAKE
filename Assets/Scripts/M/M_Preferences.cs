using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Preferences : MonoBehaviour
{
    public GameObject preferenceImage; //환경설정 이미지
    bool isPause = false; //게임이 멈춘 상태인지 체크

    void Start()
    {
        preferenceImage.SetActive(false);
    }

    void Update()
    {
        //ESC를 누르면 게임이 일시정지되고 환경설정 이미지가 뜸
        //ECS를 한 번 더 누르면 일시정지 풀리고 환경설정 이미지 꺼짐
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Time.timeScale = 1;
                preferenceImage.SetActive(false);
                isPause = false;
            }
            else
            {
                Time.timeScale = 0;
                preferenceImage.SetActive(true);
                isPause = true;
            }
        }
    }

    public void ExitGame()
    {
        //'나가기'버튼에 연결 할 함수.
        //게임이 종료된다
        Application.Quit();
    }
}
