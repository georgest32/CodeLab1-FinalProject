using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    private int projectileCount = 10;
    public int rotSpeed;
    public GameObject bulletPrefab;
    private bool isShooting = false;
    private bool alerted = false;
    public GameObject viewCone;
    public TextMesh text;
    public string line;
    public string alertLine;
    private GameObject textMeshObject;
    private Transform textLookTargetTransform;

    private void Start()
    {
        text.text = "";
        textMeshObject = text.gameObject;
        textLookTargetTransform = GameObject.FindObjectOfType<Camera>().transform;
    }

    public int ProjectileCount
    {
        get
        {
            return projectileCount;
        }

        set
        {
            projectileCount = value;
        }
    }

    private void Update()
    {
        if (!isShooting && alerted)
        {
            InvokeRepeating("ShootProjectile", 0, 2);
        }

        if (!alerted)
        {
            RotateSelf();
            CancelInvoke("ShootProjectile");
        }

        if (alerted)
        {
            Debug.Log("shooting");
            FaceTarget();
        }
        
        FaceTextMeshToCamera();
    }

    public virtual void ShootProjectile()
    {
        isShooting = true;
        
        if (projectileCount > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 dir = FindObjectOfType<PlayerControl>().transform.position - bullet.transform.position;
            bullet.GetComponent<Rigidbody2D>().velocity = dir;

            //projectileCount--;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            alerted = true;
            viewCone.GetComponent<SpriteRenderer>().enabled = false;
            SayDialogue(alertLine);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            alerted = false;
            isShooting = false;
            viewCone.GetComponent<SpriteRenderer>().enabled = true;
            SayDialogue(line);
            Invoke("ClearDialogue", 5);
        }

        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void SayDialogue(string line)
    {
        text.text = line;
    }

    void RotateSelf()
    {
        transform.Rotate(Vector3.forward * (Time.deltaTime * rotSpeed));
    }
    
    void FaceTextMeshToCamera(){
        Vector3 origRot = textMeshObject.transform.eulerAngles;
        textMeshObject.transform.LookAt(textLookTargetTransform);
        origRot.z = textMeshObject.transform.eulerAngles.z;
        textMeshObject.transform.eulerAngles = origRot;
    }

    void FaceTarget()
    {
        Vector3 vectorToTarget = FindObjectOfType<PlayerControl>().transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1);
    }
    
    private void ClearDialogue()
    {
        SayDialogue("");
    }
}
