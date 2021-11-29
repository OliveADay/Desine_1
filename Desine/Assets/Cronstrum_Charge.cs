using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cronstrum_Charge : StateMachineBehaviour
{
    Rigidbody2D rb;
    float speed;
    public float startLength;
    float length;
    Cronstrum cronst_main;
    Vector3 lastScale;
    public float shrink;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        length = startLength;
        rb = animator.gameObject.GetComponent<Rigidbody2D>();
        cronst_main = animator.gameObject.GetComponent<Cronstrum>();
        lastScale = animator.gameObject.transform.localScale;
        cronst_main.charge = true;
        if(cronst_main != null)
        {
            animator.gameObject.transform.position = new Vector3(animator.gameObject.transform.position.x - shrink * 2, animator.gameObject.transform.position.y - shrink, animator.gameObject.transform.position.z);
            cronst_main.turn = false;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (cronst_main != null)
        {
            if (cronst_main.wallCheck)
            {
                animator.SetTrigger("charge_exit");
            }
        }
        if(length <= 0)
        {
            animator.SetTrigger("charge_exit");
        }
        else
        {
            length -= Time.deltaTime;
        }
        if(cronst_main != null)
        {
            if (cronst_main.wallCheck)
            {
                animator.SetTrigger("charge_exit");
            }
        }
        if(animator.gameObject.transform.localScale.x != lastScale.x)
        {
            animator.SetTrigger("charge_exit");
        }
        lastScale = animator.gameObject.transform.localScale;
    }
    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (cronst_main != null)
        {
            animator.gameObject.transform.position = new Vector3(animator.gameObject.transform.position.x + shrink, animator.gameObject.transform.position.y + shrink, animator.gameObject.transform.position.z);
            cronst_main.turn = true;
        }
        cronst_main.charge = false;
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
