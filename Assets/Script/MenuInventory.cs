using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInventory : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerGun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = 0.0f;
            menu.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Time.timeScale = 1.0f;
            menu.SetActive(false);
        }
    }

    public void MulticolorPaint()
    {
        player.GetComponent<PlayerController>().Potion();
        menu.SetActive(false);
    }

    public void BlackPaint()
    {
        playerGun.GetComponent<PlayerGunController>().PaintItBlack();
        menu.SetActive(false);
    }

    public void WhitePaint()
    {
        player.GetComponent<PlayerController>().Invulnerability();
        menu.SetActive(false);
    }
}
