using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_warningImage : MonoBehaviour
{
    public Image warningImage; //경고 이미지 

    void Start()
    {
        StartCoroutine(IeGradation());
    }

    void Update()
    {
        
    }

    //점점 빨개짐
    //
    IEnumerator IeGradation()
    {
        float a = 0.0f;
        while (a < 1.0f)
        {
            a += 0.005f;
            yield return new WaitForSeconds(0.01f);
            warningImage.color = new Color(1f, 0f, 0f, a);
        }
        Destroy(gameObject);
    }
}
