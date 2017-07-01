using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour {
	Button button;

	void Awake(){
		button = GetComponent<Button> ();
	}

	void Start(){
		GameController.Instance.OnPressEnter += OnPressEnter;
	}

	public void ReturnToStartScreen(){
		GameController.Instance.RestartGame ();
	}

	void OnPressEnter(){
		if( UserInterfaceController.Instance.IsGameOvertMenuVisible){
			var pointer = new PointerEventData (EventSystem.current);

			ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.pointerClickHandler);
		}
	}

	void OnDestroy(){
		GameController.Instance.OnPressEnter -= OnPressEnter;

	}
}
