using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {
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
