using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private bool fromPlayerGun = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fromPlayerGun) //On veut que les balles ennemies puissent toucher le joueur. 
        {
            if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Shield"))
                {
                    Destroy(this.gameObject);
                }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyController>().LoseLife();
            }
        } else
        {
            if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Shield"))
            {
                Destroy(this.gameObject);
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.GetComponent<PlayerController>().LoseLife();
            }
        }
    }

    public void isFromPlayerGun ()
    {
        fromPlayerGun = true;
    }
}
