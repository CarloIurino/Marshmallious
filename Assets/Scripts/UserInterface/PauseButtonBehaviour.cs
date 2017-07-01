using UnityEngine;
using System.Collections;

public class PauseButtonBehaviour : MonoBehaviour {

	public void PauseGame(){
		GetComponentInParent<PausePlayController> ().PauseGame ();
	}

}
