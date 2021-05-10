using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
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
        Debug.Log("gooaaaaaaaaaallllllll");
        EndLevel();
    }

    void EndLevel()
    {
        //SceneManager.LoadScene("Level" + (int.Parse(SceneManager.GetActiveScene().name.Substring(5)) + 1));
        //GameManager.instance.GetComponent<ASCIILevelLoader>().LoadLevel();
        GameManager.instance.GetComponent<ASCIILevelLoader>().CurrentLevel++;
    }
}
