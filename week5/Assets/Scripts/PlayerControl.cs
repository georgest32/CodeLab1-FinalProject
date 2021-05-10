using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float forceAmount = 10;  //public var for force amount
    public bool moveable;

    public GameObject targetBreakable;

    Rigidbody2D rb2D; //var for the Rigidbody2D

    //static variable means the value is the same for all the objects of this class type and the class itself
    public static PlayerControl instance; //this static var will hold the Singleton
    
    public TextMesh text;
    private string line = "huh, musta been the breeze...";
    private GameObject textMeshObject;
    private Transform textLookTargetTransform;
    
    private string[] playerLines;
    private int sayIndex = 0;

    void Awake()
    {
        moveable = true;
        
        if (instance == null)  //instance hasn't been set yet
        {
            DontDestroyOnLoad(gameObject);  //Dont Destroy this object when you load a new scene
            instance = this; //set instance to this object
        }
        else //if the instance is already set to an object
        {
            Destroy(gameObject);  //destroy this new object, so there is only ever one
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        textMeshObject = text.gameObject;
        textLookTargetTransform = GameObject.FindObjectOfType<Camera>().transform;
        rb2D = GetComponent<Rigidbody2D>();  //get the Rigidbody2D  off of this gameObject
        playerLines = new string[4];
        playerLines[0] = "i'm hit!!!";
        playerLines[1] = "dang i'm losing blood...";
        playerLines[2] = "oh man that hurts!";
        playerLines[3] = "the horror...";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && moveable) //if W is pressed
        {
            rb2D.AddForce(Vector2.up * forceAmount); //apply to the up mult by the "force" var
        }
        
        if (Input.GetKey(KeyCode.S) && moveable) //if S is pressed
        {
            rb2D.AddForce(Vector2.down * forceAmount); //apply to the up mult by the "force" var
        }
        
        if (Input.GetKey(KeyCode.A) && moveable) //if A is pressed
        {
            rb2D.AddForce(Vector2.left * forceAmount); //apply to the up mult by the "force" var
        }
        
        if (Input.GetKey(KeyCode.D) && moveable) //if D is pressed
        {
            rb2D.AddForce(Vector2.right * forceAmount); //apply to the up mult by the "force" var
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            rb2D.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.PupCount > 0 && targetBreakable != null)
        {
            //GameManager.instance.PupCount--;
            Destroy(targetBreakable);
        }

        //show map view and return to normal
        if (Input.GetKey(KeyCode.Tab))
        {
            GetComponentInChildren<Camera>().transform.position =
                new Vector3(GetComponentInChildren<Camera>().transform.position.x, -22.3f, -52.75f);
            moveable = false;
        } 
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            GetComponentInChildren<Camera>().transform.localPosition =
                new Vector3(0, 1, -10);
            moveable = true;
        }
    }
    
    public void SayDialogue(string line)
    {
        text.text = line;
    }

    void FaceTextMeshToCamera(){
        Vector3 origRot = textMeshObject.transform.eulerAngles;
        textMeshObject.transform.LookAt(textLookTargetTransform);
        origRot.z = textMeshObject.transform.eulerAngles.z;
        textMeshObject.transform.eulerAngles = origRot;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Color color = GetComponent<SpriteRenderer>().color;
            Color newColor = new Color(color.r + 0.2f, color.g, color.b);
            GetComponent<SpriteRenderer>().color = newColor;

            if (newColor.r >= 1)
            {
                GameManager.instance.GameOver();
            }
            
            SayDialogue(playerLines[sayIndex]);
            Invoke("ClearDialogue", 5);
            sayIndex++;
            
            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Dog"))
        {
            Color color = GetComponent<SpriteRenderer>().color;
            Color newColor = new Color(color.r + 0.2f, color.g, color.b);
            GetComponent<SpriteRenderer>().color = newColor;

            if (newColor.r >= 1)
            {
                GameManager.instance.GameOver();
                return;
            }
            
            SayDialogue(playerLines[sayIndex]);
            Invoke("ClearDialogue", 5);
            sayIndex++;
            
            other.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void ClearDialogue()
    {
        SayDialogue("");
    }
}
