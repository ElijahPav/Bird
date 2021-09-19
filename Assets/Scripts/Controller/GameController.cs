using UnityEngine;

public class GameController : SingletonMono<GameController>
{
    public BirdBehaviour Bird;
    public int Points = 0;
    private int candys;

    private void Start()
    {
        candys = PlayerPrefs.GetInt("Candys");
        UIController.Instance.StartButton.StartButtonClick += StartButtonClick;
        Bird.birdRebound += AddPoint;
        Bird.birdCandy += CandyCollect;
        Bird.birdDeath += SaveCandys;
    }


    protected void OnDestroy()
    {
        UIController.Instance.StartButton.StartButtonClick -= StartButtonClick;
        Bird.birdRebound -= AddPoint;
        Bird.birdCandy -= CandyCollect;
        Bird.birdDeath -= SaveCandys;
    }

    private void AddPoint()
    {
        Points++;
    }

    private void CandyCollect()
    {
        candys++;
    }

    private void SaveCandys()
    {
        PlayerPrefs.SetInt("Candys",candys);
    }

    public void StartButtonClick()
    {
        Points = 0;
        Bird.Reborn();
    }
}