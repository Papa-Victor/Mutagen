using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestScript_William : MonoBehaviour
{

    [SerializeField]
    private SceneInfo gameSceneInfo;


    // Use this for initialization
    void Start()
    {
        
        Debug.Log(MutationManager.Instance.AOEMutation.Description.mutationLevels);
    }
}