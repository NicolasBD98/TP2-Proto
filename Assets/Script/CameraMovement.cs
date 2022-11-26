using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] GameObject player;
    private Vector3 cameraOffset = new Vector3(0, 0, -10);
    private float smoothFactor = 3f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPosition = player.transform.position + cameraOffset; // Recule la caméra hors du plan du jeu, pour qu'elle "filme" le jeu.
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime); //La position de la caméra est interpolée pour qu'elle ne suive pas directement le joueur. 
        //float clampX = Mathf.Clamp(smoothPosition.x, 0f, 100f); // Permet de restreindre la caméra aux limites du terrain si nécessaire. 
        float clampY = Mathf.Clamp(smoothPosition.y, 0f, 20f);
        transform.position = new Vector3(smoothPosition.x, clampY, -10f);
    }
}
