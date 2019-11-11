using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript_Emile : MonoBehaviour {
    [SerializeField]
    SceneInfo gameSceneInfo;

	// Use this for initialization
	void Start () {
        Scenes.Load(gameSceneInfo);
	}
	
	
	void Update () {
		
	}
}
