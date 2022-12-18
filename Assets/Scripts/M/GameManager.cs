using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text scoreText;
    //public Image bossHpImage;
    public GameObject gameOverPanel;
    public GameObject endPanel;
    public Image fade;
    public Camera mainCamera;
    int score;
    public GameObject endingRestartBtn;
    public Text endText;

    //�̼�
    //public GameObject mission;
    //public Text missionTitle;
    //public Text missionText;

    private void Awake()
    {
        //instance = this;
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);

        if (PlayerPrefs.HasKey("Character"))
        {
            //print(PlayerPrefs.GetInt("Character"));
        }

        //scoreText = transform.Find("ScoreText");
        /*if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainCamera.enabled = true;
            PlayerPrefs.SetInt("Score", 0);
        }*/

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            StartCoroutine("IeJettCinemachine");
        }

        //���� ü�¹� �� ���̰�
        gameOverPanel.SetActive(false);
        //bossHpImage.enabled = false;
        mainCamera.enabled = false;
        endingRestartBtn.SetActive(false);
        
    }


    //ó��������
    public void StartScene()
    {
        //SceneManager.LoadScene(1);
        //gameOverPanel.SetActive(false);
        //endPanel.SetActive(false);
    }

    public void EndingScene()
    {
        //StartCoroutine(IeEndingScene());
    }

    IEnumerator IeEndingScene()
    {
        float a = 0.0f;
        while (a < 1.0f)
        {
            a += 0.01f;
            yield return new WaitForSeconds(0.01f);
            fade.color = new Color(0, 0, 0, a);
            endText.color = new Color(1, 1, 1, a);
        }
        endingRestartBtn.SetActive(true);
        mainCamera.enabled = true;
        endPanel.SetActive(true);
        fade.color = new Color(0, 0, 0, 0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //���� �����
    public void SceneReload()
    {
       /* mainCamera.enabled = false;
        gameOverPanel.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //M_CharacterSpawner.instance.PlayerSpawn();
        score = PlayerPrefs.GetInt("RoundScore");
        scoreText.text = PlayerPrefs.GetInt("RoundScore").ToString();*/
    }

    //��Ʈ �ó׸ӽ� �̵�
    IEnumerator IeJettCinemachine()
    {
        yield return new WaitForSeconds(16.0f); //16��
        NextScene();
    }

    //���� ������ �̵�
    public void NextScene()
    {
        mainCamera.enabled = false;
        StartCoroutine(IeNextSceneIdle());
    }

    IEnumerator IeNextSceneIdle()
    {
        float a = 0.0f;
        while (a < 1.0f)
        {
            a += 0.01f;
            yield return new WaitForSeconds(0.01f);
            fade.color = new Color(0, 0, 0, a);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        while (a > 0.0f)
        {
            a -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            fade.color = new Color(0, 0, 0, a);
        }
        //yield return new WaitForSeconds(3.0f);
        
        PlayerPrefs.SetInt("RoundScore", int.Parse(scoreText.text));
    }

    
    /*public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }*/

    //���� ȹ��
    public void ScoreUp(int i)
    {
        /*//print("������");
        score += i;
        PlayerPrefs.SetInt("Score", score);
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();*/
    }

    
    private void Update()
    {
        /*if (SceneManager.GetActiveScene().buildIndex == 5)
            Destroy(gameObject);
            if (K_PlayerHealth.Instance)
        {
            gameOverPanel.SetActive(false);
            mainCamera.enabled = false;
        }*/
        /*if (PlayerPrefs.HasKey("Score"))
            scoreText.text = PlayerPrefs.GetInt("Score").ToString();*/
        

        PlayerHpCheck();
    }

    //�̼� ���� �ٲٱ�
    public void MissionChange(string mTitle, string mText)
    {
       /* missionTitle.text = mTitle;
        missionText.text = mText;*/
    }

    public void PlayerHpCheck()
    {
        /*if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainCamera.enabled = true;
            PlayerPrefs.SetInt("Score", 0);
        }

        if (!K_PlayerHealth.Instance) 
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {

            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {

            }
            else
            {
                GameOver();
            }
        }*/
    }

    public void GameOver()
    {
       /* mainCamera.enabled = true;
        gameOverPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;*/
    }
}
