using UnityEngine;
using System.Collections;

public class HamsterEat : MonoBehaviour {

	HamsterAnimation hamsterAnimation;

	[SerializeField]
	ParticleSystem crumbs;
	[SerializeField]
	ParticleSystem badCrumbs;

	BoxCollider boxCollider;

	MarshMallowBehaviour target;

	void Awake(){
		hamsterAnimation = GetComponent<HamsterAnimation> ();
		boxCollider = GetComponent<BoxCollider> ();
	}

	
	void OnTriggerEnter( Collider c ){
		if (c.CompareTag ("Marshmallow") || c.CompareTag("BlackMarshmallow") ) {
			target = c.gameObject.GetComponent<MarshMallowBehaviour>();
			target.SetAsTarget ();
			PrepareEat ();
		}
	}

	void PrepareEat(){
		if (HamsterController.Instance.Died)
			return;
		
		boxCollider.enabled = false;
		hamsterAnimation.EatAnimation ();
	}

	public void Eat(){
		if (target == null)
			return;

		if (HamsterController.Instance.Died)
			return;

		SoundManager.Instance.EatingSound ();

		if (target.CompareTag ("BlackMarshmallow")) {
			GameController.Instance.GameOver ();
			badCrumbs.Play ();
		}
		else if (target.CompareTag ("Marshmallow")){
//			particles.Play ();
			GameController.Instance.TakeMarshMallow();
			crumbs.Play ();
		}

		Destroy (target.gameObject);
			
	}

	public void FinishEat(){
		boxCollider.enabled = true;
		target = null;
	}

}
