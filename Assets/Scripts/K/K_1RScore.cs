using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class K_1RScore : MonoBehaviour
{
    public static K_1RScore Instance;
    
    public List<AudioClip> scoreSoundPool = new List<AudioClip>();

    public AudioSource scoreAudioSource;

    int curScore = 0;

    public Text curScoreUI;

    public GameObject enemyFactory;


    private void Awake()
    {
        Instance = this;
    }
    bool isSoundPlay = false;

    public int CurScore
    {
        get
        {
            return curScore;
        }
        set
        {
            curScore = value;



            // curScoreUI.text = "Current Score : " + curScore;
            if (curScore > 0 && curScore < 5)
            {

                PlayAttackSound(curScore - 1);

                curScoreUI.text = "�Ϲ� ��弦���� �� óġ" + " : " + (curScore % 5) + " / 5";



            }

            else if (curScore == 5)
            {
                PlayAttackSound(4);
                curScoreUI.text = "�Ϲ� ��弦���� �� óġ" + " : " + "5 / 5" + "\r\n" +
                    "�ñر� ��弦���� �� óġ" + " : " + "0 / 5";




            }
            else if (curScore > 5 && curScore < 10)
            {
                PlayAttackSound(curScore - 6);
                curScoreUI.text = "�Ϲ� ��弦���� �� óġ" + " : " + "5 / 5" + "\r\n" +
                    "�ñر� ��弦���� �� óġ" + " : " + (curScore % 5) + "/ 5";




            }
            else if (curScore == 10)
            {
                PlayAttackSound(4);
                curScoreUI.text = "�Ϲ� ��弦���� �� óġ" + " : " + "5 / 5" + "\r\n" +
                    "�ñر� ��弦���� �� óġ" + " : " + "5 / 5";




            }
            else if (curScore > 10&& curScore < 50)
            {
                PlayAttackSound(curScore%5);
              
                curScoreUI.text = "�Ϲ� ��弦���� �� óġ" + " : " + "5/ 5" + "\r\n" +
                    "�ñر� ��弦���� �� óġ" + " : 5/ 5" + "\r\n" + "���� 50���� óġ : " + (curScore-10) +" ���� óġ";
               
            }    
            else if (curScore >= 50)
            {
                curScoreUI.text = "�Ϲ� ��弦���� �� óġ" + " : " + "5/ 5" + "\r\n" +
                    "�ñر� ��弦���� �� óġ" + " : 5/ 5" + "\r\n" + "���� 50���� óġ : " + (curScore - 10) + " ���� óġ"
                     + "\r\n" + "P Ű : Boss";

            }    

        }

    }
    public void GenerateEnemy ()
    {
        for(int i=0;i<5;i++)
        {
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = transform.GetChild(i).transform.position;
        }
    }

    void Start()
    {

        //curScoreUI.text = "Current Score : " + curScore;
        curScoreUI.text = "�Ϲ� ��弦���� �� óġ" + " : 0 / 5";
    }



    // Update is called once per frame
    void PlayAttackSound(int num)
    {
        scoreAudioSource.clip = scoreSoundPool[num];
        scoreAudioSource.Play();
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            GenerateEnemy();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("K_MapTest 3");
        if (Input.GetKeyUp(KeyCode.T))
        {
            SceneManager.LoadScene("jettCinemachine");
        //if (Input.GetKeyUp(KeyCode.P))
        //{
        //    SceneManager.LoadScene("jettCinemachine");
        //}
        }
}
