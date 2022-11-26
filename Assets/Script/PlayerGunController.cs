using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunController : GunActions 
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeGun("BlueGun"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //left click
        {
            // Récupère la position de la souris pour viser
            Vector3 mouseInScreen = Input.mousePosition;
            mouseInScreen.z = 10;  
            Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(mouseInScreen);
            // Tire
            Shoot(mouseInWorld);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeGun("BlueGun");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeGun("GreenGun");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeGun("RedGun");
        }
    }
}
