using System;
using UnityEngine;

public class ElytraDoor : MonoBehaviour {
    private DoorController door;
    public Elytre leftElytra;
    public Elytre rightElytra;
    private bool activated;

    public void Start() {
        door = GetComponent<DoorController>();
    }

    public void Update() {
        if (leftElytra.instrumentPlaying && rightElytra.instrumentPlaying && !activated) {
            activated = true;
            leftElytra.LockElytra();
            rightElytra.LockElytra();
            door.Activate();
        }
    }
}