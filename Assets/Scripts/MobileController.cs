using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MobileController : MonoBehaviour {
	[SerializeField]
	LeftButtonMobileController leftButton;
	[SerializeField]
	RightButtonMobileController rightButton;

	float h;

	GraphicRaycaster graphicRaycaster;

	void Awake(){
		if (!Application.isMobilePlatform)
			gameObject.SetActive (false);
		else {
			graphicRaycaster = GetComponent<GraphicRaycaster> ();
			graphicRaycaster.enabled = false;
		}
	}

	void Start(){
		GameController.Instance.OnGameStart += OnGameStart;
		GameController.Instance.OnGameOver += OnGameOver;


	}

	public float GetHorizontal(){
		return h;
	}

	void Update(){
		if (leftButton.Pressed) {
			h = -1;
		} else if (rightButton.Pressed) {
			h = 1;
		} else {
			h = 0;
		}
		Debug.Log (h);
	}

	void OnGameStart(){
		graphicRaycaster.enabled = true;
	}

	void OnGameOver(){
		graphicRaycaster.enabled = false;
	}

	void OnDestroy(){
		GameController.Instance.OnGameStart -= OnGameStart;
		GameController.Instance.OnGameOver -= OnGameOver;
	}

}
