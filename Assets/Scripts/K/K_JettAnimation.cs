using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_JettAnimation : MonoBehaviour
{
    [SerializeField] Animator jettAnimator;

    private const float defaultAnimationSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    } 

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IdleAnimation()
    {
        
        jettAnimator.Play("Idle");
    }

    public void WalkingAnimation()
    {
        
        jettAnimator.Play("Walking");
    }

    public void RuningAnimation()
    {

        jettAnimator.Play("Runnig");
    }

}
