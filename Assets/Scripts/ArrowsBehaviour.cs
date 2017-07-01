using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArrowsBehaviour : MonoBehaviour {
	[SerializeField]
	SpriteRenderer leftArrow;
	[SerializeField]
	SpriteRenderer rightArrow;
	[SerializeField]
	GameObject controlHelp;

	bool isBlinking;

	void Awake(){
		Hide ();
		isBlinking = false;
	}

	void Start(){
		HamsterController.Instance.GetComponent<HamsterMovement> ().OnMove += OnMove;
		GameController.Instance.OnGameStart += OnGameStart;
		GameController.Instance.OnGameOver += OnGameOver;
		GameController.Instance.OnPauseGame += OnPauseGame;
		GameController.Instance.OnUnpauseGame += OnUnpauseGame;

		controlHelp.SetActive (false);
	}

	void Blink(){
		controlHelp.SetActive (true);
		StartCoroutine ("BlinkCoroutine");
	}

	IEnumerator BlinkCoroutine(){
		Show ();
		yield return new WaitForSeconds (0.4f);
		Hide ();
		yield return new WaitForSeconds (0.3f);
		StartCoroutine ("BlinkCoroutine");
	}

	public void Disable(){
		StopAllCoroutines ();
		controlHelp.SetActive (false);
		Hide ();
	}

	void Show(){
		leftArrow.enabled = true;
		rightArrow.enabled = true;
	}

	void Hide(){
		leftArrow.enabled = false;
		rightArrow.enabled = false;
	}
		
	void Update(){
		Vector3 position = transform.position;
		position.y = position.y + (Mathf.PingPong (Time.time/2, 0.5f) - 0.25f)*Time.deltaTime;
		transform.position = position;
	}

	void OnGameStart(){
		if (GameController.Instance.IsFirstStart || isBlinking) {
			isBlinking = true;
			Blink ();
		}
	}

	void OnMove(){
		isBlinking = false;
		Disable ();
	}

	void OnGameOver(){
		Disable ();
	}

	void OnPauseGame(){
		Disable ();
	}

	void OnUnpauseGame(){
		if (isBlinking)
			Blink ();
	}

	void OnDestroy(){
		HamsterController.Instance.GetComponent<HamsterMovement> ().OnMove -= OnMove;
		GameController.Instance.OnGameStart -= OnGameStart;
		GameController.Instance.OnGameOver -= OnGameOver;
		GameController.Instance.OnPauseGame -= OnPauseGame;
		GameController.Instance.OnUnpauseGame -= OnUnpauseGame;
	}
}
