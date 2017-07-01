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
	[SerializeField]
	GameObject vignette;

	public bool IsStartMenuVisible{ get { return startScreen.IsVisible; } }
	public bool IsGameOvertMenuVisible{ get { return gameOverUI.IsVisible; } }


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
		vignette.SetActive (true);
		startScreen.Show ();
	}

	public void HideStartScreen(){
		startScreen.Hide ();
	}

	public void ShowGameOver(){
		vignette.SetActive (true);

		gameOverUI.ShowGameOverUI ();
	}

	public void HideGameOver(){
		gameOverUI.HideGameOverUI ();
	}


	public void ShowPointUI(){
		vignette.SetActive (false);
		pointsUI.Show ();
	}

	public void HidePointUI(){
		pointsUI.Hide ();
	}

	public void UpdateMarshmallows(){
		pointsUI.UpdateMarshmallows ();
	}
}
