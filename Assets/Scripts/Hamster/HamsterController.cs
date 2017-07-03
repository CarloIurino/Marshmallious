using UnityEngine;
using System.Collections;

/// <summary>
/// Gestisce gli stati del para: Attivo, Non Attivo, in Movimento, Morto
/// </summary>

[RequireComponent ( typeof ( SpriteRenderer ) )]
[RequireComponent ( typeof (HamsterMovement) ) ]
[RequireComponent ( typeof (BoxCollider) ) ]
public class HamsterController : MonoBehaviour {
	static HamsterController _instance;
	public static HamsterController Instance{ get { return _instance; } }


	SpriteRenderer _spriteRenderer;
	HamsterMovement _hamsterMovement;
	BoxCollider _boxCollider;
	HamsterAnimation _hamsterAnimation;

	Vector3 _startPosition;

	public bool IsDied{ get; set; }

	void Awake () {
		if (_instance == null)
			_instance = this;
		else
			Destroy (gameObject);


		InitOnAwake ();

	}


    // Recuper i componenti
	void InitOnAwake(){
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		_hamsterMovement = GetComponent<HamsterMovement> ();
		_boxCollider = GetComponent<BoxCollider> ();
		_hamsterAnimation = GetComponent<HamsterAnimation> ();

		_startPosition = transform.position;
	}


    // Abilita il personaggio
	public void EnableHamster(){
		_boxCollider.enabled = true;
		_spriteRenderer.enabled = true;
		_hamsterMovement.enabled = true;
	}
	

    // Disabilita e nasconde
	public void DisableHamster(){
		_hamsterAnimation.StopDieAnimation ();
		_boxCollider.enabled = false;
		_spriteRenderer.enabled = false;
		_hamsterMovement.enabled = false;
	}


    // Disabilita i controlli (es. quando muore)
	public void StopMovement(){
		_boxCollider.enabled = false;
		_hamsterMovement.enabled = false;
	}


    // Il personaggio muore. Fa partire la coroutine che disabilita i controlli e abilità l'animazione 
	public void Die(){
		StartCoroutine (DieRoutine ());
	}



	IEnumerator DieRoutine(){

		IsDied = true;

		yield return new WaitForSeconds (0.6f);

        // Disabilito i controlli
		StopMovement ();

        // Faccio partire l'animazione della morte del personaggio
		_hamsterAnimation.StopWalkAnimation ();
		_hamsterAnimation.DieAnimation ();
	}


    // Ripristino per una nuova partita
	public void Reset(){
		IsDied = false;
		transform.position = _startPosition;
		_hamsterMovement.Reset ();
	}
}
