using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : Activable
{
    private int _collisionCount = 0;
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public override void Activate() {
        gameObject.SetActive(false);
    }

    public override void DeActivate() {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            ++_collisionCount;
        }

        if (_collisionCount >= 2)
        {
            gameObject.SetActive(false);
        }
    }
}
