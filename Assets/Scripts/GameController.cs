using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class GameController : SingletonMono<GameController>
{
     private StartButton startButton;
    [SerializeField] private BirdBehaviour bird;

    private void Start()
    {
        startButton = UIController.Instance.startButton;    
        startButton.StartButtonClick += StartButtonClick;
    }

    public void StartButtonClick()
    {
        bird.Reborn();
    }
}