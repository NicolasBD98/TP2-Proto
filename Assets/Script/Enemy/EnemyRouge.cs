using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRouge : EnemyController
{
    float speed;

    public float attackTimer;
    public float cooldownTimer;
    public float distanceTarget;
    public float waitTime;





    // Start is called before the first frame update
    void Start()
    {
        this.SetupHealth(5);
        
        waitTime = 3f;
        speed = 3f;
        rangeAttack = 10f;
        attackTimer = 0.3f;
        cooldownTimer = 2f;

        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (life > 0)
        {
            if (target == null)
                target = GameObject.FindGameObjectWithTag("Player");
            distanceTarget = Vector3.Distance(this.gameObject.transform.position, target.transform.position);
            if ((distanceTarget < rangeAttack) && !isCoolingDown && !isAttacking)
            {


                StartCoroutine(StartAttack());
            }

            if (isAttacking)
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            else
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(speed, this.gameObject.GetComponent<Rigidbody2D>().velocity.y, 0);
        }
        else
            Dies();
        
    }

    IEnumerator Dies()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }


    IEnumerator StartAttack()
    {
        isAttacking = true;
        this.gameObject.GetComponent<Animator>().SetTrigger("Attack!");
        yield return new WaitForSeconds(attackTimer);
        myGun.GetComponent<EnemyGunController>().Shoot(target.transform.position, false);
        isAttacking = false;
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownTimer);
        isCoolingDown = false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        ChangeDirection();
    }

    void ChangeDirection()
    {
        speed *= -1;
        StartCoroutine(Wait());
    }

}
