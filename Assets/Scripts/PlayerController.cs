using System; 
using System.Collections; 
using System.Collections.Generic; 
using System.Timers; 
using UnityEngine; 
public class PlayerController : MonoBehaviour 
{ 
    public GameObject player; 
    private float deplacementDroit = 0.1f; 
    private float deplacementDiag; 
    private bool[] RightLeftUpDown= new bool[4]; 
    private bool shiftClick = false; 
    private int timer = 0; 
// Start is called before the first frame update 
    void Start() 
    { 
        deplacementDiag = Mathf.Sqrt((Mathf.Pow(deplacementDroit,2f))/2); 
        RightLeftUpDown[0] = false; 
        RightLeftUpDown[1] = false; 
        RightLeftUpDown[2] = false; 
        RightLeftUpDown[3] = false; 
    } 
// Update is called once per frame 
    void Update() 
    { 
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        { 
            RightLeftUpDown[2] = true; 
        } 
        if (Input.GetKeyDown(KeyCode.DownArrow)) 
        { 
            RightLeftUpDown[3] = true; 
        } 
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        { 
            RightLeftUpDown[0] = true; 
        } 
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        { 
            RightLeftUpDown[1] = true; 
        } 
        if (Input.GetKeyUp(KeyCode.UpArrow)) 
        { 
            RightLeftUpDown[2] = false; 
        } 
        if (Input.GetKeyUp(KeyCode.DownArrow)) 
        { 
            RightLeftUpDown[3] = false; 
        } 
        if (Input.GetKeyUp(KeyCode.RightArrow)) 
        { 
            RightLeftUpDown[0] = false; 
        } 
        if (Input.GetKeyUp(KeyCode.LeftArrow)) 
        { 
            RightLeftUpDown[1] = false; 
        } 
        if (RightLeftUpDown[0]) 
        { 
            if (RightLeftUpDown[3]) 
            { 
                player.transform.rotation = Quaternion.AngleAxis(-45, new Vector3(0,1,0)); 
                player.transform.Translate(deplacementDiag, 0, 0); 
            } 
            else if (RightLeftUpDown[2]) 
            { 
                player.transform.rotation = Quaternion.AngleAxis(45, new Vector3(0,1,0)); 
                player.transform.Translate(deplacementDiag, 0, 0); 
            } 
            else 
            { 
                player.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0,1,0)); 
                player.transform.Translate(deplacementDroit, 0, 0); 
            } 
        } 
        if (RightLeftUpDown[1]) 
        { 
            if (RightLeftUpDown[3]) 
            { 
                player.transform.rotation = Quaternion.AngleAxis(-135, new Vector3(0,1,0)); 
                player.transform.Translate(deplacementDiag, 0, 0); 
            } 
            else if (RightLeftUpDown[2]) 
            { 
                player.transform.rotation = Quaternion.AngleAxis(135, new Vector3(0,1,0)); 
                player.transform.Translate(deplacementDiag, 0, 0); 
            } 
            else 
            { 
                player.transform.rotation = Quaternion.AngleAxis(180, new Vector3(0,1,0)); 
                player.transform.Translate(deplacementDroit, 0, 0); 
            } 
        } 
        if ((RightLeftUpDown[2]) && (!RightLeftUpDown[1]) && (!RightLeftUpDown[0])) 
        { 
            player.transform.rotation = Quaternion.AngleAxis(-90, new Vector3(0,1,0)); 
            player.transform.Translate(deplacementDroit, 0, 0); 
        } 
        if ((RightLeftUpDown[3]) && (!RightLeftUpDown[1]) && (!RightLeftUpDown[0])) 
        { 
            player.transform.rotation = Quaternion.AngleAxis(90, new Vector3(0,1,0)); 
            player.transform.Translate(deplacementDroit, 0, 0); 
        } 
        if (Input.GetKeyDown(KeyCode.LeftShift)) { 
            if (!shiftClick) { 
                shiftClick = true; 
                deplacementDroit *= 15; 
                deplacementDiag = Mathf.Sqrt((Mathf.Pow(deplacementDroit,2f))/2); 
            } 
        } 
        if (shiftClick ) { 
            if (timer == 5) { 
                deplacementDroit = deplacementDroit/15; 
                deplacementDiag = Mathf.Sqrt((Mathf.Pow(deplacementDroit,2f))/2); 
                timer = 0; 
                shiftClick = false; 
            } 
            else { 
                timer++; 
            } 
        } 
    } 
}