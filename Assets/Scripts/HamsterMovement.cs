using UnityEngine;
using System.Collections;

public class HamsterMovement : MonoBehaviour {

	[SerializeField]
	float speed;

	HamsterAnimation hamsterAnimation;

	void Awake () {
		hamsterAnimation = GetComponent<HamsterAnimation> ();
	}
	
	void Update () {
		Movement ();
	}

	void Movement(){

		float h = Input.GetAxis ("Horizontal");

		if (IsOnScreenBorder (h)) {
			hamsterAnimation.StopWalkAnimation ();
			return;
		}


		transform.position += Vector3.right * h * speed * Time.deltaTime;

		if (h != 0 && !HamsterController.Instance.Died) {
			hamsterAnimation.WalkAnimation ();

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
