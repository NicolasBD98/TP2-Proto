using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private bool isPickedUp;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isPickedUp)
            {
                if (Input.GetKey("c"))
                { 
                    this.transform.SetParent(collision.transform);
                    Destroy(gameObject.GetComponent<Rigidbody2D>());
                    this.transform.position += new Vector3(0f,0.25f,0f);
                    isPickedUp = true;
                }
                
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isPickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp)
        {
            if (Input.GetKeyUp("c"))
            {
                this.transform.SetParent(null);
                gameObject.AddComponent<Rigidbody2D>();
                this.GetComponent<Rigidbody2D>().mass = 8;
                this.GetComponent<Rigidbody2D>().gravityScale = 2;
                isPickedUp = false;
            }
        }
    }
}
