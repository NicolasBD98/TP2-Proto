using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected string collectibleType; // whitePaint, blackPaint, multiPaint
    [SerializeField] GameObject menu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        menu.GetComponent<MenuInventory>().addCollectible(collectibleType);
        Destroy(this.gameObject);
    }

        // Start is called before the first frame update
        void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
