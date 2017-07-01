using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour {
	Button button;

	void Awake(){
		button = GetComponent<Button> ();
	}

	void Start(){
		GameController.Instance.OnPressEnter += OnPressEnter;
	}

	public void StartGame(){
		

		if (GameController.Instance.IsFirstStart)
			GameController.Instance.StartPresentation ();
		else
			GameController.Instance.StartGame ();

	}

	void OnPressEnter(){
		if( UserInterfaceController.Instance.IsStartMenuVisible){
			var pointer = new PointerEventData (EventSystem.current);

			ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.pointerClickHandler);
		}
	}

	void OnDestroy(){
		GameController.Instance.OnPressEnter -= OnPressEnter;

	}

}
