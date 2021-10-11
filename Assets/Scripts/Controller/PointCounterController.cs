using TMPro;
using UnityEngine;

public class PointCounterController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointCounter;

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
        pointCounter.text = GameController.Instance.score.ToString();
    }

    private void BirdDeath()
    {
        pointCounter.text = null;
    }
}