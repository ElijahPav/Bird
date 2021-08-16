using System;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalForce;
    [SerializeField] private float verticalForceAfterWallCollision;
    public event Action birdDeth;
    private float direction = 1;
    private Rigidbody2D rb;
    private Transform tr;
    private const string wallTag = "Wall";
    private const string spikeTag = "Spike";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = transform;
    }

    private void FixedUpdate()
    {
        //todo: подумать насчет логики передвижения без rigidbody
        rb.velocity = new Vector2(horizontalSpeed * direction, rb.velocity.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(horizontalSpeed * direction, 0);
            rb.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(wallTag))
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180 * direction, 0);
            direction *= -1;
            rb.velocity = new Vector2(horizontalSpeed * direction, 0);
            rb.AddForce(Vector2.up * verticalForceAfterWallCollision, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag(spikeTag))
        {
            birdDeth?.Invoke();
            Deth();
            //GameController.Instance.BirdDeth();
            Debug.Log("Шип");
        }
    }

    public void Deth()
    {
        gameObject.SetActive(false);
        birdDeth?.Invoke();
    }

    public void Reborn()
    {
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        direction = 1;
        gameObject.SetActive(true);
    }
}