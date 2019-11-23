using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : Activable {
    public float deplacement = 2f;
    public float speed = 10f;
    public bool invertDirection = false;
    
    
    private bool _activated;
    private bool _move = false;
    private int _collisionCount = 0;
    private float _deplacementEffectif;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (_activated && _move) {
            transform.Translate(speed * Time.deltaTime * (invertDirection?Vector3.down:Vector3.up));
            _deplacementEffectif += speed * Time.deltaTime;
        } else if (_move) {
            transform.Translate(speed * Time.deltaTime * (invertDirection?Vector3.up:Vector3.down));
            _deplacementEffectif -= speed * Time.deltaTime;
        }

        if (_deplacementEffectif >= deplacement || _deplacementEffectif <= 0) {
            _move = false;
        }
    }

    public override void Activate() {
        _activated = true;
        _move = true;
    }

    public override void DeActivate() {
        _activated = false;
        _move = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            ++_collisionCount;
        }

        if (_collisionCount >= 2) {
            _rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}
