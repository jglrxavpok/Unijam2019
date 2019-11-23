using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMonsterTouch : MonoBehaviour {
    
    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Monster")) {
            Debug.LogWarning("GAME OVER"); // TODO: changer de scène
            SceneManager.LoadScene("GameOver");
            // TODO: animation de mort
        }
    }
}