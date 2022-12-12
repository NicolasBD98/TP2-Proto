using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PersonWithHealth
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

    Vector3 savePosition;

    [SerializeField] private LayerMask PlateformeLayerMask;
    [SerializeField] GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        SetupHealth(100);

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
        UpdateLife();
        isDead = NoLifeLeft();
        if (isDead)
        {
            Debug.Log("Player dead.");
        }

        //Gère le mouvement
        if (!isDead)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                Move(Vector2.left, moveSpeed);
                isGrounded(); //checke si on est au sol pour enregistrer la position (au cas où on tombe)
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Move(Vector2.right, moveSpeed);
                isGrounded(); //checke si on est au sol pour enregistrer la position (au cas où on tombe)
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

        }

        //Regarde si on est tombé
        if (transform.position.y < -20f)
        {
            transform.position = savePosition;
            rb.velocity = new Vector2(0, 0);
            LoseLife();
        }


    }

    private void FixedUpdate()
    {
        if(isInvincible)
        {
            remainingTimer -= Time.deltaTime;
            if(remainingTimer <= 0)
            {
                remainingTimer = invincibleTimer;
                isInvincible = false;
                shield.SetActive(false);
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
        if (raycastHit.collider)
        {
            savePosition = transform.position;
        }
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LoseLife();
        }
    }

    public void LoseLife()
    {
        if (!isInvincible)
        {
            takeDamage(5);
        }
    }

    public void Invulnerability()
    {
        isInvincible = true;
        shield.SetActive(true);
    }

    public void Potion()
    {
        Heal(10);
    }
}
