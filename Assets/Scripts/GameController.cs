using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using System;

public class GameController : MonoBehaviour {

	static GameController _instance;
	public static GameController Instance { get {return _instance;}}

	public Action OnPressEnter;
	public Action OnGameStart;
	public Action OnGameOver;
	public Action OnPauseGame;
	public Action OnUnpauseGame;

	[SerializeField]
	PresentationController presentationController;

	DepthOfField depthOfField;

	public bool IsFirstStart { get; set; }

	int marshmallows = 0;
	int maxMarshmallows = 0;

	public int Marshmallows{ get { return marshmallows; } }
	public int Record{ get { return maxMarshmallows; } }

	void Awake(){

		if (_instance == null) {
			_instance = this;

		} else {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);

		InitOnAwake ();
	}

	void Start(){
		MarshmallowController.Instance.MenuRain (0.2f, 0.5f);
		Blur (true);
	}

	void InitOnAwake(){
		IsFirstStart = true;
		HamsterController.Instance.DisableHamster ();
		depthOfField = Camera.main.GetComponent<DepthOfField> ();
		depthOfField.enabled = false;
	}
		

	public void StartPresentation(){
		UserInterfaceController.Instance.HideStartScreen ();
		presentationController.StartSequence ();
	}
		
	public void StartGame(){
		if (OnGameStart != null)
			OnGameStart ();

		IsFirstStart = false;

		Blur (false);
		SoundManager.Instance.PlaySGameSoundtrack ();
		UserInterfaceController.Instance.HideStartScreen ();
		UserInterfaceController.Instance.ShowPointUI ();

		HamsterController.Instance.Reset ();
		HamsterController.Instance.EnableHamster ();

		MarshmallowController.Instance.StopRain ();
		MarshmallowController.Instance.GameRain ();
	}
		

	public void TakeMarshMallow(){
		marshmallows++;

		if (marshmallows > maxMarshmallows) {
			maxMarshmallows = marshmallows;
		}

		UserInterfaceController.Instance.UpdateMarshmallows ();
	}

	public void GameOver(){
		StartCoroutine (GameOverRoutine ());

	}

	IEnumerator GameOverRoutine(){

		HamsterController.Instance.Die ();
		yield return new WaitForSeconds (2);

		if (OnGameOver != null)
			OnGameOver ();

		MarshmallowController.Instance.StopRain ();
		MarshmallowController.Instance.MenuRain (0.2f, 0.5f);
		SoundManager.Instance.PlayGameOver ();
		UserInterfaceController.Instance.HidePointUI ();
		UserInterfaceController.Instance.ShowGameOver ();

		Blur (true);
	}
		

	public void RestartGame(){
		Reset ();
		SoundManager.Instance.StopSnoring ();
		HamsterController.Instance.DisableHamster ();
		UserInterfaceController.Instance.HideGameOver ();
		UserInterfaceController.Instance.HidePointUI ();
		UserInterfaceController.Instance.ShowStartScreen ();
	}

	public void Reset(){
		HamsterController.Instance.Reset ();
		marshmallows = 0;
		UserInterfaceController.Instance.UpdateMarshmallows ();

	}

	public void PauseGame(){
		if (OnPauseGame != null)
			OnPauseGame ();

		Time.timeScale = 0;
		Blur (true);

	}

	public void UnpauseGame(){
		if (OnUnpauseGame != null)
			OnUnpauseGame ();

		Time.timeScale = 1;
		Blur (false);

	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (OnPressEnter != null)
				OnPressEnter ();
		}
	}

	void Blur(bool state){
		#if UNITY_WEBPLAYER
		depthOfField.enabled = state;
		#endif
	}
		

}
