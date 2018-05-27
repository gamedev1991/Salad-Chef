using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {
	public ThirdPersonUserControl player1_ThirdPersonUserControl;
	public ThirdPersonUserControl player2_ThirdPersonUserControl;
	public CustomerManager customerManager;
	public GameObject startScreen;
	public GameObject resultScreen;
	public Text resultScreenText;
	private IEnumerator checkTimerCoroutine;
	// Use this for initialization
	void Start () {
		checkTimerCoroutine = CheckTimer ();
		StartCoroutine (checkTimerCoroutine);
	}
	
	public void StartGame()
	{
		startScreen.SetActive (false);
		player1_ThirdPersonUserControl.enabled = true;
		player2_ThirdPersonUserControl.enabled = true;
		customerManager.enabled = true;
	}

	IEnumerator CheckTimer()
	{
		while(!player1_ThirdPersonUserControl.isTimerOver || !player2_ThirdPersonUserControl.isTimerOver){
			yield return new WaitForSeconds (1f);
		}
		GameOver ();

	}

	void GameOver()
	{
		resultScreen.SetActive (true);
		if (player1_ThirdPersonUserControl.playerScore > player2_ThirdPersonUserControl.playerScore) {
			resultScreenText.text = "Player 1 Wins";
		} else if (player1_ThirdPersonUserControl.playerScore < player2_ThirdPersonUserControl.playerScore) {
			resultScreenText.text = "Player 2 Wins";
		} else {
			resultScreenText.text = "Its a Draw !!";
		}
		player1_ThirdPersonUserControl.enabled = false;
		player2_ThirdPersonUserControl.enabled = false;
		customerManager.enabled = false;
		Time.timeScale = 0;
	}

	public void RestartGame()
	{
		Time.timeScale = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Demo");
	}
}
