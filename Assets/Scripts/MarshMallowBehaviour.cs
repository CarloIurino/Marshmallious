using UnityEngine;
using System.Collections;

public class MarshMallowBehaviour : MonoBehaviour {
	Rigidbody rb;
	Vector3 rotationDirection;

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
		rb.angularVelocity = rotationDirection * 4;
	}
}
