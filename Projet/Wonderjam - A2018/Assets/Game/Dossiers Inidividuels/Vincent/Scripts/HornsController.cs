using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornsController : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] sprites;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        spriteRenderer.sprite = sprites[MutationManager.Instance.DashMutation.CurrentLevel];
	}
}
