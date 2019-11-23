using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    public Animator animator;
    public GameObject camera;

    bool doWalk;
    bool doRotate;

    Quaternion fromRotation;

    float lerp;
    
    void Start()
    {
        doWalk = true;
        doRotate = false;
    }

    void Update()
    {
        Debug.Log(camera.transform.rotation.eulerAngles.x);

        if (doWalk)
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
        }
        else
        {
            if (camera.transform.rotation.eulerAngles.x >= 304 || camera.transform.rotation.eulerAngles.x == 0)
            {
                camera.transform.Rotate(new Vector3(-10, 0, 0) * Time.deltaTime);
            }
            else
            {
                doWalk = true;
                animator.SetBool("walk", true);
                animator.SetBool("stil", false);
                doRotate = true;
                fromRotation = camera.transform.rotation;
            }
        }

        if (doRotate)
        {
            camera.transform.rotation = Quaternion.Slerp(fromRotation, Quaternion.Euler(-20.421f, -361.711f, 0f), lerp);
            lerp += Time.deltaTime * 1 / 5f;

            if(lerp > 1)
            {
                doRotate = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("stop"))
        {
            doWalk = false;
            animator.SetBool("stil", true);
        }
    }
}
