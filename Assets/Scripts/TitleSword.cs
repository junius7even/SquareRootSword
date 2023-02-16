using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TitleSword : MonoBehaviour
{
    private Animator titleSwordAnimator;

    private readonly int pauseFinishedHash = Animator.StringToHash("PauseFinished");

    private readonly Stopwatch elapsedTime = new();
    void Start()
    {
        titleSwordAnimator = GetComponent<Animator>();
        titleSwordAnimator.SetBool(pauseFinishedHash, false);
        elapsedTime.Start();
    }
    void FixedUpdate()
    {
        // The loop has played once already and it's the flashing state
        if (titleSwordAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && titleSwordAnimator.GetCurrentAnimatorStateInfo(0).IsName("TitleRootSwordFlashing"))
        {
            // Transition nto the idle state and start the elapsed time
            titleSwordAnimator.SetBool(pauseFinishedHash, false);
            elapsedTime.Start();
        }
        else if(titleSwordAnimator.GetCurrentAnimatorStateInfo(0).IsName("TitleRootSwordIdle"))
        {
            if (elapsedTime.Elapsed.Seconds >= 2)
            {
                // Transition into the flashing state
                titleSwordAnimator.SetBool(pauseFinishedHash, true);
                elapsedTime.Reset();
            }
        }
    }
}
