using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAppearLast : MonoBehaviour
{

    public GameObject monster;
    public AudioSource source;
    public AudioClip clip;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stop"))
        {
            monster.SetActive(true);
            source.clip = clip;
            source.Play();
        }
    }
}
