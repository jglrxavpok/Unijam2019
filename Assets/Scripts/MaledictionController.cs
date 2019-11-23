using System;
using System.Collections;
using System.Collections.Generic;
using Unity.UNetWeaver;
using UnityEngine;
using UnityEngine.UI;

public class MaledictionController : MonoBehaviour {
    public Text maledictionText;
    public GameObject monster;
    public List<Activable> activables;
    public AudioSource music;
    public AudioClip clipAudio;

    private bool _alreadyTriggered = false; //Etre maudit une fois, c'est suffisant
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") || _alreadyTriggered) return;
        _alreadyTriggered = true;
        foreach (var activable in activables) { //On active tous les Activables
            activable.Activate();
        }
        if (clipAudio) { //Si l'objet n'est ppas null
            music.clip = clipAudio;
            music.Play(); //On lance la musique
        }
        if(maledictionText)
            maledictionText.gameObject.SetActive(true);//On affiche le message
        if (monster) {
            monster.transform.GetChild(0).gameObject.SetActive(true); //On affiche le monstre
            monster.GetComponent<MonsterMovementBehavior>()
                .InitPositions(500); //nombre de frames durant lesquelles le monstre est immobile
        }

        StartCoroutine(clearText()); //On supprime l'affichage du message après 5s
        
    }

    private IEnumerator clearText() {
        yield return new WaitForSeconds(5); //Après 5s
        if(monster)
            monster.GetComponent<CapsuleCollider>().enabled = true;//On autorise le GameOver
        if(maledictionText)
            maledictionText.gameObject.SetActive(false);//et on efface le message
    }
}
