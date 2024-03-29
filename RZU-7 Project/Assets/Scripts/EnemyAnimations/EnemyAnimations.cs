﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : ActorAnimations
{
    [SerializeField]
    protected Rigidbody2D rigidBody;

    void Update()
    {
        SetMovementValues(MovementConstants.ActorMovementStates.WALK);
    }

    public override void SetMovementBooleans(MovementConstants.ActorMovementStates movementState)
    {
        switch (movementState)
        {
            case MovementConstants.ActorMovementStates.WALK:
                animator.SetBool(AnimationConstants.isWalking, true);
                animator.SetBool(AnimationConstants.isRunning, false);
                break;
            case MovementConstants.ActorMovementStates.RUN:
                animator.SetBool(AnimationConstants.isWalking, false);
                animator.SetBool(AnimationConstants.isRunning, true);
                break;
            case MovementConstants.ActorMovementStates.NONE:
                animator.SetBool(AnimationConstants.isWalking, false);
                animator.SetBool(AnimationConstants.isRunning, false);
                break;
            default:
                break;
        }
    }

    public void SetMovementValues(MovementConstants.ActorMovementStates movementState)
    {
        SetMovementValues(rigidBody.velocity.x, rigidBody.velocity.y, movementState);
    }

    /// <summary>
    /// Sets the IsAttacking boolean in the animator
    /// </summary>
    /// <param name="val">The boolean value to set IsAttacking to</param>
    public void SetIsAttackingStatus(bool val)
    {
        SetActorStatus(AnimationConstants.isAttacking, val);
    }

    /// <summary>
    /// Sets the IsAttacking boolean in the animator
    /// </summary>
    /// <remarks>
    /// Sigh...This exists because AnimatorEvents do not support calling functions with bool parameters
    /// Also, there is some bug in Unity where the original overloaded name of this function caused the
    /// animation event logic to fail because it tries to use the bool version of this funcion. Hence the
    /// "Enum" at the end of the name.
    /// </remarks>
    /// <param name="val">The boolean value to set IsAttacking to</param>
    public void SetIsAttackingStatusEnum(Constants.ActorBoolState val)
    {
        SetActorStatus(AnimationConstants.isAttacking, val);
    }

    public override void CheckImobileConditions(ref float x, ref float y)
    {
        if (animator.GetBool(AnimationConstants.isDead) ||
            animator.GetBool(AnimationConstants.isInteracting) ||
            animator.GetBool(AnimationConstants.isAttacking))
        {
            x = y = 0f;
        }
    }

    public void SetImobileBools(AnimationConstants.ImobileStates state)
    {
        switch (state)
        {
            case AnimationConstants.ImobileStates.ATTACK:
                animator.SetBool(AnimationConstants.isDead, false);
                animator.SetBool(AnimationConstants.isInteracting, false);
                animator.SetBool(AnimationConstants.isAttacking, true);
                break;
            case AnimationConstants.ImobileStates.INTERACT:
                animator.SetBool(AnimationConstants.isDead, false);
                animator.SetBool(AnimationConstants.isInteracting, true);
                animator.SetBool(AnimationConstants.isAttacking, false);
                break;
            case AnimationConstants.ImobileStates.DEAD:
                animator.SetBool(AnimationConstants.isDead, true);
                animator.SetBool(AnimationConstants.isInteracting, false);
                animator.SetBool(AnimationConstants.isAttacking, false);
                break;
            case AnimationConstants.ImobileStates.NONE:
                animator.SetBool(AnimationConstants.isDead, false);
                animator.SetBool(AnimationConstants.isInteracting, false);
                animator.SetBool(AnimationConstants.isAttacking, false);
                break;
        }
    }
}
