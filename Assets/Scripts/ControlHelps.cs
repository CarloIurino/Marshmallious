using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlHelps : MonoBehaviour {
    [SerializeField]
    GameObject _arrows;
	[SerializeField]
	GameObject _controlsHelp;

	private bool _isEnabled;

	void Awake(){
        // Nascondo gli aiuti
        _arrows.SetActive(false);

        _isEnabled = false;

        _controlsHelp.SetActive(false);
    }

    void Start(){

        // Registro gli eventi

		HamsterController.Instance.GetComponent<HamsterMovement> ().OnMove += OnMove;
		GameController.Instance.OnGameStart += OnGameStart;
		GameController.Instance.OnGameOver += OnGameOver;
		GameController.Instance.OnPauseGame += OnPauseGame;
		GameController.Instance.OnUnpauseGame += OnUnpauseGame;
	}

    // Mostro gli aiuti per i controlli
    // Viene visualizzato il messaggio e le frecce iniziano a lampeggiare
	void Blink(){
		_controlsHelp.SetActive (true);
		StartCoroutine ("BlinkCoroutine");
	}


    // Coroutine ricorsiva per far lampeggiare le frecce
	IEnumerator BlinkCoroutine(){
        _arrows.SetActive(true);
		yield return new WaitForSeconds (0.6f);
        _arrows.SetActive(false);
        yield return new WaitForSeconds (0.4f);
		StartCoroutine ("BlinkCoroutine");
	}


  


    // Una volta che il giocatore comanda il personaggio, disabilito gli aiuti
    public void Disable(){
		StopAllCoroutines ();
        _arrows.SetActive(false);
        _controlsHelp.SetActive(false);
    }


    // Eseguo una piccola animazione, le frecce oscillano verticalmente
	void Update(){
		Vector3 position = _arrows.transform.position;
		position.y = position.y + (Mathf.PingPong (Time.time/2, 2f) - 1f)*10f*Time.deltaTime;
		_arrows.transform.position = position;
	}


    #region Callbacks

    // Quando il gioco comincia visualizzo gli aiuti
    void OnGameStart(){
		if (GameController.Instance.IsFirstStart || _isEnabled) {
			_isEnabled = true;
			Blink ();
		}
	}

    // Appena il giocatore muove il personaggio disabilito gli aiuti
	void OnMove(){
		_isEnabled = false;
		Disable ();
	}

    
    // Se il personaggio muore mentre gli aiuti sono ancora attivi, nascondo gli aiuti
	void OnGameOver(){
		Disable ();
	}


    // Disabilito gli aiuti se va in pausa
	void OnPauseGame(){
		Disable ();
	}


    // Al riprendere del gioco, se gli aiuti erano attivi, li riattivo
	void OnUnpauseGame(){
		if (_isEnabled)
			Blink ();
	}

    #endregion

    void OnDestroy(){
		HamsterController.Instance.GetComponent<HamsterMovement> ().OnMove -= OnMove;
		GameController.Instance.OnGameStart -= OnGameStart;
		GameController.Instance.OnGameOver -= OnGameOver;
		GameController.Instance.OnPauseGame -= OnPauseGame;
		GameController.Instance.OnUnpauseGame -= OnUnpauseGame;
	}
}
