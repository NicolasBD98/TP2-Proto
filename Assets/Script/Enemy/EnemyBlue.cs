using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlue : EnemyController
{
    float speed;
    public float jumpDuration;
    public float jumpTimer;
    public float cooldownTimer;
    public float jumpforce;
    public float startJumpAnim;

    [SerializeField] private LayerMask WallLayerMask;
    public int nbJumpTurn;

    // Start is called before the first frame update
    void Start()
    {
        this.SetupHealth(20);
        jumpforce = 7f;
        speed = 0.55f;
        rangeAttack = 10f;
        jumpTimer = 2.5f;
        cooldownTimer = jumpTimer;
        jumpDuration = 1f;


        startJumpAnim = 0.5f;
        nbJumpTurn = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (life > 0)
        {
            if (target == null)
                target = GameObject.FindGameObjectWithTag("Player");
            float distanceTarget = Vector3.Distance(this.gameObject.transform.position, target.transform.position);


            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
            else
            {
                StartCoroutine(StartJump());
                nbJumpTurn--;
                if (nbJumpTurn == 0)
                {
                    speed *= -1;
                    nbJumpTurn = 2;
                }
                cooldownTimer = jumpTimer;
                if (distanceTarget < rangeAttack && !isAttacking /*&& isCoolingDown*/)
                {
                    StartCoroutine(StartAttack());
                }
            }
            if (speed > 0)
                this.gameObject.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            else
                this.gameObject.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            StartCoroutine(Dies());
        }
    }

    IEnumerator StartAttack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(jumpDuration);
        myGun.GetComponent<EnemyGunController>().Shoot(target.transform.position, false);
        isAttacking = false;
        //StartCoroutine(AttackCooldown());
    }

    //IEnumerator AttackCooldown()
    //{
    //    isCoolingDown = true;
    //    yield return new WaitForSeconds(cooldownTimer);
    //    isCoolingDown = false;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sol")
        {

            if (collision.GetContact(0).point.y < this.gameObject.transform.position.y)
            { 
                this.gameObject.GetComponent<Animator>().SetBool("hitGround", true);
                
            }           
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().LoseLife();
        }
    }

    IEnumerator StartJump()
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("isJumping");
        this.gameObject.GetComponent<Animator>().SetBool("hitGround", false);
        yield return new WaitForSeconds(startJumpAnim);
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(jumpforce * speed, jumpforce);
    }

    IEnumerator Dies()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
