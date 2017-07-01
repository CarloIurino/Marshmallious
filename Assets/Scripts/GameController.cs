using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	static GameController _instance;
	public static GameController Instance { get {return _instance;}}

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

	void InitOnAwake(){
		HamsterController.Instance.DisableHamster ();
	}
		
	public void StartGame(){
		UserInterfaceController.Instance.HideStartScreen ();
		UserInterfaceController.Instance.ShowPointUI ();

		HamsterController.Instance.Reset ();
		HamsterController.Instance.EnableHamster ();
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

		yield return new WaitForSeconds (0.6f);
		HamsterController.Instance.Die ();
		yield return new WaitForSeconds (2);

		UserInterfaceController.Instance.HidePointUI ();
		UserInterfaceController.Instance.ShowGameOver ();
	}

	public void RestartGame(){
		Reset ();
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

}
