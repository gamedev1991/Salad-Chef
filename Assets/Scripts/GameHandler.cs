using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {
	public ThirdPersonUserControl player1_ThirdPersonUserControl;
	public ThirdPersonUserControl player2_ThirdPersonUserControl;
	public CustomerManager customerManager;
	public GameObject startScreen;
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
	}
}
