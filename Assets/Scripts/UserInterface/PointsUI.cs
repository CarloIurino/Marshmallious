using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointsUI : MonoBehaviour {
	[SerializeField]
	Text marshmallowsNumber;

	MenuHider menuHider;

	Outline outline;
	Shadow shadow;

	void Awake(){
		menuHider = GetComponent<MenuHider> ();
		outline = marshmallowsNumber.GetComponent<Outline> ();
		shadow = marshmallowsNumber.GetComponent<Shadow> ();
	}

	public void Show (){
		menuHider.Show ();
		shadow.enabled = true;
		outline.enabled = true;
	}

	public void Hide(){
		shadow.enabled = false;
		menuHider.Hide ();
		outline.enabled = false;
	}

	public void UpdateMarshmallows(){
		marshmallowsNumber.text = GameController.Instance.Marshmallows.ToString ();
	}

}
