using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PersonWithHealth
{
    // Start is called before the first frame update
    void Start()
    {
        SetupHealth(30);
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
}
