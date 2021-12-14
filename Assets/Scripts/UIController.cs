using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : SingletonMono<UIController>
{
    public StartButton StartButton
    {
        get => startButton;
        private set { startButton = value; }
    }

    [SerializeField] private StartButton startButton;
    [SerializeField] private TextMeshProUGUI candyCounter;
    [SerializeField] private Image candyImage;
    [SerializeField] private TextMeshProUGUI bestScoreLog;
    [SerializeField] private TextMeshProUGUI bestScore;

    private void Start()
    {
        bestScore.text = PlayerPrefsController.Instance.GetBestScore().ToString();
        candyCounter.text = PlayerPrefsController.Instance.GetCandiesCount().ToString();
        StartButton.StartButtonClick += StartButtonClick;
        GameController.Instance.Bird.birdDeath += BirdDeath;
    }

    private void StartButtonClick()
    {
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    private void BirdDeath()
    {
        bestScore.text = PlayerPrefsController.Instance.GetBestScore().ToString();
        candyCounter.text = PlayerPrefsController.Instance.GetCandiesCount().ToString();
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}