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
        rb2D = GetComponent<Rigidbody2D>();  //get the Rigidbody2D  off of this gameObject
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("TB null is " + targetBreakable);
            Debug.Log(GameManager.instance.PupCount);
        }

        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.PupCount > 0 && targetBreakable != null)
        {
            Debug.Log("boom");
            GameManager.instance.PupCount--;
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
}
