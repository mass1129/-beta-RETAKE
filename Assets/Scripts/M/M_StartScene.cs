using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_StartScene : MonoBehaviour
{
    //타이틀 관련
    public GameObject titlePanel;
    public Text startText;
    public Button startbutton;
    public Text titleText;

    //캐릭터 선택창
    public GameObject characterPanel;
    public Image character1Panel;
    public Image character2Panel;
    public Button gameStart;

    void Start()
    {
        StartCoroutine(IeFadeIn());
        gameStart.gameObject.SetActive(false);
    }

    IEnumerator IeFadeIn()
    {
        float a = 0.0f;
        while (a < 1.0f)
        {
            a += 0.01f;
            yield return new WaitForSeconds(0.01f);
            startText.color = new Color(0.24f, 0.47f, 0.6f, a);
            startbutton.image.color = new Color(0, 0, 0, a);
            titleText.color = new Color(1, 1, 1, a);

        }
    }

    //캐릭터 선택창 열기
    public void StartButtonClick()
    {
        titlePanel.SetActive(false);
        characterPanel.SetActive(true);
    }

    //캐릭터 저장, 스타트 버튼 활성화
    public void SelectCharacter(int i)
    {
        PlayerPrefs.SetInt("Character", i);
        //print(i);
        //print(PlayerPrefs.GetInt("Character"));
        gameStart.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }
}