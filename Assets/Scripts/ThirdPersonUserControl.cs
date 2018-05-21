using System;
using UnityEngine;

public enum InputType {
	ARROW_KEYS,
	WASD
}

[RequireComponent(typeof (ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
	public InputType inputType;
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
    
    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
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
			h = Input.GetAxis ("Horizontal");
			v = Input.GetAxis ("Vertical");
		}
        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v*m_CamForward + h*m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v*Vector3.forward + h*Vector3.right;
        }

        // pass all parameters to the character control script
        m_Character.Move(m_Move);
    }
}
