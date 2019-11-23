using System; 
using System.Collections; 
using System.Collections.Generic; 
using System.Timers;
using UnityEditor;
using UnityEngine; 
public class PlayerController : MonoBehaviour {
    public float dashDuration = 6f;
    public float dashSpeedMultiplier = 5f;

    private GameObject player;
    public float speed = 10f;
    //private bool[] RightLeftUpDown= new bool[4];
    private bool shiftClick = false;
    private int timer = 0;
    public bool moving;
    
    private float _xAxisInput;
    private float _yAxisInput;
// Start is called before the first frame update 
    void Start() {
        
    } 
// Update is called once per frame 
    void Update() {
        _yAxisInput = Input.GetAxis("Horizontal");
        _xAxisInput = Input.GetAxis("Vertical");
        moving = (_xAxisInput != 0 || _yAxisInput != 0);
        Vector3 moveVector3 = (Vector3.right * _yAxisInput + Vector3.forward * _xAxisInput);
        transform.Translate(Time.deltaTime * speed * moveVector3.normalized , Space.World);
        if (moving) {
            transform.rotation = Quaternion.AngleAxis(Vector3.Angle(Vector3.right, moveVector3)*(moveVector3.z<0?1:-1) + 90, Vector3.up);
        }

        if (Input.GetAxis("Jump")>0) {
            if (!shiftClick) {
                shiftClick = true;
                speed *= dashSpeedMultiplier;
            }
            StartCoroutine(ResetDash());
        }
    }

    private IEnumerator ResetDash() {
        yield return new WaitForSeconds(dashDuration);
        if (shiftClick) {
            shiftClick = false;
            speed /= dashSpeedMultiplier;
        }
    }
    public bool isDashing() {
        return shiftClick;
    }

    public bool isMoving() {
        return moving;
    }
}