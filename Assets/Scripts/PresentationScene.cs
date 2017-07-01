using UnityEngine;
using System.Collections;

public class PresentationScene : MonoBehaviour {

	MenuHider menuHider;

	void Awake(){
		menuHider = GetComponent<MenuHider> ();
	}

	public void Show(){
		menuHider.Show ();
	}

	public void Hide(){
		menuHider.Hide ();
	}
}
