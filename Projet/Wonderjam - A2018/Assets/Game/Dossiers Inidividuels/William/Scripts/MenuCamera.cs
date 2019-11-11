using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MenuCamera : MonoBehaviour {

    private List<Transform> travelPoints;

	void Start () {

        GameObject travelPointsObject = GameObject.FindGameObjectWithTag("TravelPoints");

        travelPoints = new List<Transform>();

        foreach (Transform travelPoint in travelPointsObject.transform)
        {
            travelPoints.Add(travelPoint);
        }
        MenuAnimation();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
        {
            ChangeTravelPoints();
        }
	}

    private void ChangeTravelPoints()
    {
        int randomNumber = UnityEngine.Random.Range(0, travelPoints.Capacity);
        Vector3 randomPosition = new Vector3(travelPoints[randomNumber].position.x, travelPoints[randomNumber].position.y, transform.position.z);

        if (randomPosition != transform.position)
            transform.position = new Vector3(travelPoints[randomNumber].position.x, travelPoints[randomNumber].position.y, transform.position.z);
        else
            ChangeTravelPoints();
    }

    private void MenuAnimation()
    {
        int randomNumber = UnityEngine.Random.Range(0, travelPoints.Capacity);
        Vector3 randomPosition = new Vector3(travelPoints[randomNumber].position.x, travelPoints[randomNumber].position.y, transform.position.z);

        if (transform.position != randomPosition)
        {
            Sequence menuSequence = DOTween.Sequence();
            menuSequence.Append(transform.DOMove(randomPosition, 5f));
            menuSequence.SetEase(Ease.Linear);
            menuSequence.onComplete += MenuAnimation;
            menuSequence.Play();
        }
        else
            MenuAnimation();
    }
}
