using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PresentationController : MonoBehaviour {
	public PresentationScene [] scenes;

	public bool IsVisible{ get { return menuHider.IsVisible; } }

	MenuHider menuHider;

	int current = 0;


	void Awake(){
		menuHider = GetComponent<MenuHider> ();

	}

	void Start(){
		GameController.Instance.OnPressEnter += OnPressEnter;

		for (int i = 0; i < scenes.Length; i++) {
			scenes [i].Hide ();
		}
	}

	public void StartSequence(){
		menuHider.Show ();
		scenes [current].Show();
	}

	public void Next(){
		scenes [current].Hide ();;
		scenes[++current].Show();
	}

	public void StartGame(){
		menuHider.Hide ();
		scenes[current].Hide();
		GameController.Instance.StartGame ();
	}

	void OnPressEnter(){
		if( IsVisible ){
			Button button = scenes [current].GetComponentInChildren<Button> ();

			var pointer = new PointerEventData (EventSystem.current);

			ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.pointerClickHandler);
		}
	}

	void OnDestroy(){
		GameController.Instance.OnPressEnter -= OnPressEnter;

	}
}
