using UnityEngine;
using System.Collections;

public class HamsterEat : MonoBehaviour {

	HamsterAnimation hamsterAnimation;

    // Effetti particellari delle briciole quando vengono magiati i marshmallows
	[SerializeField]
	ParticleSystem _crumbs;
	[SerializeField]
	ParticleSystem _badCrumbs;

    // E' il collider che rileva un marshmallow vicino la bocca
	BoxCollider _boxCollider;


    // Il marshmallow che verrà inghiottito
	Marshmallow _targetMarshmallow;

	void Awake(){
		hamsterAnimation = GetComponent<HamsterAnimation> ();
		_boxCollider = GetComponent<BoxCollider> ();
	}

	
    // Quando un marshmallow è vicino alla bocca
	void OnTriggerEnter( Collider c ){
		if (c.CompareTag ("Marshmallow") || c.CompareTag("BlackMarshmallow") ) {
			_targetMarshmallow = c.gameObject.GetComponent<Marshmallow>();

            // Il marshmallow viene impostato come target. Viene cambiato il colore e si avvicina alla bocca
			_targetMarshmallow.SetAsTarget ();

			PrepareEat ();
		}
	}


    // Il topolino si prepara a mangiare il marshmallow target.
    // Durante questa fase non può mangiare altro finche non mangia il target poichè il collider viene disabilitato.
    // Il target può essere a una certa distanza dalla bocca, quindi si avvicina finchè non viene inghiottito
	void PrepareEat(){
		if (HamsterController.Instance.IsDied)
			return;
		
		_boxCollider.enabled = false;

        // Viene lanciata l'animazione mangia che contiene l'evento Eat() quando la bocca viene spalancata
		hamsterAnimation.EatAnimation ();
	}


    // Mangia il marshmallow
    // E' chiamato da un evento nell'animazione, quando la bocca è spalancata
	public void Eat(){
		if (_targetMarshmallow == null)
			return;

		if (HamsterController.Instance.IsDied)
			return;

        // Eseguo il suono che mangia
		SoundManager.Instance.EatingSound ();


        // Controllo se è un marshmallow buono o brutto
		if (_targetMarshmallow.CompareTag ("BlackMarshmallow")) {

            // Marshmallow brutto, fa addormentare il topolino
			GameController.Instance.GameOver ();
			_badCrumbs.Play ();
		}
		else if (_targetMarshmallow.CompareTag ("Marshmallow")){

            // Marshmallow buono

            // Segnalo al gameController che è stato magiato un marshmallow buono, si occuperà di aumentare i punti e altro
            GameController.Instance.TakeMarshMallow();

            // Eseguo effetto particellare
			_crumbs.Play ();
		}

        // Disattivo il marshmallow mangiato
        _targetMarshmallow.gameObject.SetActive(false);
			
	}

    // Riabilito il collider per mangiare altri marshmallow
	public void FinishEat(){
		_boxCollider.enabled = true;
		_targetMarshmallow = null;
	}

}
