using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class GameController : SingletonMono<GameController>
{
    [SerializeField] private StartButton startButton;
    [SerializeField] private BirdBehaviour bird;

    private void Start()
    {
        startButton.StartButtonClick += StartButtonClick;
    }

    public void StartButtonClick()
    {
        bird.Reborn();
    }
}