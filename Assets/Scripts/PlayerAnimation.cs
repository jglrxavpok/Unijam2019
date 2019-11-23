using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    public PlayerController controller;
    public Animator animator;

    public void Update() {
        animator.SetBool("Dashing", controller.isDashing());
        animator.SetBool("Moving", controller.isDashing() || controller.isMoving());
        // TODO: animation de mort
    }
}