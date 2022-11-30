using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFiller : MonoBehaviour
{

    float smoothFactor = 4f;
    float newWidth;
    float maxWidth;

    // Start is called before the first frame update
    void Start()
    {
        newWidth = this.GetComponent<SpriteRenderer>().size.x;
        maxWidth = this.GetComponent<SpriteRenderer>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float widthValue = this.GetComponent<SpriteRenderer>().size.x;


        if (newWidth != widthValue)
        {
            float smoothWidth = Mathf.Lerp(widthValue, newWidth, smoothFactor * Time.deltaTime);
            this.GetComponent<SpriteRenderer>().size = new Vector2(smoothWidth, 0.719f);
        }
        
    }

    public void fillBar(int lifePoints, int maxLife)
    {
        newWidth = ((float)maxWidth / maxLife) * (float)lifePoints;
    }
}
