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
		}
	}

	void OnTriggerExit()
	{
		if (isTriggerCheckForPickUp) {
			thirdPersonUserControl.isInsideVegetablePickUpZone = false;
		} 
	}

}
