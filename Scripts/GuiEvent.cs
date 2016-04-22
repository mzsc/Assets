using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GuiEvent : MonoBehaviour {
    private string SceneName;

    public void SetSceneName(string Name){
        SceneName = Name;
    }

    public void ChangeScene(){
        if (SceneName == "???") {
            return ;
        }
        SceneManager.LoadScene(SceneName);
    }

    public void EnterTetris ()
    {
        SceneManager.LoadScene ("Tetris");
    }

    public void EnterStartGame ()
    {
        SceneManager.LoadScene ("StartGame");
    }

    public void EnterPlaneWar ()
    {
        SceneManager.LoadScene ("PlaneWar");
    }
        
    public void GameOver ()
    {
        Application.Quit ();
    }
}
