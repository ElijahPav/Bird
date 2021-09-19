using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CandyController : MonoBehaviour
{
    [SerializeField] private GameObject candyPrefab;
    private Vector3 defaultPosition = new Vector3(-2.15f, 3.82f, 0);
    private const int wallSize = 10;
    private const float DistanceBetweenCandies = 0.77f;
    private int direction = 1;
    private GameObject candy;

    private void Start()
    {
        candy = Instantiate(candyPrefab, transform);
        SetCandyDefault();
        GameController.Instance.Bird.birdDeath += SetCandyDefault;
        GameController.Instance.Bird.birdRebound += SetCandy;
        
    }

    private void OnDestroy()
    {
        GameController.Instance.Bird.birdDeath -= SetCandyDefault;
        GameController.Instance.Bird.birdRebound -= SetCandy;
       
        
    }

    private void SetCandyDefault()
    {
        candy.SetActive((false));
        candy.transform.position = defaultPosition;
        direction = 1;
    }

    private void SetCandy()
    {
        if (!candy.activeSelf)
        {
            int position = Random.Range(0, wallSize);
            candy.transform.position = new Vector3(defaultPosition.x * direction, defaultPosition.y - position * DistanceBetweenCandies);
            candy.SetActive(true);
        }

        direction *= -1;
    }
}