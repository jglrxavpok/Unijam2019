using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * Ce script est associé à un objet permettant d'activer une action.
 * Les éléments activés par cet objet doivent être placés dans la liste 'activables',
 * ils doivent hériter de la classe 'Activable' (à la place de 'MonoBehaviour')
 * L'objet contrôllé peut également avoir sa fonctionnalité bloquée par un autre Activator
 * @author: rene
 */
public abstract class Activable : MonoBehaviour {
    public abstract void Activate(); //Fonction appellée lorsque la plaque est activée

    public abstract void DeActivate(); //Fonction appellée quand la plaque se désactive
}
public class ActivatorController : Activable {
    public AudioClip activateSound;
    public AudioClip deactivateSound;
    
    public List<Activable> activables; //La liste des objets à activer
    public Material activeMaterial; //Le matériau de la plaque lorsqu'elle est activée (peut être null, dans ce cas la plaque ne change pas de matériau quand elle est activée)
    public Material lockedMaterial;
    public bool locked = true;
    public float activeDuration = -1f; //Le temps en secondes durant lequel la plaque est activée. Si négatif, la plaque reste activée indéfiniment.

    private Material _unactiveMaterial;
    private MeshRenderer _meshRenderer;
    private AudioSource _source;
    private bool _active = false;
    public int _nbObjetsQuiMOntActive= 0;

    // Start is called before the first frame update
    void Start() {
        _source = gameObject.GetComponent<AudioSource>();
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _unactiveMaterial = _meshRenderer.material;
        if (locked && lockedMaterial) {
            _meshRenderer.material = lockedMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate() {
        locked = false;
        _meshRenderer.material = _unactiveMaterial;
    }

    public override void DeActivate() {
        locked = true;
        if (lockedMaterial) {
            _meshRenderer.material = lockedMaterial;
        }
        
        if (_active && activeDuration > 0) {
            StartCoroutine(ResetActive());
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") || other.CompareTag("Box")){
            ++_nbObjetsQuiMOntActive;
            Debug.Log("Touché par "+other.tag);
            if(_nbObjetsQuiMOntActive>1) return;
            if(_active || locked) return;
            _active = true;
            foreach (var activable in activables) {
                activable.Activate();
            }
            transform.Translate(transform.localScale.y/2 * Vector3.down);
            if(activeMaterial)
                _meshRenderer.material = activeMaterial;
            if (activateSound) {
                _source.PlayOneShot(activateSound, 0.5f);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")|| other.CompareTag("Box")) {
            --_nbObjetsQuiMOntActive;
            if (activeDuration > 0 && _nbObjetsQuiMOntActive == 0) {
                StartCoroutine(ResetActive());
            }
        }
    }

    private IEnumerator ResetActive() {
        yield return new WaitForSeconds(activeDuration);
        if (_active) {
            if (deactivateSound) {
                _source.PlayOneShot(deactivateSound);
            }

            foreach (var activable in activables) {
                activable.DeActivate();
            }
            _active = false;
            if (locked) {
                _meshRenderer.material = lockedMaterial;
            }
            else {
                _meshRenderer.material = _unactiveMaterial;
            }
            transform.Translate(transform.localScale.y/2 * Vector3.up);
        }
    }
}
