using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.PupCount++;

            if (GameObject.FindObjectsOfType<Pup>().Length <= 1)
            {
                GameManager.instance.timeElapsed = 0;
                GameManager.instance.timeLimit -= GameManager.instance.timeLimit / 6;
                GameManager.instance.Score += GameManager.instance.PupCount;
                GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(19, -5.8f);
                GameManager.instance.GetComponent<ASCIILevelLoader>().LoadLevel();
            }
            
            Destroy(gameObject);
        }
    }
}
