using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

    [SerializeField]
    private SceneInfo gameSceneInfo;

    [SerializeField]
    private float timeToChangeScene = 5;

	// Use this for initialization
	void Start () {
        this.DelayedCall(() =>
        {
            Scenes.Load(gameSceneInfo);
        }, timeToChangeScene);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
