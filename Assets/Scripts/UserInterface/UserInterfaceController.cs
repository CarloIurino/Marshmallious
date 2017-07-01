using UnityEngine;
using System.Collections;

public class UserInterfaceController : MonoBehaviour {
	static UserInterfaceController _instance;
	public static UserInterfaceController Instance { get{ return _instance;}}

	[SerializeField]
	StartScreen startScreen;
	[SerializeField]
	GameOverUI gameOverUI;
	[SerializeField]
	PointsUI pointsUI;

	void Awake () {
		if (_instance == null)
			_instance = this;
		else
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

	}

	void Start(){
		ShowStartScreen ();
	}
	
	public void ShowStartScreen(){
		startScreen.Show ();
	}

	public void HideStartScreen(){
		startScreen.Hide ();
	}

	public void ShowGameOver(){
		gameOverUI.ShowGameOverUI ();
	}

	public void HideGameOver(){
		gameOverUI.HideGameOverUI ();
	}


	public void ShowPointUI(){
		pointsUI.Show ();
	}

	public void HidePointUI(){
		pointsUI.Hide ();
	}

	public void UpdateMarshmallows(){
		pointsUI.UpdateMarshmallows ();
	}
}
