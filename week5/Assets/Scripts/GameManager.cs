using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    //file to store high scores
    const string FILE_HIGH_SCORES = "/Logs/highScores.txt";
    
    //full path to high scores
    string FILE_PATH_HIGH_SCORE;
    
    public static GameManager instance;
    private bool isGame = true;
    
    //text components
    public Text pupText;
    public Text scoreText;
    public Text timerText;
    public Text finalScoreText;
    public Text hSText;

    public GameObject gameOverPanel;

    //timer stuff
    public float timeElapsed = 0;
    public int timeLimit = 10;
    
    //score stuff
    private int _score = 0;

    //List of all the high scores
    List<int> highScores = null;
    private int _pupCount = 0;

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
        FILE_PATH_HIGH_SCORE = Application.dataPath + FILE_HIGH_SCORES;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetKey(KeyCode.Tab))
        {
            timeElapsed += Time.deltaTime;
        }
        
        if (!isGame) //if we're not in the game, display high scores
        {
            //make the highScoreString
            string highScoreString = "";

            //for loop for all the high scores
            for (var i = 0; i < highScores.Count; i++)
            {
                //add each high score to the string
                highScoreString += (i + 1) + ".   <b>" + highScores[i] + "</b>\n------\n";
            }

            //set the displayText to the string we just built
            hSText.text = highScoreString;
        }
        
        scoreText.text = "Score: " + _score;
        timerText.text = (timeLimit - (int)timeElapsed).ToString();

        if (timeElapsed >= timeLimit && isGame)
        {
            GameOver();
        }
    }
    
    void UpdateHighScores()
    {
        if (highScores == null) //if we don't have the high scores yet
        {
            highScores = new List<int>();

            string fileContents = File.ReadAllText(FILE_PATH_HIGH_SCORE);

            string[] fileScores = fileContents.Split(',');
            
            for (var i = 0; i < fileScores.Length - 1; i++)
            {
                highScores.Add(Int32.Parse(fileScores[i]));
            }
        }
        
        for (var i = 0; i < highScores.Count; i++)
        {
            if (_score > highScores[i])
            {
                highScores.Insert(i, _score);
                break;
            }
        }

        string saveHighScoreString = "";

        for (var i = 0; i < highScores.Count; i++)
        {
            saveHighScoreString += highScores[i] + ",";
        }

        File.WriteAllText(FILE_PATH_HIGH_SCORE, saveHighScoreString);
    }

    public void GameOver()
    {
        _score += _pupCount;
        SceneManager.LoadScene("GameOver");
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + _score;
        isGame = false;
        UpdateHighScores(); //call update high scores
    }
}
