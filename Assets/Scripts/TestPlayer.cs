using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {
    public float maxWalkSpeed;
    public float maxRunSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool running = Input.GetKey(KeyCode.Space);
        Vector2 direction = new Vector2();
        float speed = running ? maxRunSpeed : maxWalkSpeed;
        if (Input.GetKey(KeyCode.Z)) {
            direction.y += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            direction.y -= 1;
        }
        if (Input.GetKey(KeyCode.Q)) {
            direction.x -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            direction.x += 1;
        }
        direction.Normalize();
        direction.Scale(new Vector2(speed, speed));

        Vector3 currentPos = transform.position;
        Vector3 newPos = new Vector3(currentPos.x + direction.x, currentPos.y, currentPos.z+direction.y);
        transform.position = newPos;
    }
}
