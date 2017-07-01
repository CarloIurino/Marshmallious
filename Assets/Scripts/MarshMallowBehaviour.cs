using UnityEngine;
using System.Collections;

public class MarshMallowBehaviour : MonoBehaviour {
	Rigidbody rb;
	Vector3 rotationDirection;

	float v;

	bool isTarget;

	[SerializeField]
	int lifeTime;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rotationDirection = Random.insideUnitSphere;

		Destroy (gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isTarget) {
			Vector3 position = rb.position;
			position.x = Mathf.SmoothDamp(position.x, HamsterController.Instance.transform.position.x, ref v, 0.2f)+0.02f;
			rb.position = position;
		}

		rb.angularVelocity = rotationDirection * 4;
	}

	public void SetAsTarget(){
		GetComponent<MeshRenderer> ().material.color = Color.red;
		isTarget = true;
	}
}
