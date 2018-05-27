using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dustbin : MonoBehaviour {
	public ThirdPersonUserControl player1_ThirdPersonUserControl;
	public ThirdPersonUserControl player2_ThirdPersonUserControl;

	void OnMouseDown()
	{
		if(player2_ThirdPersonUserControl.isNearDustbin){
			player2_ThirdPersonUserControl.PutVegetablesInDustbin ();
		}
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.name.Equals ("Player1")) {
			player1_ThirdPersonUserControl.isNearDustbin = true;
		} else {
			player2_ThirdPersonUserControl.isNearDustbin = true;
		}
	}

	void OnTriggerExit(Collider player)
	{
		if (player.name.Equals ("Player1")) {
			player1_ThirdPersonUserControl.isNearDustbin = false;
		} else {
			player2_ThirdPersonUserControl.isNearDustbin = false;
		}
	}
}

