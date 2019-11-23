using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    public GameObject player;
    public Text text;
    RectTransform textPos;

    // Start is called before the first frame update
    void Start()
    {
        textPos = text.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
        textPos.localPosition = textPos.localPosition + new Vector3(0, 75, 0) * Time.deltaTime;
    }
}
