using TMPro;
using UnityEngine;

public class PointCounterController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointCounter;
    [SerializeField] public Color32[] textColor = new Color32[7];

    private void Start()
    {
        GameController.Instance.Bird.birdRebound += CounterUpdate;
        GameController.Instance.Bird.birdDeath += BirdDeath;
        pointCounter.text = null;
    }

    private void OnDestroy()
    {
        GameController.Instance.Bird.birdRebound -= CounterUpdate;
        GameController.Instance.Bird.birdDeath -= BirdDeath;
    }

    private void CounterUpdate()
    {
        int colorIndex = Mathf.RoundToInt(GameController.Instance.coloeCurve.Evaluate(GameController.Instance.score));
        pointCounter.faceColor = textColor[colorIndex];
        pointCounter.text = GameController.Instance.score.ToString();
    }

    private void BirdDeath()
    {
        pointCounter.text = null;
    }
}