using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         Color color = other.GetComponent<SpriteRenderer>().color;
    //         Color newColor = new Color(color.r + 0.2f, color.g, color.b);
    //         other.GetComponent<SpriteRenderer>().color = newColor;
    //         if (newColor.r >= 1)
    //         {
    //             GameManager.instance.GameOver();
    //         }
    //         Destroy(gameObject);
    //     }

        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
