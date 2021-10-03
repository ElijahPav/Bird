using TMPro;
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
    [SerializeField] private TextMeshProUGUI candyCounter;

    [SerializeField] private Image candyImage;

    private void Start()
    {
        candyCounter.text = PlayerPrefs.GetInt("Candys").ToString();
        StartButton.StartButtonClick += StartButtonClick;
        GameController.Instance.Bird.birdDeath += BirdDeth;
    }

    private void StartButtonClick()
    {
        candyCounter.gameObject.SetActive(false);
        candyImage.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(false);
    }


    private void BirdDeth()
    {
        StartButton.gameObject.SetActive(true);
        candyImage.gameObject.SetActive(true);
        candyCounter.text = PlayerPrefs.GetInt("Candys").ToString();
        candyCounter.gameObject.SetActive(true);
    }
}