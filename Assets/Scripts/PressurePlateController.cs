using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * Ce script est associé à une plaque de pression.
 * Les éléments activés par la plaque doivent être placés dans la liste 'activables'
 * Ils doivent hériter de la classe 'Activable' (à la place de 'MonoBehaviour')
 * @author: rene
 */
public abstract class Activable : MonoBehaviour {
    public abstract void Activate(); //Fonction appellée lorsque la plaque est activée

    public abstract void DeActivate(); //Fonction appellée quand la plaque se désactive
}
public class PressurePlateController : MonoBehaviour {
    public List<Activable> activables; //La liste des objets à activer
    public Material activeMaterial; //Le matériau de la plaque lorsqu'elle est activée (peut être null, dans ce cas la plaque ne change pas de matériau quand elle est activée)
    public float activeDuration = -1f; //Le temps en secondes durant lequel la plaque est activée. Si négatif, la plaque reste activée indéfiniment.

    private Material _unactiveMaterial;
    private MeshRenderer _meshRenderer;
    private bool _active = false;

    // Start is called before the first frame update
    void Start() {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _unactiveMaterial = _meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !_active) {
            _active = true;
            foreach (var activable in activables) {
                activable.Activate();
            }
            transform.Translate(0.05f * Vector3.down);
            if(activeMaterial)
                _meshRenderer.material = activeMaterial;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Player") && !_active) {
            _active = true;
            foreach (var activable in activables) {
                activable.Activate();
            }
            transform.Translate(0.05f * Vector3.down);
            if(activeMaterial)
                _meshRenderer.material = activeMaterial;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            if (activeDuration > 0) {
                StartCoroutine(ResetActive());
            }
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.collider.CompareTag("Player")) {
            if (activeDuration > 0) {
                StartCoroutine(ResetActive());
            }
        }
    }

    private IEnumerator ResetActive() {
        yield return new WaitForSeconds(activeDuration);
        if (_active) {
            foreach (var activable in activables) {
                activable.DeActivate();
            }
            _active = false;
            _meshRenderer.material = _unactiveMaterial;
            transform.Translate(0.05f * Vector3.up);
        }
    }
}
