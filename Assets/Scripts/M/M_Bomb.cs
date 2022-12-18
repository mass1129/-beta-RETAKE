using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_Bomb : MonoBehaviour
{
    public GameObject warningImage;
    public GameObject attackParticle;
    public GameObject bombObject;

    void Start()
    {
        StartCoroutine(IeDelay());
    }

    void Update()
    {
        
    }

    IEnumerator IeDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 warningPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        Instantiate(warningImage, warningPosition, Quaternion.identity);
    }

    IEnumerator IeDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        bombObject.SetActive(false);
        if (other.gameObject.tag == "Player")
        {
            K_PlayerHealth.Instance.HP--;
            Destroy(gameObject);
            Instantiate(attackParticle, transform.position, transform.rotation);
        }
        else if (other.gameObject.name == "Plane")
        {
            Destroy(gameObject);
            Instantiate(attackParticle, transform.position, transform.rotation);
        }
        else
            Destroy(gameObject);

        StartCoroutine(IeDestroy());
    }
}
