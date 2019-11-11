using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript_Gabriel : MonoBehaviour {

    [SerializeField]
    private SceneInfo gameSceneInfo;

	// Use this for initialization
	void Start () {
        Scenes.Load(gameSceneInfo);
	}
}
