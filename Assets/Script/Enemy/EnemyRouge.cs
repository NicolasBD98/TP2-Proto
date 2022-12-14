using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRouge : EnemyController
{
    float speed;

    public float attackTimer;
    public float cooldownTimer;
    public float distanceTarget;





    // Start is called before the first frame update
    void Start()
    {

        speed = 3f;
        rangeAttack = 15f;
        attackTimer = 0.8f;
        cooldownTimer = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");
        distanceTarget = Vector3.Distance(this.gameObject.transform.position, target.transform.position);
        if ((distanceTarget < rangeAttack /*&& rangeAttack > -distanceTarget*/)&& !isCoolingDown && !isAttacking)
        {
            
            if (!HasClearShot())
            {
                print("ok");
                StartCoroutine(StartAttack());
            }
        }

        if (isAttacking)
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        else
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(speed, this.gameObject.GetComponent<Rigidbody2D>().velocity.y, 0);


    }




    IEnumerator StartAttack()
    {
        isAttacking = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sol")
        {
            print("hello");
            speed *= -1;
        }
    }
}
