using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {
	MenuHider menuHider;

	public bool IsVisible{ get{ return menuHider.IsVisible;} }

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
