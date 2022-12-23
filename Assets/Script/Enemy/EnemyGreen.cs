using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreen : EnemyController
{

    float speed;
    
    public float attackTimer;
    public float cooldownTimer;
    [SerializeField] private LayerMask WallLayerMask;

    // Start is called before the first frame update
    void Start()
    {

        speed = 3f;
        rangeAttack = 15f;
        attackTimer = 0.8f;
        cooldownTimer = 2f;

        this.SetupHealth(5);
    }

    // Update is called once per frame
    void Update()
    {
        if (life > 0)
        {
            if (target == null)
                target = GameObject.FindGameObjectWithTag("Player");
            float distanceTarget = Vector3.Distance(this.gameObject.transform.position, target.transform.position);
            if (distanceTarget < rangeAttack && !isCoolingDown && !isAttacking)
            {
                if (!HasClearShot())
                {
                    StartCoroutine(StartAttack());
                }
            }

            if (isAttacking)
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            else
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(speed, this.gameObject.GetComponent<Rigidbody2D>().velocity.y, 0);

            if (speed < 0)
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            else
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else        
            StartCoroutine(Death());
    }


    

    IEnumerator StartAttack()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
        isAttacking = true;
        yield return new WaitForSeconds(attackTimer);
        myGun.GetComponent<EnemyGunController>().Shoot(target.transform.position,false);
        isAttacking = false;
        this.gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownTimer);
        isCoolingDown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            speed *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().LoseLife();
        }
    }
  
    IEnumerator Death()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
