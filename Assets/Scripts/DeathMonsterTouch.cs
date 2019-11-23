using System;
using UnityEngine;

public class DeathMonsterTouch : MonoBehaviour {
    
    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Monster")) {
            Debug.LogWarning("GAME OVER"); // TODO: changer de scène
        }
    }
}