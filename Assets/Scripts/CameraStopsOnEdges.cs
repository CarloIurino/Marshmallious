using UnityEngine;
using System.Collections;


/// <summary>
/// La camera segue il personaggio a destra e sinistra e si blocca ai limiti dello sfondo.
/// </summary>

public class CameraStopsOnEdges : MonoBehaviour {
    [SerializeField] Renderer _background;
	
    [SerializeField]
    private float _smoothTime;      // Quanto è veloce la camera a seguire il personaggio

    // Usata dal metodo smooth
    private Vector3 _velocity;

    // Il target da seguire con la camera
    private Transform _target;

    // I marrgini del background
    private Vector3 _leftPosition;
    private Vector3 _rightPosition;

    void Start(){
		_target = GameObject.FindGameObjectWithTag ("Player").transform;

        _leftPosition = _background.bounds.min;
        _rightPosition = _background.bounds.max;
	}


	void Update(){
		
		FollowTarget ();

	}

	void FollowTarget () {

		if (_target != null) {
            // La prossima posizione della camera
			Vector3 _targetPosition = transform.position;

            // Sposto la camera solo sull'asse x
            _targetPosition.x = _target.position.x;

            // La posizione attuale, se la camera arriva sul bordo, ripristino questa posizione
            Vector3 currentPosition = transform.position;

            transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, _smoothTime);

            // Se mi trovo al limite dello sfondo smetto di seguire il target e ripristino la posizione precedente
            if ( IsOnLimit() )
                transform.position = currentPosition;
                
		}
	}


    /// <summary>
    /// Verifica che la camera non sia arrivata sul bordo dello sfondo
    /// </summary>
    /// <returns> Vero se si è sul bordo </returns>
	bool IsOnLimit () {
			
        // Quando la posizione dei limiti di riferimento entrano nella visuale della camera
        // ritorno true, altrimenti false
		if (Camera.main.WorldToScreenPoint (_leftPosition).x >= 0 || 
            Camera.main.WorldToScreenPoint(_rightPosition).x <= Screen.width) {

			_velocity = Vector3.zero;

			return true;
		}

        return false;
	}

}
