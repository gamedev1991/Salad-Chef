using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public enum InputType {
	ARROW_KEYS,
	WASD
}

[RequireComponent(typeof (ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
	public InputType inputType;
	public List<char> rawVegetables;
	public SpriteRenderer[] rawVegetableSprites;
	public GameObject rawVegetableHUD;
	public Color color_R,color_B,color_C,color_T,color_E,color_F;
	public RectTransform rawVegetableHUDCanvas;
	public bool isInsideVegetablePickUpZone;
	public bool isNearDustbin;
	public ChoppingBoard choppingBoard;
	public string choppedVegetables;
	public GameObject choppedVegetablesHUD;
	public TextMesh choppedVegetablesText;
	public Text playerScoreText;
	public Text timerText;
	public Dustbin dustbin;
	[HideInInspector]
	public int playerScore;
	public float totalTimeForPlaying;
	public bool isNearCustomer;
	public CustomerManager customerManager;
	public bool isCustomerAngry;
	public bool isTimerOver;

	private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
	private IEnumerator timerCoroutine;
	private string sortedSaladCombination;
	private int maxNumberOfVegetablesPlayerCanCarry = 2;
	private int valuePerVegetable = 5;
	private Vector3 m_Move;

    private void Start()
    {
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
		playerScore = 0;
		playerScoreText.text = playerScore.ToString();
		totalTimeForPlaying = Time.time + totalTimeForPlaying;
		timerCoroutine = RunTimer ();
		StartCoroutine (timerCoroutine);
    }

	void Update()
	{
		if (this.tag.Equals ("Player 1") && isInsideVegetablePickUpZone) {
			if (rawVegetables.Count < maxNumberOfVegetablesPlayerCanCarry) {
				if (Input.GetKeyDown (KeyCode.R)) {
					if (!rawVegetables.Contains ('R')) {
						rawVegetables.Add ('R');
					} else {
						rawVegetables.Remove ('R');
					}
				} else if (Input.GetKeyDown (KeyCode.B)) {
					if (!rawVegetables.Contains ('B')) {
						rawVegetables.Add ('B');
					} else {
						rawVegetables.Remove ('B');
					}
				} else if (Input.GetKeyDown (KeyCode.C)) {
					if (!rawVegetables.Contains ('C')) {
						rawVegetables.Add ('C');
					} else {
						rawVegetables.Remove ('C');
					}
				} else if (Input.GetKeyDown (KeyCode.T)) {
					if (!rawVegetables.Contains ('T')) {
						rawVegetables.Add ('T');
					} else {
						rawVegetables.Remove ('T');
					}
				} else if (Input.GetKeyDown (KeyCode.E)) {
					if (!rawVegetables.Contains ('E')) {
						rawVegetables.Add ('E');
					} else {
						rawVegetables.Remove ('E');
					}
				} else if (Input.GetKeyDown (KeyCode.F)) {
					if (!rawVegetables.Contains ('F')) {
						rawVegetables.Add ('F');
					} else {
						rawVegetables.Remove ('F');
					}
				} 
				if (rawVegetables.Count == 0) {
					rawVegetableHUD.SetActive (false);
				} else {
					rawVegetableHUD.SetActive (true);
					SetHUD ();
				}
			} else if (rawVegetables.Count == maxNumberOfVegetablesPlayerCanCarry) {
				if (Input.GetKeyDown (KeyCode.R)) {
					if (rawVegetables.Contains ('R')) {
						rawVegetables.Remove ('R');
					}
				} else if (Input.GetKeyDown (KeyCode.B)) {
					if (rawVegetables.Contains ('B')) {
						rawVegetables.Remove ('B');
					}
				} else if (Input.GetKeyDown (KeyCode.C)) {
					if (rawVegetables.Contains ('C')) {
						rawVegetables.Remove ('C');
					}
				} else if (Input.GetKeyDown (KeyCode.T)) {
					if (rawVegetables.Contains ('T')) {
						rawVegetables.Remove ('T');
					}
				} else if (Input.GetKeyDown (KeyCode.E)) {
					if (rawVegetables.Contains ('E')) {
						rawVegetables.Remove ('E');
					}
				} else if (Input.GetKeyDown (KeyCode.F)) {
					if (rawVegetables.Contains ('F')) {
						rawVegetables.Remove ('F');
					}
				} 

				if (rawVegetables.Count == 0) {
					rawVegetableHUD.SetActive (false);
				} else {
					rawVegetableHUD.SetActive (true);
					SetHUD ();
				}
			}
		}

		if (this.tag.Equals ("Player 1") && choppedVegetablesHUD.activeSelf && isNearDustbin) {
			if (Input.GetKeyDown (KeyCode.Delete)) {
				PutVegetablesInDustbin ();
			}
		}

		if (this.tag.Equals ("Player 1") && isNearCustomer) {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				CheckOrder (0);
			}else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				CheckOrder (1);
			}else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				CheckOrder (2);
			}else if (Input.GetKeyDown (KeyCode.Alpha4)) {
				CheckOrder (3);
			}
		}
	}

	public void PutVegetablesInDustbin()
	{
		int numberOfVegetablesThrown = 0;
		for (int i = 0; i < choppedVegetables.Length; i++) {
			if (choppedVegetables [i] == ',') {
				numberOfVegetablesThrown++;
			}
		}

		playerScore -= numberOfVegetablesThrown * valuePerVegetable;
		playerScoreText.text = playerScore.ToString();
		choppedVegetablesHUD.SetActive (false);
		choppedVegetables = "";
	}

	public void SetHUD()
	{
		for (int i = 0; i < rawVegetableSprites.Length; i++) {
			rawVegetableSprites [i].gameObject.SetActive(false);
		}
		for (int i = 0; i < rawVegetables.Count; i++) {
			rawVegetableSprites [i].gameObject.SetActive(true);
			switch(rawVegetables[i])
			{
			case 'R':
				rawVegetableSprites [i].color = color_R;
				break;
			case 'B':
				rawVegetableSprites [i].color = color_B;
				break;
			case 'C':
				rawVegetableSprites [i].color = color_C;
				break;
			case 'T':
				rawVegetableSprites [i].color = color_T;
				break;
			case 'E':
				rawVegetableSprites [i].color = color_E;
				break;
			case 'F':
				rawVegetableSprites [i].color = color_F;
				break;

			}
		}
	}
    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
		if (!choppingBoard.isVegetableChopping) {
			float h = 0.0f;
			float v = 0.0f;

			if (inputType.Equals (InputType.ARROW_KEYS)) {
				// read inputs
				h = Input.GetAxis ("Horizontal_ArrowKeys");
				v = Input.GetAxis ("Vertical_ArrowKeys");
			} else {
				h = Input.GetAxis ("Horizontal_WASD");
				v = Input.GetAxis ("Vertical_WASD");
			}

			// we use world-relative directions in the case of no main camera
			m_Move = v * Vector3.forward + h * Vector3.right;

			// pass all parameters to the character control script
		} else {
			m_Move = Vector3.zero;
		}
		m_Character.Move (m_Move);


    }

	void LateUpdate()
	{
		rawVegetableHUDCanvas.localPosition = new Vector3 (this.transform.localPosition.x, rawVegetableHUDCanvas.localPosition.y, this.transform.localPosition.z);
	}

	IEnumerator RunTimer()
	{
		while (true) {
			float timeDiff = totalTimeForPlaying - Time.time;
			timerText.text = ((int)timeDiff).ToString ();

			if (timeDiff < 0) {
				StopCoroutine (timerCoroutine);
				isTimerOver = true;
			}
			yield return new WaitForSeconds (1f);
		}

	}

	public void CheckOrder(int customerID)
	{
		List<char> orderList = new List<char> ();
		for (int i = 0; i < choppedVegetables.Length; i++) {
			if(choppedVegetables[i] != ','){
				orderList.Add(choppedVegetables[i]);
			}
		}
		orderList.Sort ();
		for (int i = 0; i < orderList.Count; i++) {
			sortedSaladCombination += orderList [i];
		}
		Debug.Log ("sortedSaladCombination = " + sortedSaladCombination);
		Debug.Log ("customerManager.customerList [customerID].sortedCurrentOrder = " + customerManager.customerList [customerID].sortedCurrentOrder);
		if (sortedSaladCombination.Equals (customerManager.customerList [customerID].sortedCurrentOrder)) {
			customerManager.customerList [customerID].ServeCustomer ();
			playerScore += sortedSaladCombination.Length * valuePerVegetable;
			playerScoreText.text = playerScore.ToString ();
			choppedVegetablesHUD.SetActive (false);
			choppedVegetables = "";
			sortedSaladCombination = "";
			Debug.Log ("Correct Order");
		} else {
			isCustomerAngry = true;
			customerManager.customerList [customerID].waitTimeOffset += 1;
			customerManager.customerRenderers [customerID].material = customerManager.angryMaterial;
		}
	}
}
