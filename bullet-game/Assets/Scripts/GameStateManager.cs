using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState
{
    Default, StartMenu, InLevel, Options, Paused
}
public class GameStateManager : MonoBehaviour
{
    public GameState State { get; set; }
    //private GameState state = GameState.Default;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        State = GameState.Default;
        cam = Camera.main;
        if(State == GameState.Default)
        {
            LoadMap("StartMenu");
            State = GameState.StartMenu;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && State == GameState.InLevel)
        {
            CallPause();
        }
    }
    public void StartGame()
    {
        this.LoadMap("Level1");
        // Does a single mode load of the map scene
        AsyncOperation op = SceneManager.UnloadSceneAsync("StartMenu");
        //op.completed += this.UnloadLoadingScreen; // Unloads loading screen on map load
        //Remove Camera and Light from scene
        cam.enabled = false;
        State = GameState.InLevel;
    }
    public void NewGame()
    {
        this.LoadMap("Level1");
        // Does a single mode load of the map scene
        AsyncOperation op = SceneManager.UnloadSceneAsync("StartMenu");
        //op.completed += this.UnloadLoadingScreen; // Unloads loading screen on map load
        //Remove Camera and Light from scene
        cam.enabled = false;
        State = GameState.InLevel;
    }
    public void ReturnToMenu()
    {

        //AsyncOperation op = SceneManager.UnloadSceneAsync("Level1");
        //op.completed += this.UnloadLoadingScreen; // Unloads loading screen on map load
        //Remove Camera and Light from scene

        State = GameState.StartMenu;
        for(int i = 0; i<SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).name != "Main")
            {
                AsyncOperation op = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
            }
        }
        this.LoadMap("StartMenu");
        cam.enabled = true;
        Time.timeScale = 1;
    }
    public void OpenOptions()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadMap(string mapName)
    {

        this.LoadLoadingScreen(); // Show loading screen

        // Does a single mode load of the map scene
        AsyncOperation op = SceneManager.LoadSceneAsync(mapName, LoadSceneMode.Additive);
        op.completed += this.UnloadLoadingScreen; // Unloads loading screen on map load
        //op.completed += this.LoadGui; // Load GUI on complete
        //op.completed += this.IntializePlayers; // Place players in world

        // Saves the name of the current map
        //currentMap = mapName;
    }
    private void LoadLoadingScreen()
    {
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);
    }
    private void UnloadLoadingScreen(AsyncOperation op = null)
    {
        SceneManager.UnloadSceneAsync("LoadingScreen");
    }

    //UI Functions
    public void Pause(InputAction.CallbackContext context)
    {
        //CallPause();
        
    }
    public void Pause()
    {
        CallPause();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void CallPause()
    {
        if (State == GameState.InLevel)
        {
            Debug.Log("Pausing...");
            State = GameState.Paused;
            Time.timeScale = 0;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }
        else if(State == GameState.Paused)
        {
            Debug.Log("Un-Pausing...");
            State = GameState.InLevel;
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("PauseMenu");
        }
    }
}
