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

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     Debug.Log(other.transform.tag);
    //     if (other.CompareTag(birdTag))
    //     {
    //         gameObject.SetActive(false);
    //     }
    // }
}