using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] sprites;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            MutationManager.Instance.TentacleMutation.Upgrade();
        }
        if(MutationManager.Instance.TentacleMutation.CurrentLevel == 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
        }
        
    }
}
