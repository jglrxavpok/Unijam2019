using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button play;
    public Button exit;

    public void Start()
    {
        play.onClick.AddListener(TaskOnClickPlay);
        exit.onClick.AddListener(TaskOnClickExit);
    }

    void TaskOnClickPlay()
    {
        SceneManager.LoadScene("OpeningCinematic");
    }

    void TaskOnClickExit()
    {
        Application.Quit();
    }
}
