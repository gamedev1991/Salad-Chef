using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour {
	public ThirdPersonUserControl thirdPersonUserControl;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		if (thirdPersonUserControl.isInsideVegetablePickUpZone) {
			string objectName = (this.gameObject.name.Remove (0, this.gameObject.name.Length - 1));
			char charToBeAdded = objectName [0];
			if (thirdPersonUserControl.rawVegetables.Count <= 2) {
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
			} 
		}
	}
}
