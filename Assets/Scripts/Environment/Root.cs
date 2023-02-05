using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{

    [SerializeField] private Sprite rootFace; 
    [SerializeField] private GameObject faceSocket; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        OnTriggerExecute(col);
    }

    public void OnTriggerChild(Collider2D col)
    {
        OnTriggerExecute(col);
    }

    private void OnTriggerExecute(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // play death sound
            // play death visual
            Checkpoint.ReturnPlayer();
        }

        if (col.gameObject.CompareTag("Axe"))
        {
            /* SpriteRenderer spriteRenderer = faceSocket.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = rootFace;
            spriteRenderer.sortingOrder = 20;
            faceSocket.transform.parent = null;
            faceSocket.transform.localScale = new Vector3(0.1f, 0.1f, 0);
            faceSocket.transform.parent = gameObject.transform;
            */

            Destroy(gameObject);
        }
    }
}
