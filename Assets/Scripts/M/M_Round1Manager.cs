using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Round1Manager : MonoBehaviour
{
    int brokenMachine = 0; //�ν��� ��ġ ��
    public GameObject portal; //�� �̵� ��Ż
    public static M_Round1Manager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    
    void Update()
    {
        SetMission();
    }

    void SetMission()
    {
        string missionText = "��ġ�� �ı��ϼ���\n(" + brokenMachine + "/3)";
        GameManager.instance.MissionChange("Main", missionText);
    }

    void Round1Set()
    {
    }

    public void BreakMachine()
    {
        brokenMachine++;
        if (brokenMachine == 3)
        {
            portal.SetActive(true);
        }
    }
}
