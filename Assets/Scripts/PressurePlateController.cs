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
    public abstract void Activate();

    public abstract void DeActivate();
}
public class PressurePlateController : MonoBehaviour {
    public List<Activable> activables;
    public float activeDuration = -1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Player")) {
            foreach (var activable in activables) {
                activable.Activate();
            }
            transform.Translate(0.1f * Vector3.down);
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
        transform.Translate(0.1f * Vector3.up);
        foreach (var activable in activables) {
            activable.DeActivate();
        }
    }
}
