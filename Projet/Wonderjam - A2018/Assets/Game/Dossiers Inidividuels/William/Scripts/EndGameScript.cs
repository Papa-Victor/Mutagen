using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class EndGameScript : MonoBehaviour {

    [SerializeField]
    private float timeBeforeNewGame = 7;

    private Action onComplete;

    [SerializeField]
    private GameObject textMesh;
	// Use this for initialization
	void Start () {
        TextMeshProUGUI[] textMeshes = GetComponentsInChildren<TextMeshProUGUI>();

        foreach(TextMeshProUGUI textMesh in textMeshes)
        {
            textMesh.DOFade(0,0);
            textMesh.DOFade(1, timeBeforeNewGame-2);
        }

        this.DelayedCall(() =>
        {
            onComplete();
        }, timeBeforeNewGame);
	}
	
	void Update () {
		
	}

    public void Init(Action onComplete)
    {
        this.onComplete = onComplete;
    }
}
