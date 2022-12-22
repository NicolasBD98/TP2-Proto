using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun 
{
    public int FireRate { get; set; }
    public int BulletSpeed { get; set; }
    public float BulletTimer { get; set; }
    public string LayerName { get; set; }

    public Gun()
    {
        FireRate = 0;
        BulletSpeed = 0;
        BulletTimer = 0;
        LayerName = "Default";
    }

    public Gun(int fireRate, int bulletSpeed, float bulletTimer, string layerName)
    {
        this.FireRate = fireRate;
        this.BulletSpeed = bulletSpeed;
        this.BulletTimer = bulletTimer;
        this.LayerName = layerName;
    }

    public Gun(Gun gun)
    {
        this.FireRate = gun.FireRate;
        this.BulletSpeed = gun.BulletSpeed;
        this.BulletTimer = gun.BulletTimer;
        this.LayerName = gun.LayerName;
    }

    public static Dictionary<string, Gun> GunDictionnary = new Dictionary<string, Gun>()
    {
        //Clé d'accès, Données (fireRate, bulletSpeed, BulletTimer, color)
        {"BlueGun", new Gun(1,25,0f, "Blue") },
        {"RedGun", new Gun(3,15,0.1f, "Red") },
        {"GreenGun", new Gun(1,15,0f, "Green") }
    };

    public static Dictionary<string, Color> ColorDictionnary = new Dictionary<string, Color>()
    {
        {"Blue", Color.blue},
        {"Red", Color.red},
        {"Green", Color.green},
        {"Black", Color.black}
    };

    public static Dictionary<string, string> LayerToGun = new Dictionary<string, string>()
    {
        {"Blue", "BlueGun"},
        {"Red", "RedGun"},
        {"Green", "GreenGun"},
    };
}
