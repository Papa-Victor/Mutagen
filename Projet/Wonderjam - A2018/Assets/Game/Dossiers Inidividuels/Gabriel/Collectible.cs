using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour {

    public float m_LifeTime = 8.0f;
    public float m_timeFade = 2.0f;

    float m_tempsReste;

	// Use this for initialization
	void Start () {
        m_tempsReste = m_LifeTime;
        Invoke("FadeFin", (m_LifeTime - m_timeFade));
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_tempsReste -= Time.deltaTime;
        if (m_tempsReste < 0.0f)
            Destroy(gameObject);//Debug.Log("meur");
    }

    void FadeFin()
    {
        Sequence seq = DOTween.Sequence();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        seq.Append(sr.DOFade(0.0f, m_timeFade));
        seq.Join(transform.DOScale(1.5f, m_timeFade));

        seq.SetEase(Ease.InOutFlash, 9);

        //seq.Play();
    }
}
