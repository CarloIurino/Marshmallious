using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig", order = 0)]
public class GameConfigScriptableObject : ScriptableObject {

    public GameObject goodMarshmallowPrefab;
    public GameObject badMarshmallowPrefab;
    public Color targetMarshmallowColor = Color.red;
	
}
