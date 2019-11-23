using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cinematic : MonoBehaviour
{
    public Animator animator;

    public GameObject camera;

    public Image black;
    Color tempColor;

    bool doWalk;
    bool doRotate;
    bool transition;

    Quaternion fromRotation;

    float lerp;
    
    void Start()
    {
        doWalk = true;
        doRotate = false;
        tempColor = black.color;
        tempColor.a = 0;
        black.color = tempColor;
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

        if (transition)
        {
            tempColor.a += 0.25f * Time.deltaTime;
            black.color = tempColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("stop"))
        {
            doWalk = false;
            animator.SetBool("stil", true);
        }

        if (other.CompareTag("fondu"))
        {
            transition = true;
        }
    }
}
