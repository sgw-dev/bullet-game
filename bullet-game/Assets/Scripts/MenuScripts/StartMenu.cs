using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameStateManager gsm;
    // Start is called before the first frame update
    void Start()
    {
        gsm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartClicked()
    {
        gsm.StartGame();
    }
    public void OptionsClicked()
    {
        gsm.OpenOptions();
    }
    public void NewGameClicked()
    {
        gsm.NewGame();
    }
    public void ExitClicked()
    {
        gsm.QuitGame();
    }
    public void MainMenu()
    {
        gsm.ReturnToMenu();
    }
    public void ResumeClicked()
    {
        gsm.Pause();
    }
}
