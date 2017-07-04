using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MarshmallowPooling))]
public class MarshmallowsManager : MonoBehaviour {
	static MarshmallowsManager _instance;
	public static MarshmallowsManager Instance{ get{ return _instance;}}

    public MarshmallowPooling pooling;

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
        Marshmallow marshmallow;

        if (Random.value > blackMarshmallowFrequency) {
            marshmallow = pooling.GetMarshmallow( MarshmallowType.Good);
        }
        else
            marshmallow = pooling.GetMarshmallow(MarshmallowType.Bad);


        int randomXPosition = FindXPosition ();

        marshmallow.transform.position = new Vector3(randomXPosition, 3, -3);
        marshmallow.transform.rotation = Random.rotation;

        marshmallow.transform.parent = transform;
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
        pooling.DestroyLists();
	}




}
