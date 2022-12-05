using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyGreen : EnemyController
{

    float speed;
    public float rangeAttack;
    public float attackTimer;
    public float cooldownTimer;
    public GameObject target;

    bool isAttacking;
    bool isCoolingDown;
    public GameObject myGun;

    [SerializeField] private LayerMask PlateformeLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        if(myGun == null)
        {
            this.gameObject.transform.GetChild(0);
        }

        isAttacking = false;
        isCoolingDown = false;

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
        float distanceTarget = Vector3.Distance(this.gameObject.transform.position, target.transform.position);
        if(distanceTarget < rangeAttack && !isCoolingDown && !isAttacking)
        {
            if(!HasClearShot())
            {
                StartCoroutine(StartAttack());
            }
        }

        if (isAttacking)
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        else
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(speed, this.gameObject.GetComponent<Rigidbody2D>().velocity.y, 0);


    }


    private bool HasClearShot() //vérifie s'il y a un mur entre l'ennemi et le joueur
    {
        BoxCollider2D col = this.gameObject.GetComponent<BoxCollider2D>();

        RaycastHit2D hit = Physics2D.Raycast(col.bounds.center, target.transform.position - this.transform.position, rangeAttack, PlateformeLayerMask);

        return hit.collider;
    }

    IEnumerator StartAttack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackTimer);
        myGun.GetComponent<EnemyGunController>().Shoot(target.transform.position,false);
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
