using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float widthValue = this.GetComponent<SpriteRenderer>().size.x;
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (widthValue > 0)
            {
                widthValue -= 0.5f;
                this.GetComponent<SpriteRenderer>().size = new Vector2(widthValue, 0.719f);
            } else
            {
                widthValue = 0f;
                this.GetComponent<SpriteRenderer>().size = new Vector2(widthValue, 0.719f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (widthValue < 4.36f)
            {
                widthValue += 0.5f;
                this.GetComponent<SpriteRenderer>().size = new Vector2(widthValue, 0.719f);
            }
            else
            {
                widthValue = 4.36f;
                this.GetComponent<SpriteRenderer>().size = new Vector2(widthValue, 0.719f);
            }
        }
    }
}
