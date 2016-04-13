using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
	public void EnterTetris ()
	{
		SceneManager.LoadScene ("Tetris");
	}

	public void EnterGamesStart ()
	{
		SceneManager.LoadScene ("GamesStart");
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
