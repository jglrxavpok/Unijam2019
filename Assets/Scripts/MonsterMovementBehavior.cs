using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementBehavior : MonoBehaviour {
    public GameObject player;
    
    // nombre total de frames avant que le monstre bouge
    public int frameLagCount = 60 * 5; // 5s
    
    // vitesse au dessus de laquelle le monstre accélère
    public float speedThreshold;

    private List<Vector2> playerPositions = new List<Vector2>();
    
    private Vector2 lastPlayerPosition;
    
    // Start is called before the first frame update
    void Start() {
        var position = player.transform.position;
        lastPlayerPosition = new Vector2(position.x, position.z);
        playerPositions.Clear();
    }

    // Update is called once per frame
    void Update() {
        Vector3 currentPlayerPosition = player.transform.position;
        Vector2 currentPlayerPosition2D = new Vector2(currentPlayerPosition.x, currentPlayerPosition.z);
        playerPositions.Add(currentPlayerPosition2D);
        playerPositions.Add(new Vector2(currentPlayerPosition2D.x, currentPlayerPosition2D.y));
        playerPositions.Add(new Vector2(currentPlayerPosition2D.x, currentPlayerPosition2D.y));
        playerPositions.Add(new Vector2(currentPlayerPosition2D.x, currentPlayerPosition2D.y));
        playerPositions.Add(new Vector2(currentPlayerPosition2D.x, currentPlayerPosition2D.y));
        while (playerPositions.Count >= frameLagCount) { // ne pas dépasser un lag donné
            playerPositions.RemoveAt(0);
        }

        float playerSpeed = (lastPlayerPosition - currentPlayerPosition2D).magnitude;
        int speed = 4;
        if (playerSpeed > speedThreshold) {
            speed = 20;
            Debug.Log("Speeeed!");
        }

        lastPlayerPosition = currentPlayerPosition2D;
        /*if (lag > 0) {
            lag--;
            return;
        }*/

        if (playerPositions.Count == 0) {
            Debug.LogError("Plus de positions disponibles!");
        }
        for (int i = 0; i < speed && playerPositions.Count > 1; i++) {
            playerPositions.RemoveAt(0);
        }
        Vector2 position = playerPositions[0];
        transform.position = new Vector3(position.x, 0, position.y);
    }
}