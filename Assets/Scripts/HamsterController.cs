using UnityEngine;
using System.Collections;

[RequireComponent ( typeof ( SpriteRenderer ) )]
[RequireComponent ( typeof (HamsterMovement) ) ]
[RequireComponent ( typeof (BoxCollider) ) ]
public class HamsterController : MonoBehaviour {
	static HamsterController _instance;
	public static HamsterController Instance{ get { return _instance; } }


	SpriteRenderer spriteRenderer;
	HamsterMovement hamsterMovement;
	BoxCollider boxCollider;
	HamsterAnimation hamsterAnimation;

	Vector3 startPosition;

	public bool Died{ get; set; }

	void Awake () {
		if (_instance == null)
			_instance = this;
		else
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		InitOnAwake ();

	}

	void InitOnAwake(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
		hamsterMovement = GetComponent<HamsterMovement> ();
		boxCollider = GetComponent<BoxCollider> ();
		hamsterAnimation = GetComponent<HamsterAnimation> ();

		startPosition = transform.position;
	}

	public void EnableHamster(){
		boxCollider.enabled = true;
		spriteRenderer.enabled = true;
		hamsterMovement.enabled = true;
	}
	
	public void DisableHamster(){
		hamsterAnimation.StopDieAnimation ();
		boxCollider.enabled = false;
		spriteRenderer.enabled = false;
		hamsterMovement.enabled = false;
	}

	public void StopMovement(){
		boxCollider.enabled = false;
		hamsterMovement.enabled = false;
	}

	public void Die(){
		StartCoroutine (DieRoutine ());
	}

	IEnumerator DieRoutine(){
		Died = true;
		yield return new WaitForSeconds (0.6f);
		StopMovement ();
		hamsterAnimation.StopWalkAnimation ();
		hamsterAnimation.DieAnimation ();
	}

	public void Reset(){
		Died = false;
		transform.position = startPosition;
		hamsterMovement.Reset ();
	}
}
