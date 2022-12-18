using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerBullet : MonoBehaviour
{
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = transform.position - Vector3.forward;
        transform.position += dir * 1 * Time.deltaTime;
    }
}
