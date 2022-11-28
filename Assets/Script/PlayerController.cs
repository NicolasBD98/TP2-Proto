using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    public float initialSpeed;
    public float jumpHeight;
    public int jumpCount;
    public bool isDead;
    
    bool isInvincible;
    float invincibleTimer;
    float remainingTimer;
    
    public GameObject menu;

    [SerializeField] private LayerMask PlateformeLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        initialSpeed = 5;
        jumpHeight = 10;
        jumpCount = 1;
        isDead = false;

        isInvincible = false;
        invincibleTimer = 2f;
        remainingTimer = invincibleTimer;

        rb.gravityScale = 2 + this.gameObject.transform.localScale.x;
        moveSpeed = initialSpeed - this.gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {

        //Gère le mouvement
        if (!isDead)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                Move(Vector2.left, moveSpeed);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Move(Vector2.right, moveSpeed);
            }

            //stop le personnage quand on lache les inputs
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow))
                rb.velocity = new Vector2(0, rb.velocity.y);

            //s'assure que le personnage ne va pas trop vite.
            if (rb.velocity.x > moveSpeed)
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            else if (rb.velocity.x < -moveSpeed)
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            //Jump!
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded())
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                }
                else if (jumpCount > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                    jumpCount--;
                }
            }

            //Sprint!
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                moveSpeed *= 2f;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                moveSpeed /= 2f;
            }

            //menu
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                Time.timeScale = 0.0f;
                menu.SetActive(true);
            }
            if(Input.GetKeyUp(KeyCode.Tab))
            {
                Time.timeScale = 1.0f;
                menu.SetActive(false);
            }
        }


    }

    private void FixedUpdate()
    {
        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer <= 0)
            {
                remainingTimer = invincibleTimer;
                isInvincible = false;
            }
        }
    }

    void Move(Vector2 direction, float speed) //Fait bouger le personnage
    {
        rb.AddForce(direction * speed);
    }

    private bool isGrounded() //détecte si le personnage est par dessus le sol
    {
        BoxCollider2D col = this.gameObject.GetComponent<BoxCollider2D>();

        RaycastHit2D raycastHit = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.extents.y + 0.5f, PlateformeLayerMask);
        return raycastHit.collider != null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sol")) // redonne le jump si le personnage touche le dessus du sol
        {
            if (isGrounded())
            {
                jumpCount = 1;
            }
        }
    }

    public void Invulnerabilty()
    {
        print("I'M INVINCIBLE");
        isInvincible = true;
        menu.SetActive(false);
    }

    public void Potion()
    {
        print("I need healing");
        menu.SetActive(false);
    }
}
