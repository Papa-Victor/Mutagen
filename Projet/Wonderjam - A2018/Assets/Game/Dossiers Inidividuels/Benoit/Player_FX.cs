using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player_FX : MonoBehaviour {

 //   private SpriteRenderer sr;
 //   private Color originalColor;
 //   public Color mutationColor;
 //   public float mutationTransitionTime = 1.5f;

 //   public GameObject mutationFeedbackPrefab;
 //   private Text mutationFeedbackInstance;

 //   // Use this for initialization
 //   void Start () {
 //       sr = GetComponent<SpriteRenderer>();
 //       originalColor = sr.color;

 //       mutationFeedbackInstance = Instantiate(mutationFeedbackPrefab).GetComponentInChildren<Text>();
 //       mutationFeedbackInstance.rectTransform.position = Camera.current.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));
 //   }

 //   private void FixedUpdate()
 //   {
 //       Vector3 position = Camera.current.WorldToScreenPoint(transform.position);
 //       mutationFeedbackInstance.rectTransform.position = position;
 //   }


 //   public void MutationEffect()
 //   {
 //       Sequence mySequence = DOTween.Sequence();
 //       mySequence.Append(sr.DOColor(mutationColor, mutationTransitionTime / 2.0f));
 //       mySequence.Append(sr.DOColor(originalColor, mutationTransitionTime / 2.0f));
 //   }
 //   //this.DelayedCall(() => { SetIsStun(true); }, m_timeStun);


 //   void Update () {
		
	//}

}
