using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunActions : MonoBehaviour
{
    //public GameObject weapon;
    public Rigidbody2D bulletPrefab;
    public Gun equippedGun;
    private bool isShooting;

    public void Shoot(Vector3 target, bool isFromPlayer)
    {
        isShooting = true;
        StartCoroutine(BulletDelay(target, isFromPlayer));
    }

    public IEnumerator BulletDelay(Vector3 target, bool isFromPlayer)
    {
        for (int i = 0; i < equippedGun.FireRate; i++)
        {
            Rigidbody2D bullet = Instantiate(bulletPrefab, this.transform.position, transform.rotation) as Rigidbody2D;
            bullet.gameObject.layer = LayerMask.NameToLayer(equippedGun.LayerName); // Change la layer de la balle.
            bullet.GetComponent<Renderer>().material.color = Gun.ColorDictionnary[equippedGun.LayerName]; // Change la couleur de la balle. 
            if (isFromPlayer)
            {
                bullet.GetComponent<BulletDestroy>().isFromPlayerGun();
            }
            Vector3 direction = target - this.transform.position; // Vecteur qui va vers la cible.
            direction.Normalize(); // La longueur du vecteur devient 1 (pour qu'on puisse contrôler la vitesse de la balle). 
            bullet.velocity = transform.TransformDirection(direction * equippedGun.BulletSpeed);

            if (equippedGun.BulletSpeed == 15f && equippedGun.FireRate == 1f)
            {
                for (int j = -1; j < 2; j+=2)
                {
                    Rigidbody2D bullet2 = Instantiate(bulletPrefab, this.transform.position, transform.rotation) as Rigidbody2D;
                    bullet2.gameObject.layer = LayerMask.NameToLayer(equippedGun.LayerName); // Change la layer de la balle.
                    bullet2.GetComponent<Renderer>().material.color = Gun.ColorDictionnary[equippedGun.LayerName]; // Change la couleur de la balle. 
                    if (isFromPlayer)
                    {
                        bullet2.GetComponent<BulletDestroy>().isFromPlayerGun();
                    }
                    Vector3 direction2 = target - this.transform.position; // Vecteur qui va vers la cible.
                    direction2.Normalize(); // La longueur du vecteur devient 1 (pour qu'on puisse contrôler la vitesse de la balle). 
                    direction2 = Quaternion.AngleAxis(-20*j, Vector3.forward) * direction2; 
                    bullet2.velocity = transform.TransformDirection(direction2 * equippedGun.BulletSpeed);
                }
            }
            yield return new WaitForSeconds(equippedGun.BulletTimer);
        }
        isShooting = false;
    }

    public void ChangeGun(string accessKey)
    {
        if (!isShooting) // Evite de changer de gun pendant qu'on tire
        {
            equippedGun = new Gun(Gun.GunDictionnary[accessKey]);
        }
    }
}
