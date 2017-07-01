using UnityEngine;
using System.Collections;

public class HamsterEat : MonoBehaviour {

	HamsterAnimation hamsterAnimation;
	ParticleSystem particles;


	void Awake(){
		hamsterAnimation = GetComponent<HamsterAnimation> ();
		particles = GetComponentInChildren<ParticleSystem> ();

	}

	
	void OnTriggerEnter( Collider c ){
		if (c.CompareTag ("Marshmallow") || c.CompareTag("BlackMarshmallow") ) {
			Eat (c);
		}
	}

	void Eat(Collider c){
		hamsterAnimation.EatAnimation ();

		Destroy (c.gameObject, 0.4f);

		if (c.CompareTag ("BlackMarshmallow")) {
			GameController.Instance.GameOver ();
		}
		else {
			particles.Play ();
			GameController.Instance.TakeMarshMallow();
		}
	}

}
