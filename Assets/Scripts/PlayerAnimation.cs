using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    public PlayerController controller;
    public Animator animator;

    public void Update() {
        animator.SetBool("Dashing", controller.IsDashing());
        animator.SetBool("Moving", controller.IsDashing() || controller.IsMoving());
        animator.SetBool("Pushing", controller.isPushing);
    }
}