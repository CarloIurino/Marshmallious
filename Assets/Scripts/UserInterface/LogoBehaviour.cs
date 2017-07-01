using UnityEngine;
using System.Collections;

public class LogoBehaviour : MonoBehaviour {
	float delta = 5;

	void Update(){

		transform.rotation = Quaternion.Euler( new Vector3 (0, 0, Mathf.PingPong(delta, 10)-5 ) );
		delta += Time.deltaTime;
	}
}
