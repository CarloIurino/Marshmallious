using UnityEngine;
using System.Collections;

public class MarshmallowController : MonoBehaviour {

	void Start () {
		StartCoroutine (SpawnMarshMallowRoutine ());
		StartCoroutine (MarshmallowStorm());
	}
	
	IEnumerator SpawnMarshMallowRoutine(){

		SpawnMarshmallow ();
		yield return new WaitForSeconds (3);

		StartCoroutine (SpawnMarshMallowRoutine ());
	}

	void SpawnMarshmallow(){
		string randomMarshmallow;

		if (Random.value > 0.05f) {
			randomMarshmallow = "MarshMallow";
		}
		else
			randomMarshmallow = "BlackMarshMallow";
		
		Instantiate (Resources.Load (randomMarshmallow), new Vector3 (Random.Range (-6, 7), 3, -3), Random.rotation);
	}

	IEnumerator MarshmallowStorm(){
		float randomStormWait = Random.value * 5;

		yield return new WaitForSeconds (randomStormWait);

		for (int i = 0; i < 100; i++) {
			SpawnMarshmallow ();
			yield return new WaitForSeconds (0.2f);
		}

		StartCoroutine (MarshmallowStorm());

	}
}
