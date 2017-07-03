using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RightButtonMobileController : MonoBehaviour {
	[SerializeField]
	LeftButtonMobileController leftButton;

	public bool Pressed{ get; set; }


	public void SetPressed(){
		Pressed = true;
		leftButton.SetUnpressed ();
	}

	public void SetUnpressed(){
		Pressed = false;
	}


}
