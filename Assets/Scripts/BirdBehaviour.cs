using UnityEngine;
public class BirdBehaviour : MonoBehaviour
{
    [SerializeField]private float horizontalSpeed; 
    [SerializeField]private float verticalForce;
    [SerializeField] private float verticalForceAfterWallCollision;
    private float _direction= 1;
    private Rigidbody2D rb;
    private string wallTag = "Wall"; 
    
    
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //если работаешь с rigidbody, но менять вручную ускорение так себе идея
        //и я пока не знаю что предложить в данном случае, мб зафиксировать в rgb Ox и двигать прямолинейно в апдейте
        rb.velocity = new Vector2(horizontalSpeed*_direction, rb.velocity.y);
    } 
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(horizontalSpeed*_direction, 0);
            rb.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag(wallTag))
        {
            _direction *= -1;
            rb.velocity = new Vector2(horizontalSpeed*_direction, 0);
            rb.AddForce(Vector2.up*verticalForceAfterWallCollision, ForceMode2D.Impulse);
        }
    }
}
