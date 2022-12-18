using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Preferences : MonoBehaviour
{
    public GameObject preferenceImage; //ȯ�漳�� �̹���
    bool isPause = false; //������ ���� �������� üũ

    void Start()
    {
        preferenceImage.SetActive(false);
    }

    void Update()
    {
        //ESC�� ������ ������ �Ͻ������ǰ� ȯ�漳�� �̹����� ��
        //ECS�� �� �� �� ������ �Ͻ����� Ǯ���� ȯ�漳�� �̹��� ����
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
        //'������'��ư�� ���� �� �Լ�.
        //������ ����ȴ�
        Application.Quit();
    }
}
