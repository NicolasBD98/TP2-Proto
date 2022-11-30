using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : GunActions
{
    private GameObject[] player;
    public float ShootDelayTime = 3f; // L'ennemi tire toutes les 3s. 

    // Start is called before the first frame update
    void Start()
    {
        ChangeGun(Gun.LayerToGun[LayerMask.LayerToName(transform.parent.gameObject.layer)]); // Le gun de l'ennemi dépend de son layer.
        player = GameObject.FindGameObjectsWithTag("Player"); // L'ennemi vise le player. 
        StartCoroutine(ShootDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShootDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(ShootDelayTime);
            Shoot(player[0].transform.position, false);
        }
        
    }
}
