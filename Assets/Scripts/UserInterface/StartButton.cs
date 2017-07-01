using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	public void StartGame(){
		GameController.Instance.StartGame ();
	}


}
