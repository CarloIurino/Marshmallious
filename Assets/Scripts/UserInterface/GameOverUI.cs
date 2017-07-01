using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
	MenuHider menuHider;

	[SerializeField]
	Text marshmallowsNumber;
	[SerializeField]
	Text recordnumber;

	public bool IsVisible{ get { return menuHider.IsVisible; } }

	void Awake(){
		menuHider = GetComponent<MenuHider> ();
	}

	public void ShowGameOverUI(){
		UpdateResults ();
		menuHider.Show ();
	}

	public void HideGameOverUI(){
		menuHider.Hide ();
	}

	public void UpdateResults(){
		marshmallowsNumber.text = GameController.Instance.Marshmallows.ToString();
		recordnumber.text = GameController.Instance.Record.ToString();
	}

}
