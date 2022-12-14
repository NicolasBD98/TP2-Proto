using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PersonWithHealth
{

    public bool isAttacking;
    public bool isCoolingDown;
    public GameObject myGun;
    public float rangeAttack;
    public GameObject target;

    [SerializeField] private LayerMask PlateformeLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        SetupHealth(30);


        if (myGun == null)
        {
            this.gameObject.transform.GetChild(0);
        }

        isAttacking = false;
        isCoolingDown = false;
    }

    // Update is called once per frame
    void Update()
    {

        
        UpdateLife();
        if (NoLifeLeft())
        {
            Destroy(this.gameObject);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            takeDamage(5);
        }
    }*/

    public void LoseLife()
    {
        takeDamage(5);
    }

    public bool HasClearShot() //vérifie s'il y a un mur entre l'ennemi et le joueur
    {
        BoxCollider2D col = this.gameObject.GetComponent<BoxCollider2D>();

        RaycastHit2D hit = Physics2D.Raycast(col.bounds.center, target.transform.position - this.transform.position, rangeAttack, PlateformeLayerMask);
        Debug.DrawRay(this.gameObject.transform.position, target.transform.position - this.transform.position, Color.red);
        return hit.collider;
    }
}
