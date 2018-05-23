using System;
using UnityEngine;
using System.Collections.Generic;

public enum InputType {
	ARROW_KEYS,
	WASD
}

[RequireComponent(typeof (ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
	public InputType inputType;
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
	public List<char> rawVegetables;
	public SpriteRenderer[] rawVegetableSprites;
	public GameObject rawVegetableHUD;
	public Color color_R,color_B,color_C,color_T,color_E,color_F;
    private void Start()
    {
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
    }



	void Update()
	{
		if (this.tag.Equals ("Player 1")) {
			if (rawVegetables.Count <= 2) {
				if (Input.GetKeyDown (KeyCode.R)) {
					if (!rawVegetables.Contains ('R')) {
						rawVegetables.Add ('R');
					} else {
						rawVegetables.Remove ('R');
					}
				} else if (Input.GetKeyDown (KeyCode.B)) {
					if (!rawVegetables.Contains ('B')) {
						rawVegetables.Add ('B');
					}else {
						rawVegetables.Remove ('B');
					}
				} else if (Input.GetKeyDown (KeyCode.C)) {
					if (!rawVegetables.Contains ('C')) {
						rawVegetables.Add ('C');
					}else {
						rawVegetables.Remove ('C');
					}
				} else if (Input.GetKeyDown (KeyCode.T)) {
					if (!rawVegetables.Contains ('T')) {
						rawVegetables.Add ('T');
					}else {
						rawVegetables.Remove ('T');
					}
				} else if (Input.GetKeyDown (KeyCode.End)) {
					if (!rawVegetables.Contains ('E')) {
						rawVegetables.Add ('E');
					}else {
						rawVegetables.Remove ('E');
					}
				} else if (Input.GetKeyDown (KeyCode.F)) {
					if (!rawVegetables.Contains ('F')) {
						rawVegetables.Add ('F');
					}else {
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
	}


	void SetHUD()
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
            m_Move = v*Vector3.forward + h*Vector3.right;

        // pass all parameters to the character control script
        m_Character.Move(m_Move);
    }
}
