using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class MarshMallowBehaviour : MonoBehaviour {

    float _angularVelocityScaler = 5;

	bool _isTarget;

	[SerializeField]
	int _lifeTime;

	void Start () {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * _angularVelocityScaler;
	}
	
	void FixedUpdate () {
		if (_isTarget) {
			Vector3 position = transform.position;
			position.x = Mathf.Lerp(position.x, HamsterController.Instance.transform.position.x, Time.deltaTime*10)+0.02f;
			transform.position = position;
		}
	}

	public void SetAsTarget(){
        // Cambio il colore quando è prossimo ad essere mangiato
		GetComponent<MeshRenderer> ().material.color = Color.red;

		_isTarget = true;
	}
}
