using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour {
	public ThirdPersonUserControl thirdPersonUserControl;
	private int maxNumberOfVegetablesPlayerCanCarry = 2;

	void OnMouseDown()
	{
		if (thirdPersonUserControl.isInsideVegetablePickUpZone) {
			string objectName = (this.gameObject.name.Remove (0, this.gameObject.name.Length - 1));
			char charToBeAdded = objectName [0];
			if (thirdPersonUserControl.rawVegetables.Count < maxNumberOfVegetablesPlayerCanCarry) {
				if (!thirdPersonUserControl.rawVegetables.Contains (charToBeAdded)) {
					thirdPersonUserControl.rawVegetables.Add (charToBeAdded);
				} else {
					thirdPersonUserControl.rawVegetables.Remove (charToBeAdded);
				}
				if (thirdPersonUserControl.rawVegetables.Count == 0) {
					thirdPersonUserControl.rawVegetableHUD.SetActive (false);
				} else {
					thirdPersonUserControl.rawVegetableHUD.SetActive (true);
					thirdPersonUserControl.SetHUD ();
				}
			}else if(thirdPersonUserControl.rawVegetables.Count == maxNumberOfVegetablesPlayerCanCarry){
				if (thirdPersonUserControl.rawVegetables.Contains (charToBeAdded)) {
					thirdPersonUserControl.rawVegetables.Remove (charToBeAdded);
				}
				if (thirdPersonUserControl.rawVegetables.Count == 0) {
					thirdPersonUserControl.rawVegetableHUD.SetActive (false);
				} else {
					thirdPersonUserControl.rawVegetableHUD.SetActive (true);
					thirdPersonUserControl.SetHUD ();
				}
			}
		}
	}
}
