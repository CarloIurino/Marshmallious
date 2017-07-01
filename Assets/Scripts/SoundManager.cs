using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	[SerializeField]
	AudioClip startSoundtrack;
	[SerializeField]
	AudioClip gameSoundtrack;
	[SerializeField]
	AudioClip gameOver;
	[SerializeField]
	AudioClip snoring;
	[SerializeField]
	AudioClip eating;


	AudioSource audioSource;
	AudioSource eatingAudioSource;
	AudioSource snoringAudioSource;

	static SoundManager _instance;
	public static SoundManager Instance{ get { return _instance; } }

	void Awake(){

		if( _instance == null ){
			_instance = this;
		}
		else {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);

		InitOnAwake ();
	}

	void InitOnAwake(){
		audioSource = gameObject.AddComponent<AudioSource> ();
		InitAudioSource (audioSource);
		audioSource.loop = true;

		eatingAudioSource = gameObject.AddComponent<AudioSource> ();
		eatingAudioSource.clip = eating;
		InitAudioSource (eatingAudioSource);

		snoringAudioSource = gameObject.AddComponent<AudioSource> ();
		snoringAudioSource.clip = snoring;
		InitAudioSource (snoringAudioSource);
	}

	void InitAudioSource ( AudioSource source){
		source.playOnAwake = false;
		source.loop = false;
		source.bypassEffects = true;
		source.bypassListenerEffects = true;
		source.bypassReverbZones = true;
	}

	void Start(){
		GameController.Instance.OnPauseGame += OnPauseGame;
		GameController.Instance.OnUnpauseGame += OnUnpauseGame;

		PlayStartSoundtrack ();
	}

	public void PlayStartSoundtrack(){
		audioSource.clip = startSoundtrack;
		audioSource.Play ();
	}


	public void PlaySGameSoundtrack(){
		audioSource.clip = gameSoundtrack;
		audioSource.Play ();
	}

	public void StopGameSoundtrack(){
		audioSource.Stop ();
	}
		

	public void PlayGameOver(){
		StopCoroutine ("PlayGameOverRoutine");
		StartCoroutine ("PlayGameOverRoutine");
	}

	void PlaySnoring(){
		snoringAudioSource.volume = 0.2f;
		snoringAudioSource.Play ();
		StartCoroutine ("SnooringVolumeDown");
	}

	public void StopSnoring(){
		snoringAudioSource.Stop ();
		StopCoroutine ("SnooringVolumeDown");
	}

	IEnumerator SnooringVolumeDown(){
		yield return new WaitForSeconds (0.1f);
		snoringAudioSource.volume -= 0.001f;
		StartCoroutine ("SnooringVolumeDown");
	}

	public void EatingSound(){
		eatingAudioSource.Play ();
	}

	IEnumerator PlayGameOverRoutine(){
		StopGameSoundtrack ();

		audioSource.PlayOneShot (gameOver);

		PlaySnoring ();

		yield return new WaitForSeconds (2.5f);

		PlayStartSoundtrack ();
	}

	void OnPauseGame(){
		AudioListener.volume = 0.1f;
	}

	void OnUnpauseGame(){
		AudioListener.volume = 1;
	}

	void OnDestroy(){
		GameController.Instance.OnPauseGame -= OnPauseGame;
		GameController.Instance.OnUnpauseGame -= OnUnpauseGame;
	}
}
