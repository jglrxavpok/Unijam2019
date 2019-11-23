using System;
using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("Malédiction invoquée");
        _alreadyTriggered = true;
        foreach (var activable in activables) { //On active tous les Activables
            activable.Activate();
        }
        if (clipAudio) { //Si l'objet n'est pas null
            music.clip = clipAudio;
            music.Play(); //On lance la musique
        }
        else {
            Debug.Log("Référence à music non définie dans le MaledictionController");
        }

        if (maledictionText) {
            maledictionText.gameObject.SetActive(true); //On affiche le message
        }
        else {
            Debug.Log("Référence à maledictionText non définie dans le MaledictionController");
        }
        if (monster) {
            monster.transform.GetChild(0).gameObject.SetActive(true); //On affiche le monstre
            monster.GetComponent<MonsterMovementBehavior>()
                .InitPositions(500); //nombre de frames durant lesquelles le monstre est immobile
        } else {
            Debug.Log("Référence à monster non définie dans le MaledictionController");
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
