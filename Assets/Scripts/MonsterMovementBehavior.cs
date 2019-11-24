using System;
using System.Collections;
using System.Collections.Generic;
using Unity.UNetWeaver;
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

    public ParticleSystem particules;
    private int letParticlesBeFinish = 5;
    private int durationParticles = 0;
    private bool beginParticles = false;
    public bool begin = true;
    private bool dashAfterParticles = false;

    private bool following = false;
    private Vector3 startPosition;
    private Vector3 target;

    private float lerpAmount = 0f;
    public float lerpSpeed = 0.25f;
    public float distanceThreshold = 0.01f;

    // Start is called before the first frame update
    void Start() {
        player = playerController.gameObject;

        startPosition = transform.position;
        target = player.transform.position;
        
        playerPositions.Clear();
    }

    // Update is called once per frame
    // TODO: Time.deltaTime
    void Update() {
        Vector3 currentPlayerPosition = player.transform.position;
        Vector2 currentPlayerPosition2D = new Vector2(currentPlayerPosition.x, currentPlayerPosition.z);

        int nPositionsPerFrame = 6;
        Vector2 last = playerPositions.FindLast(x => true);
        float deltaDistance = (last - currentPlayerPosition2D).magnitude;
        Debug.Log(deltaDistance+" - "+distanceThreshold);
        if (deltaDistance > distanceThreshold) { // on ajoute les nouvelles positions que si le joueur a bougé
            playerPositions.Add(currentPlayerPosition2D);
            for (int i = 0; i < nPositionsPerFrame-1; i++) {
                playerPositions.Add(new Vector2(currentPlayerPosition2D.x, currentPlayerPosition2D.y));
            }
        }
        
        if (!following) {
            target = playerPositions[0];
            Vector3 delta = new Vector3(target.x, 0, target.y) - startPosition;
            if (lerpAmount >= 1f) { // on est à destination
                following = true;
                Debug.Log("Lerp done");
            } else { // on doit se rapprocher
                lerpAmount += Time.deltaTime * lerpSpeed;
                transform.position = startPosition + delta * lerpAmount;
            }   
            return;
        }
        while (playerPositions.Count >= frameLagCount*nPositionsPerFrame) { // ne pas dépasser un lag donné
            playerPositions.RemoveAt(0);
        }

        int speed = 4;
        if (dashAfterParticles) { // dash start
            int aim = (int)(nPositionsPerFrame*catchup*60) /* rattrape "catchup" secondes dès le début du dash */;
            if (aim < 0) {
                aim = 0;
            }

            dashAfterParticles = false;

            if (aim > 0) {
                playerPositions.RemoveRange(0, Math.Min(aim, playerPositions.Count-1));
            }
        }

       

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
        if (deltaPosition.sqrMagnitude > 0.00001) {
            // change l'angle que si on est pas à la même position qu'avant
            transform.rotation = Quaternion.LookRotation(deltaPosition, Vector3.up);
        }
        
        if (begin) {
            begin = false;
            particules.Stop();
        }

        if (!wasDashing &&  playerController.isDashing()) {
            particules.Play();
            dashAfterParticles = true;
            Debug.Log("play");
        }

        if (wasDashing) {
            if (!beginParticles) {
                beginParticles = true;
            }
            else {
                durationParticles++;
                if (durationParticles > letParticlesBeFinish) {
                    particules.Stop();
                    beginParticles = false;
                    Debug.Log("stop");
                    durationParticles = 0;
                }
            }
        }
        wasDashing = playerController.isDashing();
    }

    public void InitPositions(int loadSize) {
        startPosition = transform.position;
        target = player.transform.position;
        
        Vector3 position = player.transform.position;
        Vector2 initialPosition = new Vector2(position.x, position.z);
        playerPositions.Clear();
        for (int i = 0; i < loadSize; ++i) {
            playerPositions.Add(initialPosition);
        }
    }
}