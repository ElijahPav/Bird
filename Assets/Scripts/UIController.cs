using UnityEngine;
using UnityEngine.UI;

public class UIController : SingletonMono<UIController>
{
    public StartButton StartButton
    {
        get { return startButton; }
        private set { startButton = value; }
    }

    [SerializeField] private StartButton startButton;
    [SerializeField] private Text candyCounter;
    [SerializeField] private Image candyImage;
    [SerializeField] private Text pointCounter;

    private void Start()
    {
        candyCounter.text = PlayerPrefs.GetInt("Candys").ToString();
        StartButton.StartButtonClick += StartButtonClick;
        GameController.Instance.Bird.birdDeath += BirdDeth;
        GameController.Instance.Bird.birdRebound += CounterUpdate;
        candyCounter.text = null;
    }

    private void StartButtonClick()
    {
        candyCounter.gameObject.SetActive(false);
        candyImage.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(false);
    }

    private void CounterUpdate()
    {
        pointCounter.text = GameController.Instance.Points.ToString();

    }
    

    private void BirdDeth()
    {
        StartButton.gameObject.SetActive(true);
        candyImage.gameObject.SetActive(true);
        candyCounter.text = PlayerPrefs.GetInt("Candys").ToString();
        candyCounter.gameObject.SetActive(true);
        pointCounter = null;
    }
}