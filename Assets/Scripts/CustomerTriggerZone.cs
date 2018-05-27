using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTriggerZone : MonoBehaviour {
	public ThirdPersonUserControl player1_ThirdPersonUserControl;
	public ThirdPersonUserControl player2_ThirdPersonUserControl;

	void OnTriggerEnter(Collider player)
	{
		if (player.name.Equals ("Player1")) {
			player1_ThirdPersonUserControl.isNearCustomer = true;
		} else {
			player2_ThirdPersonUserControl.isNearCustomer = true;
		}
	}

	void OnTriggerExit(Collider player)
	{
		if (player.name.Equals ("Player1")) {
			player1_ThirdPersonUserControl.isNearCustomer = false;
		} else {
			player2_ThirdPersonUserControl.isNearCustomer = false;
		}
	}
}
