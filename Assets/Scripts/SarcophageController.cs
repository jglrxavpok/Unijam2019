using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarcophageController : Activable {
    private bool _onMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_onMove) {
            transform.Rotate(Vector3.up, Time.deltaTime * 10, Space.Self);
        }
    }

    public override void Activate() {
        _onMove = true;
        StartCoroutine(ResetOnMove());
    }

    private IEnumerator ResetOnMove() {
        yield return new WaitForSeconds(1);
        _onMove = false;
    }
    public override void DeActivate() {
        return;
    }
}
