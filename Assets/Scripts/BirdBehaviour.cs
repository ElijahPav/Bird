using System;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalForce;
    public event Action birdDeath;
    public event Action birdRebound;
    public event Action birdCandy;
    public float direction = 1;
    private Rigidbody2D rb;
    private const string wallTag = "Wall";
    private const string spikeTag = "Spike";
    private const string candyTag = "Candy";


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Reborn();
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
            GetComponent<BirdAnimationController>().makeFlap();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(wallTag))
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180 * direction, 0);
            direction *= -1;
            rb.velocity = new Vector2(horizontalSpeed * direction, rb.velocity.y);
            birdRebound?.Invoke();
        }

        if (collision.gameObject.CompareTag(spikeTag))
        {
            birdDeath?.Invoke();
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(candyTag))
        {
            birdCandy?.Invoke();
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
        birdDeath?.Invoke();
    }

    public void Reborn()
    {
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        direction = 1;
        gameObject.SetActive(true);
    }
}