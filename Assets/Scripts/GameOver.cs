using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Button play;
    public Button exit;

    public void Start()
    {
        play.onClick.AddListener(TaskOnClickRetry);
        exit.onClick.AddListener(TaskOnClickMainMenu);
    }

    void TaskOnClickRetry() {
        SceneManager.LoadScene(SaveScene.saveScene);
    }

    void TaskOnClickMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
