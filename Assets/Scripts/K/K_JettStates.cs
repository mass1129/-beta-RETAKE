using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_JettStates : MonoBehaviour
{
    // Start is called before the first frame update
    //�뽬�ӵ�
    [System.NonSerialized] public float dashSpeed = 40f;
    //�뽬���� �ð�
    [System.NonSerialized] public float dashDurationSeconds = 0.4f;
    //�뽬 ��Ÿ��.
    [System.NonSerialized] public int maxDashAttempts = 2;

    [System.NonSerialized] public int maxUpdraftAttempts = 2;
    [System.NonSerialized] public float updraftHeight = 6.0f;
    [System.NonSerialized] public float updraftDurationSeconds = 0.2f;

    [System.NonSerialized] public int maxUltimateAttempts = 5;
    [System.NonSerialized] public int ultimateDurationSeconds = 5;

}
