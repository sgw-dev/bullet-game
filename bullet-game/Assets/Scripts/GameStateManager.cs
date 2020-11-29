using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Default, StartMenu, InLevel, Options
}
public class GameStateManager : MonoBehaviour
{
    private GameState state = GameState.Default;
    // Start is called before the first frame update
    void Start()
    {
        if(state == GameState.Default)
        {
            LoadMap("StartMenu");
            state = GameState.StartMenu;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        this.LoadMap("Level1");
        // Does a single mode load of the map scene
        AsyncOperation op = SceneManager.UnloadSceneAsync("StartMenu");
        //op.completed += this.UnloadLoadingScreen; // Unloads loading screen on map load
    }
    public void NewGame()
    {

    }
    public void OpenOptions()
    {

    }
    public void QuitGame()
    {

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
}
