using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public Transform[] points;
    public float MoveSpeed;
    public int currrentPoint;

    public Transform platform;

    private Vector3 lastPosition; 
    public Vector3 DeltaMovement { get; private set; } 

    void Start() {
        platform.position = points[currrentPoint].position;
        lastPosition = platform.position;
    }

    void Update() {
        platform.position = Vector3.MoveTowards(platform.position, points[currrentPoint].position, MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(platform.position, points[currrentPoint].position) < 0.5f) {
            currrentPoint++;
            if (currrentPoint >= points.Length) {
                currrentPoint = 0;
            }
        }

        DeltaMovement = platform.position - lastPosition;
        lastPosition = platform.position;
    }
}