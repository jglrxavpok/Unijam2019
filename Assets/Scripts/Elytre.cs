using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elytre : MonoBehaviour {
    
    public int directionMultiplier = 1; // 1 pour droite, -1 pour gauche
    public bool instrumentPlaying;

    // Start is called before the first frame update
    void Start() {
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(90f, 0f, instrumentPlaying ? 45f * directionMultiplier : 0f);
    }
}
