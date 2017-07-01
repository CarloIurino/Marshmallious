using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeftButtonMobileController : MonoBehaviour {
	[SerializeField]
	RightButtonMobileController rightButton;

	public bool Pressed{ get; set; }


	public void SetPressed(){
		Pressed = true;
		rightButton.SetUnpressed ();
	}

	public void SetUnpressed(){
		Pressed = false;
	}


}
