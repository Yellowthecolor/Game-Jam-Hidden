using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShortcutManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager singleton;

    [SerializeField] MrGoop playerGoop;
    [SerializeField] float timeLimit;
    [SerializeField] string nextScene;
    [SerializeField] string currentScene;

    [Header("Helpers")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject levelCompletePanel;

    void Awake(){
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        if (singleton == null){
            singleton = this;
        } else {
            Destroy(this.gameObject);
        }
        StartCoroutine(GlobalTimerRoutine());
    }

    
    IEnumerator GlobalTimerRoutine(){
        while(!playerGoop.CheckDeathStatus() && !playerGoop.CheckWinStatus()){
            yield return null;
        }

        if (playerGoop.CheckDeathStatus()){
            GameOver();
        } else {
            LevelComplete();
        }
        yield return null;
    }

    public void GameOver(){
        gameOverPanel.SetActive(true);
    }

    public void LevelComplete(){
        levelCompletePanel.SetActive(true);
    }

    public void MainMenu(){
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame(){
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextLevel(){
        SceneManager.LoadScene(nextScene);
    }

}



