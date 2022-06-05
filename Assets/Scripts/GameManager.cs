using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //-------------------------------------------

    public enum GameState
    {
        Menu,
        InGame,
        InHome,
        Pause,
        GameOver
    }

    //-------------------------------------------

    public static GameManager instance;
    public GameState state { get; private set; }
    private AudioManager audioManager;

    //-------------------------------------------

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        audioManager = FindObjectOfType<AudioManager>();

        state = GameState.Menu;
    }

    //-------------------------------------------

    private void Update()
    {
        if (state == GameState.Menu && Input.GetKeyDown(KeyCode.Return))
            StartGame();
    }

    //-------------------------------------------

    public void ChangeScene(string sceneName)
    {
        if (sceneName == string.Empty)
            return;
        
        SceneManager.LoadScene(sceneName);
    }

    //-------------------------------------------

    public void StartGame()
    {
        SceneManager.LoadScene("Scene01");
        audioManager.Play("InGame");
        state = GameState.InGame;
    }

    //-------------------------------------------

    public IEnumerator SetInHomeState()
    {
        state = GameState.InHome;
        audioManager.Stop("InGame");
        audioManager.Play("Home");

        yield return new WaitForSeconds(audioManager.GetClipLength("Home"));
        state = GameState.Menu;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene("Menus");
    }

    //-------------------------------------------
}
