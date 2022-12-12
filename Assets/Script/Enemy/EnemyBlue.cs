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

    [SerializeField] private LayerMask WallLayerMask;
    public int nbJumpTurn;

    // Start is called before the first frame update
    void Start()
    {
        jumpforce = 7f;
        speed = 0.55f;
        rangeAttack = 10f;
        jumpTimer = 2f;
        cooldownTimer = jumpTimer;
        jumpDuration = 0.5f;

        nbJumpTurn = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");
        float distanceTarget = Vector3.Distance(this.gameObject.transform.position, target.transform.position);
        

        if(cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(jumpforce * speed, jumpforce);
            cooldownTimer = jumpTimer;
            if (distanceTarget < rangeAttack && !isAttacking /*&& isCoolingDown*/)
            {
                if (!HasClearShot())
                {
                    StartCoroutine(StartAttack());
                }
            }
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
            if (isGrounded())
            {
                nbJumpTurn--;
                if (nbJumpTurn == 0)
                {
                    speed *= -1;
                    nbJumpTurn = 3;
                }
            }
            else
            {
                speed *= -1;
                nbJumpTurn = 4;
            }
        }
    }

    private bool isGrounded() //détecte si le personnage est par dessus le sol
    {
        BoxCollider2D col = this.gameObject.GetComponent<BoxCollider2D>();

        RaycastHit2D raycastHit = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.extents.y + 0.5f, WallLayerMask);
        return raycastHit.collider != null;
    }
}
