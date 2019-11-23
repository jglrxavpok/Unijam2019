using System;
using UnityEngine;

public class ElytraDoor : MonoBehaviour {
    public Elytre leftElytra;
    public Elytre rightElytra;
    private bool open;

    private MeshRenderer mr;

    public void Start() {
        mr = gameObject.GetComponent<MeshRenderer>();
    }

    public void Update() {
        if (leftElytra.instrumentPlaying && rightElytra.instrumentPlaying) {
            open = true;
        } else {
            open = false;
        }
        
        // hide
        mr.enabled = !open;
    }
}