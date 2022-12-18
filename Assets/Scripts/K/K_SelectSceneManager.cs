using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class K_SelectSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    //private GameObject characterBox;
    
    public GameObject showFirstButton;
    public GameObject showScondButton;
    public GameObject[] textPool;
    public GameObject textObject;
    
    bool isSelected;
    private int index;

    void Start()
    {
        //characterPool = new GameObject[characterBox.transform.childCount];
        //for(int i=0;i< characterBox.transform.childCount; i++)
        //{
        //    characterPool[i] = characterBox.transform.GetChild(i).gameObject;
        //}
        textPool = new GameObject[5];
        for(int i = 0; i < textPool.Length; i++)
        {
            textPool[i] = textObject.transform.GetChild(i).gameObject;
            textPool[i].SetActive(false);
        }
        showFirstButton.SetActive(true);
        showScondButton.SetActive(false);
        //character.SetActive(false);



    }

    public void SelectCharacter1()
    {
    //    if (characterPool[1])
    //    {
    //        characterPool[1].SetActive(false);
    //    }

    //    characterPool[0].SetActive(true);
        showScondButton.SetActive(true);
        textPool[0].SetActive(true);
        //character.SetActive(true);
        if (!isSelected)
        {
            textPool[1].SetActive(true);
            isSelected = true;
        }

    }

    public void  ClickInfo()
    {
        
        for(int i=1;i<5;i++)
        {
            textPool[i].SetActive(false);
        }
        
        
         textPool[1].SetActive(true);
        
        
    }

    public void ClickQ()
    {
        
        for (int i = 1; i < 5; i++)
        {
            textPool[i].SetActive(false);
        }
        textPool[2].SetActive(true);
    }


    public void ClickE()
    {
        for (int i = 1; i < 5; i++)
        {
            textPool[i].SetActive(false);
        }
        textPool[3].SetActive(true);

    }

    public void ClickV()
    {
        for (int i = 1; i < 5; i++)
        {
            textPool[i].SetActive(false);
        }
        textPool[4].SetActive(true);

    }
    //public void Select2()
    //{
    //    if (characterPool[0])
    //    {
    //        characterPool[0].SetActive(false);
    //    }

    //    characterPool[1].SetActive(true);

    //}

    public void ConfirmClick()
    {
        if(showScondButton)
        {
            SceneManager.LoadScene("jettCinemachine");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
