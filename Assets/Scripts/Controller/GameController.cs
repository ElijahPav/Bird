using UnityEngine;

public class GameController : SingletonMono<GameController>
{
    [SerializeField] private GameObject wallLeft;
    [SerializeField] private GameObject wallRight;
    [SerializeField] public Color[] backColor=new Color[7];
    [SerializeField] public AnimationCurve coloeCurve;
    private float additionalValueForWallPosition = 0.25f;
    public BirdBehaviour Bird;
    public int score;
    private int currentCandiesAmount;
    private Camera camera;


    private void Start()
    {
        camera=Camera.main;
        UIController.Instance.StartButton.StartButtonClick += StartButtonClick;
        Bird.birdRebound += AddPoint;
        Bird.birdRebound += ColorChange;
        Bird.birdCandy += CandyCollect;
        Bird.birdDeath += SaveCandys;
        Bird.birdDeath += CompareBestScore;
        Vector2 middleLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
        Vector2 middleRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.5f));
        wallLeft.transform.position = new Vector2(middleLeft.x - additionalValueForWallPosition, middleLeft.y);
        wallRight.transform.position = new Vector2(middleRight.x + additionalValueForWallPosition, middleRight.y);
    }


    protected void OnDestroy()
    {
        SaveCandys();
        CompareBestScore();
        UIController.Instance.StartButton.StartButtonClick -= StartButtonClick;
        Bird.birdRebound -= AddPoint;
        Bird.birdRebound -= ColorChange;
        Bird.birdCandy -= CandyCollect;
        Bird.birdDeath -= SaveCandys;
        Bird.birdDeath -= CompareBestScore;
    }

    private void AddPoint()
    {
        score++;
    }

    private void ColorChange()
    {
        int colorIndex = Mathf.RoundToInt(coloeCurve.Evaluate(score));
        camera.backgroundColor = backColor[colorIndex];
    }

    private void CandyCollect()
    {
        currentCandiesAmount++;
    }

    private void SaveCandys()
    {
        PlayerPrefsController.Instance.UpdateCandiesCount(currentCandiesAmount);
    }

    private void CompareBestScore()
    {
        PlayerPrefsController.Instance.UpdateBestScore(score);
    }

    public void StartButtonClick()
    {
        camera.backgroundColor = backColor[0];
        score = 0;
        currentCandiesAmount = 0;
        Bird.Reborn();
    }
}