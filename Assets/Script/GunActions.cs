using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunActions : MonoBehaviour
{
    public GameObject weapon;
    public Rigidbody2D bulletPrefab;
    public Gun equippedGun;
    private bool isShooting;

    public void Shoot(Vector3 target)
    {
        isShooting = true;
        StartCoroutine(BulletDelay(target));
    }

    public IEnumerator BulletDelay(Vector3 target)
    {
        for (int i = 0; i < equippedGun.FireRate; i++)
        {
            Rigidbody2D bullet = Instantiate(bulletPrefab, weapon.transform.position, transform.rotation) as Rigidbody2D;
            bullet.gameObject.layer = LayerMask.NameToLayer(equippedGun.LayerName); // Change la layer de la balle.
            bullet.GetComponent<Renderer>().material.color = equippedGun.BulletColor; // Change la couleur de la balle. 
            Vector3 direction = target - weapon.transform.position; // Vecteur qui va vers la cible.
            direction.Normalize(); // La longueur du vecteur devient 1 (pour qu'on puisse contrôler la vitesse de la balle). 
            bullet.velocity = transform.TransformDirection(direction * equippedGun.BulletSpeed);
            yield return new WaitForSeconds(equippedGun.BulletTimer);
        }
        isShooting = false;
    }

    public void ChangeGun(string accessKey)
    {
        if (!isShooting) // Evite de changer de gun pendant qu'on tire
        {
            equippedGun = Gun.GunDictionnary[accessKey];
        }
    }
}
