using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaledictionController : MonoBehaviour {
    public Text maledictionText;
    public GameObject monster;

    private bool _alreadyTriggered; //Etre maudit une fois, c'est suffisant
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") || _alreadyTriggered) return;
        maledictionText.gameObject.SetActive(true);//On affiche le message
        monster.transform.GetChild(0).gameObject.SetActive(true);//On affiche le monstre
        monster.GetComponent<MonsterMovementBehavior>().InitPositions(500);//nombre de frames durant lesquelles le monstre est immobile
        StartCoroutine(clearText()); //On supprime l'affichage du message après 5s
        _alreadyTriggered = true;
    }

    private IEnumerator clearText() {
        yield return new WaitForSeconds(5); //Après 5s
        monster.GetComponent<CapsuleCollider>().enabled = true;//On autorise le GameOver
        maledictionText.gameObject.SetActive(false);//et on efface le message
    }
}
