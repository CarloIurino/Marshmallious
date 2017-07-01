using UnityEngine;
using System.Collections;

public class ReturnButton : MonoBehaviour {

	public void ReturnToStartScreen(){
		GameController.Instance.RestartGame ();
	}
}
