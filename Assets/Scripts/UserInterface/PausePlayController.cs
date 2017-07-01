using UnityEngine;
using System.Collections;

public class PausePlayController : MonoBehaviour {
	[SerializeField]
	GameObject pauseButton;
	[SerializeField]
	GameObject playButton;

	void Awake(){
		HideAll ();
	}

	void Start(){
		GameController.Instance.OnGameStart += OnGameStart;
		GameController.Instance.OnGameOver += OnGameOver;

	}

	public void PauseGame(){
		GameController.Instance.PauseGame ();
		ShowPlayButton ();
	}

	public void UnpauseGame(){
		GameController.Instance.UnpauseGame ();
		ShowPauseButton ();
	}

	void ShowPauseButton(){
		playButton.SetActive (false);
		pauseButton.SetActive (true);
	}

	void ShowPlayButton(){
		pauseButton.SetActive (false);
		playButton.SetActive (true);
	}

	void HideAll(){
		playButton.SetActive (false);
		pauseButton.SetActive (false);
	}

	void OnGameStart(){
		ShowPauseButton ();
	}

	void OnGameOver(){
		HideAll ();
	}

	void OnDestroy(){
		GameController.Instance.OnGameStart -= OnGameStart;
	}
}
