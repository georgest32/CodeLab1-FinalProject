using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class ASCIILevelLoader : MonoBehaviour
{
    public float xOffset;
    public float yOffset;
    
    public GameObject player;
    public GameObject wall;
    public GameObject ouch;
    public GameObject goal;
    public GameObject breakable;
    public GameObject pup;

    public string[] file_names;

    private Vector2 startPos;
    public GameObject currentPlayer;

    private int currentLevel;
    public GameObject level;

    public GameObject[] pupSpawnPoints;

    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
            LoadLevel();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        Destroy(level);
        level = new GameObject("Level");
        
        string[] current_file_paths = new string[15];
        List<string[]> allFileLines = new List<string[]>();

        //Add all ASCII modules into filepath array
        for (int i = 0; i < file_names.Length; i++)
        {
            current_file_paths[i] = 
            Application.dataPath + 
                "/Levels/" + 
                file_names[i] + ".txt";
        }
        
        //make array of lines from each filepath and add it into list of strings
        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                allFileLines.Add(File.ReadAllLines(current_file_paths[i]));
            }
            else allFileLines.Add(File.ReadAllLines(current_file_paths[Random.Range(1, current_file_paths.Length)]));
        }
        
        string[] fileLines = new string[current_file_paths.Length];
        int yCellOffset = 0;
        int xCellOffset = 1;

        //iterate through collection of text files (which have been separated into lines), then go through each line
        //to build level
        for (int i = 0; i < allFileLines.Count; i++)
        {
            int j = 0;
            
            for (int y = 0; y < allFileLines[i].Length; y++)
            {
                string lineText = allFileLines[i][j];
                char[] characters = lineText.ToCharArray();
                
                for (int x = 0; x < characters.Length; x++)
                {
                    char c = characters[x];
                
                    GameObject newObj;
                
                    switch (c)
                    {
                        case 'P':
                            newObj = Instantiate<GameObject>(player);
                            currentPlayer = newObj;
                            startPos = new Vector2(
                                x + xOffset, 
                                -y + yOffset);
                            break;
                        case 'w':
                            newObj = Instantiate<GameObject>(wall);
                            break;
                        case '*':
                            newObj = Instantiate<GameObject>(ouch);
                            break;
                        case '&':
                            newObj = Instantiate<GameObject>(goal);
                            break;
                        case 'b':
                            newObj = Instantiate<GameObject>(breakable);
                            break;
                        case 'p':
                            newObj = Instantiate<GameObject>(pup);
                            break;
                        default:
                            newObj = null;
                            break;
                    }
                
                    if (newObj != null)
                    {
                        if (newObj.gameObject.name != "Player(Clone)")
                        {
                            newObj.transform.parent = level.transform;
                            newObj.transform.position = new Vector3(x + (xCellOffset * 13), -y - (yCellOffset * 13), 0);
                        }
                        else newObj.transform.position = new Vector2(19, -5.8f);
                    }
                }
                
                j++;
            }
            
            
            //every three cells, return to starting cell x-position (left side of grid)
            if (i % 3 == 0)
            {
                yCellOffset++;
            }
            
            if (xCellOffset >= 2)
            {
                xCellOffset = -1;
            }

            xCellOffset++;
        }

        //randomly place pups around the level
        for (int i = 0; i < pupSpawnPoints.Length; i++)
        {
            if(Random.Range(0,3) == 1)
            {
                Instantiate<GameObject>(pup, pupSpawnPoints[i].transform.position, Quaternion.identity);
            }
        }

        //if the player has few pups, help em out
        if (GameManager.instance.PupCount < 2)
        {
            GameManager.instance.PupCount = 2;
        }
    }

    public void ResetPlayer()
    {
        currentPlayer.transform.position = startPos;
    }
}
