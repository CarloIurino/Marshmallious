using UnityEngine;
using System.Collections;

/// <summary>
/// Classe del marshmallow, definisce il tipo, buono o brutto e quando viene istanziato comincia a ruotare.
/// Quando diviene il marshmallow target( che verrà mangiato), si avvicina alla bocca del personaggio
/// I marshmallow nella scena sono gestiti dal sistema di pooling definito nella classe MarshmallowPooling
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Marshmallow : MonoBehaviour {
    [SerializeField] private float _angularVelocityScaler = 5;
    [SerializeField] private MarshmallowType type;


    // Quando sta per essere mangiato
	private bool _isTarget;

    // il colore che assume quando sta per essere mangiato
    private Color _targetColor = Color.magenta;

	void OnEnable () {
        _isTarget = false;
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * _angularVelocityScaler;
	}
	

	void FixedUpdate () {

        // Quando sta per essere mangiato lo avvicino alla bocca del personaggio
		if (_isTarget) {
			Vector3 position = transform.position;
			position.x = Mathf.Lerp(position.x, HamsterController.Instance.transform.position.x, Time.deltaTime*10)+0.02f;
			transform.position = position;
		}
	}



    // Setta come target e cambia colore
    public void SetAsTarget(){
        // Cambio il colore quando è prossimo ad essere mangiato
		GetComponent<MeshRenderer> ().material.color = _targetColor;

		_isTarget = true;
	}


    //Setta il colore che assume quando è target
    public void SettargetColor(Color color) {
        _targetColor = color;
    }


    // Quando collide con il trigger che si trova sotto la visuale, disattivo il marshmallow. 
    //L'object pooling si occuperà di gestire i marshmallow nella scena
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(G.DEACTIVATION_TRIGGER)) {
            gameObject.SetActive(false);
        }
    }
}


// Tipo di marshmallow, buono da mangiare o non buono
public enum MarshmallowType {
    Good,
    Bad
}