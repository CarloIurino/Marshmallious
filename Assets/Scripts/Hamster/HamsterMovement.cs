using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Muove il personaggio e lo blocca se è vicino il bordo della scena
/// </summary>

public class HamsterMovement : MonoBehaviour {

    // Evento lanciato quando il personaggio inizia a muoversi
	public Action OnMove;

	[SerializeField]
	float _speed;
	[SerializeField]
	MobileController _mobileController;

	HamsterAnimation _hamsterAnimation;

	void Awake () {
		_hamsterAnimation = GetComponent<HamsterAnimation> ();
	}
	
	void Update () {
		Movement ();
	}

	void Movement(){

		#if UNITY_ANDROID
        
		float h = mobileController.GetHorizontal();

		#else

		float h = Input.GetAxis ("Horizontal");

		#endif


        // Quando è sul bordo della visuale
		if (IsOnScreenBorder (h)) {
			return;
		}

        // Muove il personaggio in orrizontale
		transform.position += Vector3.right * h * _speed * Time.deltaTime;

        // Controllo che non sia morto
		if (h != 0 && !HamsterController.Instance.IsDied) {
			_hamsterAnimation.WalkAnimation ();

			if (OnMove != null)
				OnMove ();
		} else {
			_hamsterAnimation.StopWalkAnimation ();
		}

	}


    // Metodo che verifica se il personaggio è al limite della visuale
	bool IsOnScreenBorder(float h){
		if (h < 0) {
			return Camera.main.WorldToScreenPoint (transform.position).x < 100;
		} else {
			return Camera.main.WorldToScreenPoint (transform.position).x > Screen.width-100;
		}
	}

	public void Reset(){
		_hamsterAnimation.IdleAnimation ();
	}
}
