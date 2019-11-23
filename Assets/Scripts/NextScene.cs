using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
    public string nextScene;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            SceneManager.LoadScene(nextScene);
            SaveScene.saveScene = nextScene;

        }
    }

    public string GetNextScene() {
        return nextScene;
    }
}
