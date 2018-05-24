using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : MonoBehaviour {
	public ThirdPersonUserControl thirdPersonUserControl;
	public bool isTriggerCheckForPickUp;

	void OnTriggerEnter()
	{
		if (isTriggerCheckForPickUp) {
			thirdPersonUserControl.isInsideVegetablePickUpZone = true;
		} else {
		}
		Debug.Log("Enable Trigger");
	}

	void OnTriggerExit()
	{
		if (isTriggerCheckForPickUp) {
			thirdPersonUserControl.isInsideVegetablePickUpZone = false;
		} else {
		}
		Debug.Log("Disable Trigger");
	}

}
