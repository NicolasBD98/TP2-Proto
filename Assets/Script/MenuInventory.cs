using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInventory : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerGun;

    [SerializeField] GameObject invicibleCount;
    [SerializeField] GameObject blackBulletCount;
    [SerializeField] GameObject potionCount;

    [SerializeField] GameObject invicibleButton;
    [SerializeField] GameObject blackBulletButton;
    [SerializeField] GameObject potionButton;

    private int nbMulticolorPaint;
    private int nbBlackPaint;
    private int nbWhitePaint;
    private int collectibleMin;
    private int collectibleMax;

    // Start is called before the first frame update
    void Start()
    {
        nbMulticolorPaint = 0;
        nbBlackPaint = 0;
        nbWhitePaint = 0;
        collectibleMin = 0;
        collectibleMax = 5;
        showCount(invicibleCount, nbWhitePaint);
        showCount(blackBulletCount, nbBlackPaint);
        showCount(potionCount, nbMulticolorPaint);
    }

    // Update is called once per frame
    void Update()
    {
        //menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = 0.0f;
            showButtonIfItems(nbBlackPaint, blackBulletButton);
            showButtonIfItems(nbWhitePaint, invicibleButton);
            showButtonIfItems(nbMulticolorPaint, potionButton);
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
        if (nbMulticolorPaint > collectibleMin)
        {
            nbMulticolorPaint--;
            showCount(potionCount, nbMulticolorPaint);
            player.GetComponent<PlayerController>().Potion();
            menu.SetActive(false);
        }
    }

    public void BlackPaint()
    {
        if (nbBlackPaint > collectibleMin)
        {
            nbBlackPaint--;
            showCount(blackBulletCount, nbBlackPaint);
            playerGun.GetComponent<PlayerGunController>().PaintItBlack();
            menu.SetActive(false);
        }
    }

    public void WhitePaint()
    {
        if (nbWhitePaint > collectibleMin)
        {
            nbWhitePaint--;
            showCount(invicibleCount, nbWhitePaint);
            player.GetComponent<PlayerController>().Invulnerability();
            menu.SetActive(false);
        }
    }

    public void addCollectible(string collectibleType)
    {
        if (collectibleType == "whitePaint")
        {
            if (nbWhitePaint < collectibleMax)
            {
                nbWhitePaint++;
                showCount(invicibleCount, nbWhitePaint);
            }

        } else if (collectibleType == "blackPaint")
        {
            if (nbBlackPaint < collectibleMax)
            {
                nbBlackPaint++;
                showCount(blackBulletCount, nbBlackPaint);
            }
                
        } else if (collectibleType == "multicolorPaint")
        {
            if (nbMulticolorPaint < collectibleMax)
            {
                nbMulticolorPaint++;
                showCount(potionCount, nbMulticolorPaint);
            }
        }
    }

    void showCount(GameObject counter, int value)
    {
        counter.GetComponent<UnityEngine.UI.Text>().text = "x " + value;
    }

    void ActivateButton(GameObject button, bool mustBeActive)
    {
        button.GetComponent<UnityEngine.UI.Button>().interactable = mustBeActive;
    }

    void showButtonIfItems(int nbItems, GameObject button)
    {
        if (nbItems <= collectibleMin)
        {
            ActivateButton(button, false);
        }
        else
        {
            ActivateButton(button, true);
        }
    }
    
}
