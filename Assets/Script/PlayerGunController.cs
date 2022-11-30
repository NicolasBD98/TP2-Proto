using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunController : GunActions 
{
    public GameObject player;
    public float blackBonusTimer;
    private string currentLayer;
    private bool isBlack;

    // Start is called before the first frame update
    void Start()
    {
        ChangeGun("BlueGun");
        currentLayer = "Blue";
        ChangePlayerLayerColor();
        blackBonusTimer = 0f;
        isBlack = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.Tab))
        {
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
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangePlayerLayerColor();
                currentLayer = equippedGun.LayerName;
            }

            BlackBonusTimer();

            if (Input.GetMouseButtonDown(0)) //left click
            {
                // R�cup�re la position de la souris pour viser
                Vector3 mouseInScreen = Input.mousePosition;
                mouseInScreen.z = 10;
                Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(mouseInScreen);
                // Tire
                Shoot(mouseInWorld, true);
            }
        }
    }

    private void ChangePlayerLayerColor()
    {
        player.layer = LayerMask.NameToLayer(equippedGun.LayerName); // Change la layer du joueur.
        player.GetComponent<Renderer>().material.color = Gun.ColorDictionnary[equippedGun.LayerName]; // Change la couleur du joueur. 
    }

    public void PaintItBlack()
    {
        currentLayer = equippedGun.LayerName;
        blackBonusTimer = 5f;
        isBlack = true;
    }

    private void BlackBonusTimer()
    {
        if (isBlack)
        {
            if (blackBonusTimer > 0f)
            {
                blackBonusTimer -= Time.deltaTime;
                equippedGun.LayerName = "Black";
            }
            else
            {
                Debug.Log("Stop Bonus");
                equippedGun.LayerName = currentLayer;
                blackBonusTimer = 0f;
                isBlack = false;
            }
        }
    }
}
