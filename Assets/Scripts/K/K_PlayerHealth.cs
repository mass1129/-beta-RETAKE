using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public class K_PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public static K_PlayerHealth Instance;
    int hp;

    void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }
    public GameObject hpUI;
    Slider hpSlider;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            hpSlider.value = value;
            if(hp<=0)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {   
        hpSlider = hpUI.GetComponent<Slider>();
        hpSlider.maxValue = 100;
        HP = 100;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
