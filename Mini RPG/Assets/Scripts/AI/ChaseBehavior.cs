using Assets.Scripts;
using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : StateMachineBehaviour
{
    private SimpleCharacterControl _controller;
    private AIParameterSetter aiParams;

    [Header("Debug")]
    [SerializeField]
    private Vector3 _remain;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this._controller = animator.GetComponentInParent<SimpleCharacterControl>();
        this.aiParams = animator.GetComponentInParent<AIParameterSetter>();
        LogHelper.Log(typeof(ChaseBehavior), "Entered Chase");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 targetPos = this.aiParams.Target.transform.position;
        this._remain = Utils.ObjectSide(this._controller.transform, targetPos);
        this._controller.MoveByInput(1, this._remain.x, false);        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LogHelper.Log(typeof(ChaseBehavior), "Exited Chase");
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
