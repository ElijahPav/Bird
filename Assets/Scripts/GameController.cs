using System;
//todo: ненужные неймспейсы нужно убирать
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class GameController : SingletonMono<GameController>
{
    public BirdBehaviour Bird;

    private void Start()
    {
        UIController.Instance.startButton.StartButtonClick += StartButtonClick;
    }

    protected void OnDestroy()
    {
        UIController.Instance.startButton.StartButtonClick -= StartButtonClick;
    }

    public void StartButtonClick()
    {
        Bird.Reborn();
    }
}