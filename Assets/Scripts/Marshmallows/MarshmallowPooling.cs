using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarshmallowPooling : MonoBehaviour {
    // I prefab dei marshmallows recuperati dai data del GameManager
    private GameObject _goodMarshmallowPrefab;
    private GameObject _badMarshmallowPrefab;

    // La lista dei marshmallow buoni
    private List<Marshmallow> _goodMarshmallow = new List<Marshmallow>();

    // La lista dei marshmallow brutti
    private List<Marshmallow> _badMarshmallow = new List<Marshmallow>();


    private void Awake() {
        // Assegno alle variabili private i prefab recuperati dal GameManager
        _goodMarshmallowPrefab = GameController.Instance.data.goodMarshmallowPrefab;
        _badMarshmallowPrefab = GameController.Instance.data.badMarshmallowPrefab;
    }


    // Distruggo tutti i marshmallow istanziati
    public void DestroyLists() {

        foreach(Marshmallow m in _goodMarshmallow) {
            Destroy(m.gameObject);
        }

        foreach(Marshmallow m in _badMarshmallow) {
            Destroy(m.gameObject);
        }

        _goodMarshmallow.Clear();
        _badMarshmallow.Clear();
    }

    public Marshmallow GetMarshmallow(MarshmallowType type) {
        List<Marshmallow> list;

        if (type == MarshmallowType.Good)
            list = _goodMarshmallow;
        else
            list = _badMarshmallow;

        foreach(Marshmallow m in list) {
            if (!m.gameObject.activeInHierarchy) {
                m.gameObject.SetActive(true);
                m.GetComponent<Rigidbody>().velocity = Vector3.zero;
                return m;
            }
        }
  
        return InstantiateMarshmallow(type);
    }


    private Marshmallow InstantiateMarshmallow(MarshmallowType type) {

        Marshmallow marshmallow;

        if (type == MarshmallowType.Good) {
            marshmallow = Instantiate(_goodMarshmallowPrefab).GetComponent<Marshmallow>();
            _goodMarshmallow.Add(marshmallow);
        }
        else {
            marshmallow = Instantiate(_badMarshmallowPrefab).GetComponent<Marshmallow>();
            _badMarshmallow.Add(marshmallow);
        }

        return marshmallow;
    }

}


