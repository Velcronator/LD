using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //[SerializeField] float sceneLoadDelay = 2f;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        SceneFader.instance.LoadLevel("Game");
    }

    public void LoadMap()
    {
        SceneFader.instance.LoadLevel("Map");
    }

    public void LoadMainMenu()
    {
        SceneFader.instance.LoadLevel("MainMenu");
    }

    public void LoadGameOver()
    {
        SceneFader.instance.LoadLevel("GameOver");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

}
