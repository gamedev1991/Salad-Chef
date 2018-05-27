using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoppingBoard : MonoBehaviour {
	public ThirdPersonUserControl thirdPersonUserControl;
	public bool isVegetableChopping;
	public float choppingTimePerVegetable;
	public GameObject choppingTextObject;
	public Text choppingText;
	float choppingTime;
	bool enableText;
	char vegetableBeingChopped;
	
	// Update is called once per frame
	void Update () {
		if (isVegetableChopping) {
			if (Time.time > choppingTime) {
				choppingText.text = "Chopping " + vegetableBeingChopped + ". . ";
				enableText = !enableText;
				choppingTime = Time.time+0.5f;
				choppingTextObject.SetActive (enableText);
			}
		}
	}

	void OnTriggerEnter()
	{
		if (thirdPersonUserControl.rawVegetables.Count > 0) {
			isVegetableChopping = true;
			StartCoroutine (StartChopping ());
		}
	}

	IEnumerator StartChopping(){
		choppingTime = Time.time +0.5f;
		for (int index = 0; index < thirdPersonUserControl.rawVegetables.Count; index++) {
			vegetableBeingChopped = thirdPersonUserControl.rawVegetables [index];
			thirdPersonUserControl.choppedVegetables += thirdPersonUserControl.rawVegetables [index]+",";
			yield return new WaitForSeconds (choppingTimePerVegetable);
		}
		isVegetableChopping = false;
		thirdPersonUserControl.rawVegetables.Clear ();
		thirdPersonUserControl.SetHUD();
		thirdPersonUserControl.rawVegetableHUD.SetActive (false);
		choppingTextObject.SetActive (false);
		thirdPersonUserControl.choppedVegetablesHUD.SetActive (true);
		thirdPersonUserControl.choppedVegetablesText.text = thirdPersonUserControl.choppedVegetables;
	}



	void OnTriggerExit()
	{
		isVegetableChopping = false;
	}
}
