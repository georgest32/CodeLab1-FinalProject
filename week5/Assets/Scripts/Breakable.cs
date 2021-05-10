using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;

            PlayerControl.instance.targetBreakable = gameObject;
            
            //if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.PupCount > 0)
            //{
            //    GameManager.instance.PupCount--;
            //    Destroy(gameObject);
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<SpriteRenderer>().color = new Color32(142, 89, 60, 255);
        PlayerControl.instance.targetBreakable = null;
    }
}
