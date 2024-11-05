using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject musicPlayer;
    public void StartGame(){
        DontDestroyOnLoad(musicPlayer);
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
