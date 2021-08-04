using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalForse;
    public float yVelosity;
    public Vector2 jumpDirection;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        yVelosity = rb.velocity.y;
        rb.velocity = new Vector2(horizontalSpeed, yVelosity);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(horizontalSpeed, 0);
            rb.AddForce(jumpDirection * verticalForse, ForceMode2D.Impulse);
        }
        
    }
    void Update()
    {

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            horizontalSpeed *= -1;
            rb.velocity = new Vector2(horizontalSpeed, 0);
            rb.AddForce(jumpDirection*4, ForceMode2D.Impulse);
        }
    }
}
