using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : SingletonMono<UIController>
{
    [SerializeField] private StartButton startButton;
    [SerializeField] private BirdBehaviour bird;

    private void Start()
    {
        startButton.StartButtonClick += StartButtonClick;
        bird.birdDeth += BirdDeth;
    }

    private void StartButtonClick()
    {
        startButton.gameObject.SetActive(false);
    }

    private void BirdDeth()
    {
        startButton.gameObject.SetActive(true);
    }
}