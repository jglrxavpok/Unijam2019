using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMonsterTouch : MonoBehaviour {

    public Animator animator;

    bool animPlayed = false;

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Standing Death Forward 01"))
        {
            animPlayed = true;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Standing Death Forward 01") && animPlayed)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    
    public void OnTriggerEnter(Collider other) {


        if (other.CompareTag("Monster")) {
            Debug.LogWarning("GAME OVER");
            animator.SetTrigger("Death");

            animator.applyRootMotion = true;
            this.GetComponent<PlayerController>().enabled = false;

            Debug.Log(animPlayed);
        }
    }
}