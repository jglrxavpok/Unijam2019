using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {
    public Rigidbody rb; 
    
    float speed = 100f;

    private float rSpeed = 50f;
    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        
        float xAxis = Input.GetAxis("Vertical");
        float yAxis = Input.GetAxis("Horizontal");
        //transform.Translate( xAxis * speed * Time.deltaTime * Vector3.forward);
        rb.AddForce(xAxis * speed * Time.deltaTime * transform.forward);
        transform.Rotate(Time.deltaTime * rSpeed * yAxis * Vector3.up);
        
    }
}
