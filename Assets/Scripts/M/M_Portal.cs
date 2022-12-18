using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Portal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        GameManager.instance.NextScene();
    }
}
