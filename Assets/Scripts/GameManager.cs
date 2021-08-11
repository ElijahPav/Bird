using UnityEngine;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private GameObject startButton;
    private GameObject player;

    private void Awake()
    {
        if (manager != null)
        {
            Debug.Log("Singleton managaer exception");
            return;
        }

        manager = this;
    }

    public void onClick()
    {
        startButton.SetActive(false);
        player = Object.Instantiate(birdPrefab);
    }

    public void PlayerDeth()
    {
        Debug.Log("Deth");
        Destroy(player);
        startButton.SetActive(true);
    }
}