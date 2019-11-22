using System;
using UnityEngine;

public class Instrument : MonoBehaviour {

    public Elytre correspondingElytra;

    private int activeFrames;
    private bool active;
    public int maxActiveFrames = 300; // nombre de frames pendant lesquelles l'instrument doit rester actif après que le joueur n'y touche plus

    public void Update() {
        if (activeFrames > 0) {
            activeFrames--;
        }
        
        correspondingElytra.instrumentPlaying = active || activeFrames > 0;
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            active = true;
        }
    }

    public void OnTriggerExit(Collider other) {
        active = false;
        activeFrames = maxActiveFrames;
    }
}