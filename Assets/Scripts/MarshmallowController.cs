using UnityEngine;
using System.Collections;

public class MarshmallowController : MonoBehaviour {
	static MarshmallowController _instance;
	public static MarshmallowController Instance{ get{ return _instance;}}

	int lastMarshmallowXPosition = int.MinValue;

	void Awake(){
		if (_instance == null) {
			_instance = this;
		} else {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	public void MenuRain(float intensity, float blackMarshmallowFrequency){
		StartCoroutine ("SpawnMarshMallowRoutine" , new object[2]{intensity, blackMarshmallowFrequency});
	}

	public void StopRain(){
		StopAllCoroutines ();
		DestroyMarshmallows ();
	}

	public void GameRain(){
		StartCoroutine( GameRainSequence(0.8f, 0.1f));
	}

	IEnumerator GameRainSequence(float intensity, float frequency){
		StopCoroutine ("SpawnMarshMallowRoutine");
		StartCoroutine("SpawnMarshMallowRoutine", new object[2]{intensity, frequency} );
		yield return new WaitForSeconds (Random.value * 10 + 12);

		if (intensity > 0.09f)
			intensity -= 0.05f;
		
		StartCoroutine( GameRainSequence(intensity, Random.Range(0.3f, 0.7f)));
	}
	
	IEnumerator SpawnMarshMallowRoutine(object [] parms){
		float intensity = (float)parms[0];
		float blackMarshmallowFrequency = (float)parms[1];

		SpawnMarshmallow (blackMarshmallowFrequency);
		yield return new WaitForSeconds (intensity);

		StartCoroutine ("SpawnMarshMallowRoutine" , new object[2]{intensity, blackMarshmallowFrequency});
	}

	void SpawnMarshmallow(float blackMarshmallowFrequency){
		string randomMarshmallow;

		if (Random.value > blackMarshmallowFrequency) {
			randomMarshmallow = "MarshMallow";
		}
		else
			randomMarshmallow = "BlackMarshMallow";

		int randomXPosition = FindXPosition ();

		GameObject m = (GameObject) Instantiate (Resources.Load (randomMarshmallow), new Vector3 (randomXPosition, 3, -3), Random.rotation);
		m.transform.parent = transform;
	}

	int FindXPosition(){
		int newXPosition = Random.Range (-7, 8);

		if (lastMarshmallowXPosition != float.MinValue) {
			
			if (lastMarshmallowXPosition == newXPosition){
				if (newXPosition == -7)
					newXPosition = -6;
				else if (newXPosition == 7)
					newXPosition = 6;
				else
					newXPosition += 1;
			}

			lastMarshmallowXPosition = newXPosition;
		} else {
			lastMarshmallowXPosition = newXPosition;
		}

		return newXPosition;
	}

	void DestroyMarshmallows(){
		Transform[] marshmallows = GetComponentsInChildren<Transform> ();

		foreach (Transform t in marshmallows) {
			if ( t != transform)
				Destroy (t.gameObject);
		}
	}


}
