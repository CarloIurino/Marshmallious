using UnityEngine;
using System.Collections;
using System;

public class HamsterMovement : MonoBehaviour {
	public Action OnMove;

	[SerializeField]
	float speed;
	[SerializeField]
	MobileController mobileController;

	HamsterAnimation hamsterAnimation;

	void Awake () {
		hamsterAnimation = GetComponent<HamsterAnimation> ();
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

		if (IsOnScreenBorder (h)) {
			hamsterAnimation.StopWalkAnimation ();
			return;
		}


		transform.position += Vector3.right * h * speed * Time.deltaTime;

		if (h != 0 && !HamsterController.Instance.Died) {
			hamsterAnimation.WalkAnimation ();

			if (OnMove != null)
				OnMove ();
		} else {
			hamsterAnimation.StopWalkAnimation ();
		}

	}


	bool IsOnScreenBorder(float h){
		if (h < 0) {
			return Camera.main.WorldToScreenPoint (transform.position).x < 100;
		} else {
			return Camera.main.WorldToScreenPoint (transform.position).x > Screen.width-100;
		}
	}

	public void Reset(){
		hamsterAnimation.IdleAnimation ();
	}
}
