using System; 
using System.Collections; 
using System.Collections.Generic; 
using System.Timers;
using UnityEditor;
using UnityEngine; 
public class PlayerController : MonoBehaviour {
    public float dashDuration = 0.5f;
    public float dashSpeedMultiplier = 4f;
    public float speed = 10f;
    
    private bool _isDashing = false;
    private bool _isMoving;
    private float _xAxisInput;
    private float _yAxisInput;
// Start is called before the first frame update 
    void Start() {
        
    } 
// Update is called once per frame 
    void Update() {
        _yAxisInput = Input.GetAxis("Horizontal");
        _xAxisInput = Input.GetAxis("Vertical");
        _isMoving = (_xAxisInput != 0 || _yAxisInput != 0);
        Vector3 moveVector3 = (Vector3.right * _yAxisInput + Vector3.forward * _xAxisInput);
        transform.Translate(Time.deltaTime * speed * moveVector3.normalized , Space.World);
        if (_isMoving) {
            transform.rotation = Quaternion.AngleAxis(Vector3.Angle(Vector3.right, moveVector3)*(moveVector3.z<0?1:-1) + 90, Vector3.up);
        }

        if (!Input.GetButtonDown("Dash")) return;
        if (!_isDashing) {
            _isDashing = true;
            speed *= dashSpeedMultiplier;
        }
        StartCoroutine(ResetDash());
    }

    private IEnumerator ResetDash() {
        yield return new WaitForSeconds(dashDuration);
        if (!_isDashing) yield break;
        _isDashing = false;
        speed /= dashSpeedMultiplier;
    }
    public bool IsDashing() {
        return _isDashing;
    }

    public bool IsMoving() {
        return _isMoving;
    }
}