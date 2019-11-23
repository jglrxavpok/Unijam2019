using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Ce script contrôle l'ouverture d'une porte
 */
public class DoorController : Activable {
    public float deplacement = -1; //la distance après laquelle la porte est considérée ouverte, si négatif, la hauteur de la porte est est prise pour cette valeur 
    public float speed = 10f; //la vitesse de déplacement de la porte pendant son ouverture
    public bool invertDirection = false; //inverse la direction de déplacement de la porte pour son ouverture


    private bool _activated;
    private bool _move = false;
    private int _collisionCount = 0;
    private float _deplacementEffectif;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        if (deplacement < 0) {
            deplacement = transform.localScale.y;
        }
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
            //_rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}
