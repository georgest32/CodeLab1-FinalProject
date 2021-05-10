using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //static variable means the value is the same for all the objects of this class type and the class itself
    public static GameManager instance; //this static var will hold the Singleton
    
    //text components
    public Text pupText;
    public Text scoreText;
    public Text timerText;

    public GameObject gameOverPanel;

    //timer stuff
    public float timeElapsed = 0;
    public int timeLimit = 60;
    
    private int _score = 0;

    public int Score
    {
        get
        {
            return _score;
        } 
        set
        {
            _score = value;
        }
    }

    private int _pupCount = 3;

    public int PupCount
    {
        get
        {
            return _pupCount;
        }

        set
        {
            _pupCount = value;
        }
    }

    void Awake()
    {
        if (instance == null) //instance hasn't been set yet
        {
            DontDestroyOnLoad(gameObject);  //Dont Destroy this object when you load a new scene
            instance = this;  //set instance to this object
        }
        else  //if the instance is already set to an object
        {
            Destroy(gameObject); //destroy this new object, so there is only ever one
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetKey(KeyCode.Tab))
        {
            timeElapsed += Time.deltaTime;
        }
        
        scoreText.text = "Score: " + _score;
        pupText.text = "Pups: " + _pupCount;
        timerText.text = (timeLimit - (int)timeElapsed).ToString();

        if (timeElapsed >= timeLimit)
        {
            _score += _pupCount;
            gameOverPanel.SetActive(true);
        }
    }
}
