using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	[SerializeField]
	Transform leftLimit;
	[SerializeField]
	Transform rightlimit;
	[SerializeField]
	float smoothTime;

	Vector3 velocity;

	Transform target;
	Vector3 targetPosition;

	void Start(){
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}


	void Update(){
		
		FollowTarget ();

	}

	void FollowTarget () {
		if (target != null) {
			targetPosition = transform.position;
			targetPosition.x = target.position.x;

			if (IsOnLimit())
				return;

			transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, smoothTime);
		}
	}

	bool IsOnLimit () {
		if (targetPosition.x < transform.position.x) {
			
			if (Camera.main.WorldToScreenPoint (leftLimit.position).x >= 0) {
				velocity = Vector3.zero;
				return true;
			} else {
				
				return false;
			}
		} else if (targetPosition.x > transform.position.x) {
			if (Camera.main.WorldToScreenPoint (rightlimit.position).x < Screen.width) {
				velocity = Vector3.zero;
				return true;
			} else{
				return false;
			}
		}

		return false;

	}

}
