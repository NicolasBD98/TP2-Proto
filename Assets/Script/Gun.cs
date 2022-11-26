using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun 
{
    public int FireRate { get; set; }
    public int BulletSpeed { get; set; }
    public float BulletTimer { get; set; }
    public Color BulletColor { get; set; }
    public string LayerName { get; set; }

    public Gun()
    {
        FireRate = 0;
        BulletSpeed = 0;
        BulletTimer = 0;
        BulletColor = Color.white;
        LayerName = "Default";
    }

    public Gun(int fireRate, int bulletSpeed, float bulletTimer, Color bulletColor, string layerName)
    {
        this.FireRate = fireRate;
        this.BulletSpeed = bulletSpeed;
        this.BulletTimer = bulletTimer;
        this.BulletColor = bulletColor;
        this.LayerName = layerName;
    }

    public static Dictionary<string, Gun> GunDictionnary = new Dictionary<string, Gun>()
    {
        //Clé d'accès, Données (fireRate, bulletSpeed, BulletTimer, color)
        {"BlueGun", new Gun(1,10,0f, Color.blue, "Blue") },
        {"RedGun", new Gun(5,60,0.1f, Color.red, "Red") },
        {"GreenGun", new Gun(3,30,0.2f, Color.green, "Green") }
    };
}
