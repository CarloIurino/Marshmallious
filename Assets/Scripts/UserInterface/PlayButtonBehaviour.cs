using UnityEngine;
using System.Collections;

public class PlayButtonBehaviour : MonoBehaviour {

	public void UnpauseGame(){
		GetComponentInParent<PausePlayController> ().UnpauseGame ();
	}
}
