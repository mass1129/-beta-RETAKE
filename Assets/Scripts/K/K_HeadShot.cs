using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_HeadShot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject head;
    public ParticleSystem headShotEffect;
    

    public void HeadShot()
    {
        Instantiate(headShotEffect, transform.position, Quaternion.identity);
        
    }

}
