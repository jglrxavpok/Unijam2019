using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elytre : Activable {
    
    public int directionMultiplier = 1; // 1 pour droite, -1 pour gauche
    public bool instrumentPlaying;

    private Vector3 basePosition;
    public float translationAmount = 2f;
    public float translationSpeed = 2f;

    private float translationEffected = 0f;
    private bool locked;
    
    // Start is called before the first frame update
    void Start() {
        basePosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        transform.position = basePosition;
        if (instrumentPlaying || locked) {
            translationEffected += Time.deltaTime * translationSpeed;
            if (translationEffected >= translationAmount) {
                translationEffected = translationAmount;
            }
        }
        else {
            translationEffected -= Time.deltaTime * translationSpeed;
            if (translationEffected < 0) {
                translationEffected = 0f;
            }
        }
        transform.Translate(translationEffected * directionMultiplier, 0f, 0f);
    }

    public override void Activate() {
        instrumentPlaying = true;
    }

    public override void DeActivate() {
        instrumentPlaying = false;
    }

    public void LockElytra() {
        locked = true;
    }
}
