using UnityEngine;
using Random = UnityEngine.Random;

public class SpikesController : MonoBehaviour
{
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private AnimationCurve spikes;

    //private int spikeIndex;
    //[SerializeField] private int spikeAmount;
    private const int wallSize = 11;
    private const float DistanceBetweenSpikes = 0.77f;
    public Vector3 defaultPosition = new Vector3(2.6f, 3.82f, 0);
    public Vector3 defaultRotation = new Vector3(0, 0, 90);
    private float additionalValueForSpikesPosition = 0.2f;


    private void Start()
    {
        ObjectPooler.Instance.AddSpike(spikePrefab, 7);
        SetSpikesToDefault();
        GameController.Instance.Bird.birdDeath += SetSpikesToDefault;
        //UIController.Instance.startButton.StartButtonClick += SetSpikes;
        GameController.Instance.Bird.birdRebound += SetSpikes;
        defaultPosition = new Vector3(Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x-additionalValueForSpikesPosition, defaultPosition.y,0);
    }

    private void OnDestroy()
    {
        GameController.Instance.Bird.birdDeath -= SetSpikesToDefault;
        //UIController.Instance.startButton.StartButtonClick -= SetSpikes;
        GameController.Instance.Bird.birdRebound -= SetSpikes;
    }


    private void SetSpikesToDefault()
    {
       ObjectPooler.Instance.SetSpikesToDefault(defaultRotation,defaultPosition);
    }

    public void SetSpikes()
    {
        foreach (var spikeD in ObjectPooler.Instance.GetAllPooledSpikes())
        {
            spikeD.SetActive(false);
        }
        int spikeAmount = Mathf.RoundToInt(spikes.Evaluate(GameController.Instance.score));
        GameObject spike;
        Vector3 previousPosition = new Vector3(defaultPosition.x * GameController.Instance.Bird.direction, defaultPosition.y, 0);
        int position;
        int tempSize = wallSize;
        Vector3 tempRotation = defaultRotation * GameController.Instance.Bird.direction;

        for (int i = 0; i < spikeAmount; i++)
        {
            spike = ObjectPooler.Instance.GetPooledSpike();
            position = Random.Range(0, tempSize / 2 - 1);
            spike.transform.position = previousPosition - new Vector3(0, position * DistanceBetweenSpikes, 0);
            spike.transform.eulerAngles = tempRotation; //new Vector3(0, 0, spike.transform.eulerAngles.z+180 * direction);
            spike.SetActive(true);
            previousPosition = spike.transform.position - new Vector3(0, DistanceBetweenSpikes, 0);
            tempSize -= position;
        }
    }
}