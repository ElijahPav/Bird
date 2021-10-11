using UnityEngine;

public class CandyBehaviour : MonoBehaviour
{
    //private const string birdTag = "Bird";

    private void Start()
    {
        GameController.Instance.Bird.birdCandy += CandyCollect;
    }

    private void CandyCollect()
    {
        gameObject.SetActive(false);
    }
}