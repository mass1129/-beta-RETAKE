using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            K_PlayerHealth.Instance.HP -= 10;
    }
}
