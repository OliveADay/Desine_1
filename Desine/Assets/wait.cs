using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wait : StateMachineBehaviour
{
    public float startTime;
    float startTime_main;
    float time;
    public string[] states_triggers;
    public string[] states_bools;
    Cronstrum cronst_m;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startTime_main = startTime;
        cronst_m = animator.gameObject.GetComponent<Cronstrum>();
        if (cronst_m != null)
        {
            if (cronst_m.secondPhase)
            {
                startTime_main = startTime / 4;
                Debug.Log("wait time is " + startTime_main.ToString());
            }
        }
        else
        {
            Debug.Log("cronst_m is null");
        }
        time = startTime_main;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(time <= 0)
        {
            int i = Random.Range(0, 10);
            if(i >= 0 && i <= 4)
            {
                animator.SetTrigger(states_triggers[0]);
            }
            else if(i > 4 && i < 9)
            {
                animator.SetTrigger(states_triggers[1]);
            }
            
            time = startTime_main;
        }
        else
        {
            time -= Time.deltaTime;
        }
        if(cronst_m != null)
        {
            if (cronst_m.secondPhase)
            {
                startTime_main = startTime / 4;
                Debug.Log("wait time is " + startTime_main.ToString());
            }
        }
        else
        {
            Debug.Log("cronst_m is null");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startTime_main = startTime;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
