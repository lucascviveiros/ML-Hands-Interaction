using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCollider : MonoBehaviour
{
    public Vector3 colVect;
    public Vector3 planeVect;
    public bool fallDown;
    public bool okSet;

    void Start()
    {
        GetComponent<Collider>();
        fallDown = false;
        okSet = false;
    }

    public void setOk(Vector3 vect) 
    {
        okSet = true;
        planeVect = vect;
    }


    private void Update()
    {
         if (fallDown == true && okSet == true) 
         {
            transform.position = new Vector3(transform.position.x, planeVect.y, transform.position.z);
            okSet = false;
         }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planes")
        {
            GameObject plane = collision.gameObject.GetComponent<GameObject>();
            Renderer rend = plane.GetComponent<Renderer>();
            rend.material.SetColor("_Color", Color.red);
            colVect = collision.gameObject.GetComponent<Vector3>();
            fallDown = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Planes") 
        {
            GameObject plane = collision.gameObject.GetComponent<GameObject>();
            Renderer rend = plane.GetComponent<Renderer>();
            rend.material.SetColor("_Color", Color.red);
            colVect = collision.gameObject.GetComponent<Vector3>();
            fallDown = false;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Planes")
        {
            GameObject plane = collision.gameObject.GetComponent<GameObject>();
            Renderer rend = plane.GetComponent<Renderer>();
            rend.material.SetColor("_Color", Color.red);
            colVect = collision.gameObject.GetComponent<Vector3>();
        }
        fallDown = true;

    }

    private void OnTriggerExit(Collider other)
    {
        fallDown = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Planes")
        {
            GameObject plane = other.gameObject.GetComponent<GameObject>();
            Renderer rend = plane.GetComponent<Renderer>();
            rend.material.SetColor("_Color", Color.red);
            colVect = other.gameObject.GetComponent<Vector3>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Planes") 
        {
            colVect = other.gameObject.GetComponent<Vector3>();
            fallDown = false;
        }
    }

    public bool getBool() 
    {
        return fallDown;
    }
}
