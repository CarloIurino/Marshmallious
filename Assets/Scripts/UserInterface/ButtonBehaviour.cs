using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof( Button) )]
public class ButtonBehaviour : MonoBehaviour {
	AudioSource buttonClickAudio;

	Button button;

	void Awake(){
		buttonClickAudio = gameObject.AddComponent<AudioSource> ();
		buttonClickAudio.playOnAwake = false;

		button = GetComponent<Button> ();

		buttonClickAudio.clip =  Resources.Load ("ButtonClick") as AudioClip;
	}

	void Start(){
		button.onClick.AddListener (PlayButtonAudio);
	}

	void PlayButtonAudio(){
		buttonClickAudio.Play ();
	}
}
