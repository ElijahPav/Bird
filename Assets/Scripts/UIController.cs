using System;
using System.Collections;
using System.Collections.Generic;
//todo: неймспейсы
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : SingletonMono<UIController>
{
    //todo: если поле публичное, то оно должно быть с большой буквы
    //в идеале сделать её через public get; private set
     public StartButton startButton;
    [SerializeField] private BirdBehaviour bird;

    private void Start()
    {
        startButton.StartButtonClick += StartButtonClick;
        bird.birdDeath += BirdDeth;
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