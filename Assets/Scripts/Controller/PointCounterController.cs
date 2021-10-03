using TMPro;
using UnityEngine;

public class PointCounterController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointCounter;

    private void Start()
    {
        GameController.Instance.Bird.birdRebound += CounterUpdate;
        GameController.Instance.Bird.birdDeath += BirdDeth;
        pointCounter.text = null;
    }

    private void OnDestroy()
    {
        GameController.Instance.Bird.birdRebound -= CounterUpdate;
        GameController.Instance.Bird.birdDeath -= BirdDeth;
    }

    private void CounterUpdate()
    {
        pointCounter.text = GameController.Instance.Points.ToString();
    }

    private void BirdDeth()
    {
        pointCounter.text = null;
    }
}