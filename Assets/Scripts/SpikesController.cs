using UnityEngine;
using Random = UnityEngine.Random;

public class SpikesController : MonoBehaviour
{
    [SerializeField] private GameObject spikePrefab;
    private int spikeIndex;
    private int spikeAmount = 5;
    private const int wallSize = 11;
    private int tempSize;
    private const float delta = 0.77f;
    private Vector3 defaultPosition = new Vector3(2.6f, 3.82f, 0);
    private Vector3 defaulRotatin = new Vector3(0, 0, -90);
    private float direction = 1;

    private void Start()
    {
        spikeIndex = ObjectPooler.SharedInstance.AddObject(spikePrefab, spikeAmount);
        SpikesDefault();
        GameController.Instance.Bird.birdDeath += SpikesDefault;
        UIController.Instance.startButton.StartButtonClick += SetSpikes;
        GameController.Instance.Bird.birdRebound += SetSpikes;
    }

    private void OnDestroy()
    {
        GameController.Instance.Bird.birdDeath -= SpikesDefault;
        UIController.Instance.startButton.StartButtonClick -= SetSpikes;
        GameController.Instance.Bird.birdRebound -= SetSpikes;
    }


    private void SpikesDefault()
    {
        foreach (var spike in ObjectPooler.SharedInstance.GetAllPooledObjects(spikeIndex))
        {
            spike.transform.eulerAngles = defaulRotatin;
            spike.transform.position = defaultPosition;
            spike.SetActive(false);
        }

        direction = 1;
    }

    public void SetSpikes()
    {
        foreach (var spikeD in ObjectPooler.SharedInstance.GetAllPooledObjects(spikeIndex))
        {
            spikeD.SetActive(false);
        }

        GameObject spike;
        Vector3 previousPosition = new Vector3(defaultPosition.x * direction, defaultPosition.y, 0);
        int position;
        tempSize = wallSize;

        for (int i = 0; i < spikeAmount; i++)
        {
            spike = ObjectPooler.SharedInstance.GetPooledObject(spikeIndex);
            position = Random.Range(0, tempSize / 2 - 1);
            spike.transform.position = previousPosition - new Vector3(0, position * delta, 0);
            spike.transform.rotation =
                Quaternion.Euler(0, spike.transform.eulerAngles.y - 180 * direction, defaulRotatin.z);
            spike.SetActive(true);
            previousPosition = spike.transform.position - new Vector3(0, delta, 0);
            tempSize -= position;
        }

        direction *= -1;
    }
}