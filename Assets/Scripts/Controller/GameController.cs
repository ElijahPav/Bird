using UnityEngine;

public class GameController : SingletonMono<GameController>
{
    [SerializeField] private GameObject wallLeft;
    [SerializeField] private GameObject wallRight;
    private float additionalValueForWallPosition = 0.25f;
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
        Vector2 middleLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
        Vector2 middleRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.5f));
        wallLeft.transform.position = new Vector2(middleLeft.x - additionalValueForWallPosition, middleLeft.y);
        wallRight.transform.position = new Vector2(middleRight.x + additionalValueForWallPosition, middleRight.y);
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
        PlayerPrefs.SetInt("Candys", candys);
    }

    public void StartButtonClick()
    {
        Points = 0;
        Bird.Reborn();
    }
}