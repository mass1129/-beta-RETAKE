using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        top();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //*엔터**엔터***엔터****를 출력하고싶다.
    void top()
    {
        int i;
        int j;
        string x = "";

        for (i = 0; i < 4; i++)
        {   

            
            for (j = 0; j < i + 1; j++)
            {
                x += "*";
            }
            if(i<3)
            {
                x += "\n";
            }

        }
        print(x);
    }
    //s3*엔터s2**엔터s1***엔터s0**** 를 출력하고싶다.
    void Bottom()
    {
        int i;
        int j;
        int k;
        string x = ""; 

        for (i=0; i<4;i++)
        {
            for(j=0;j<3-i;j++)
            {
                x += " ";
            }

            for(k=0;k<i+1;k++)
            {
                x += "*";
            }
            if (i < 3)
            {
                x += "\n";
            }
        }
        
        print(x);
    }

    //sss*엔터ss*s*엔터s*s*s*엔터*s*s*s*를 출력하고싶다.
    void Middle()
    {
        int i;
        int j;
        int k;
        string x = "";

        for (i=0; i<4; i++)
        {
            for(j=0;j<3-i;j++)
            {
                x += " ";
            }
            for(k=0;k<i+1;k++)
            {
                x += "*";
                if(k<i)
                {
                    x += " ";
                }
            }
            if (i < 3)
            {
                x += "\n";
            }
        }
        print(x);
    }
}
