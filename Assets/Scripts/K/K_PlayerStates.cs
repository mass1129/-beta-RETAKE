using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_PlayerStates : MonoBehaviour
{
    // Start is called before the first frame update
    public float runSpeed { get; private set; } = 12f;
    public float walkSpeed { get; private set; } = 6f;
    public float gravity { get; private set; } = -21f;
    public float jumpHeight { get; private set; } = 3f;
    
}
