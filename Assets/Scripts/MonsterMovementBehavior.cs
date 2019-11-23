using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementBehavior : MonoBehaviour {
    public PlayerController playerController;
    public float catchup = 1.0f;
    
    // nombre total de frames avant que le monstre bouge
    public int frameLagCount = 60 * 5; // 5s
    
    // vitesse au dessus de laquelle le monstre accélère
    public float speedThreshold;

    private List<Vector2> playerPositions = new List<Vector2>();
    
    private bool wasDashing;
    private GameObject player;

    // Start is called before the first frame update
    void Start() {
        player = playerController.gameObject;
        playerPositions.Clear();
    }

    // Update is called once per frame
    // TODO: Time.deltaTime
    void Update() {
        Vector3 currentPlayerPosition = player.transform.position;
        Vector2 currentPlayerPosition2D = new Vector2(currentPlayerPosition.x, currentPlayerPosition.z);

        int nPositionsPerFrame = 6;
        playerPositions.Add(currentPlayerPosition2D);
        for (int i = 0; i < nPositionsPerFrame-1; i++) {
            playerPositions.Add(new Vector2(currentPlayerPosition2D.x, currentPlayerPosition2D.y));
        }
        while (playerPositions.Count >= frameLagCount*nPositionsPerFrame) { // ne pas dépasser un lag donné
            playerPositions.RemoveAt(0);
        }

        int speed = 4;
        if (!wasDashing && playerController.isDashing()) { // dash start
            int aim = (int)(nPositionsPerFrame*catchup*60) /* rattrape "catchup" secondes dès le début du dash */;
            if (aim < 0) {
                aim = 0;
            }

            if (aim > 0) {
                playerPositions.RemoveRange(0, Math.Min(aim, playerPositions.Count-1));
            }
        }

        wasDashing = playerController.isDashing();

        if (playerPositions.Count == 0) {
            Debug.LogError("Plus de positions disponibles!");
        }
        for (int i = 0; i < speed && playerPositions.Count > 1; i++) {
            playerPositions.RemoveAt(0);
        }
        Vector2 position = playerPositions[0];
        Vector3 previousPosition = transform.position;
        transform.position = new Vector3(position.x, 0, position.y);
        Vector3 deltaPosition = transform.position - previousPosition;
        if (deltaPosition.sqrMagnitude > 0.00001) { // change l'angle que si on est pas à la même position qu'avant
            transform.rotation = Quaternion.LookRotation(deltaPosition, Vector3.up);
        }
    }
}