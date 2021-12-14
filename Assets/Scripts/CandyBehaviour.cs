using UnityEngine;

public class CandyBehaviour : MonoBehaviour
{
    private void Start()
    {
        GameController.Instance.Bird.birdCandy += CandyCollect;
    }

    private void CandyCollect()
    {
        gameObject.SetActive(false);
    }
}