using UnityEngine;
using System.Collections.Generic;

public class RunnerController : RunnerObject {

    public float moveSpeed = 1f;
    public float rotationTime = 0.25f;

    public float maxDepth = 2;
    public float minDepth = 0;

    public float fullDepthCrossTime = 0.5f;

    public Transform character; 
    private HashSet<RunnerObject> overlappingObjects;

    private void Awake() {
        overlappingObjects = new HashSet<RunnerObject>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        RunnerObject obstacle = other.GetComponent<RunnerObject>();
        if (!obstacle) {
            return;
        }

        overlappingObjects.Add(obstacle);
    }

    private void OnTriggerExit2D(Collider2D other) {
        RunnerObject obstacle = other.GetComponent<RunnerObject>();
        if (!obstacle) {
            return;
        }

        overlappingObjects.Remove(obstacle);
    }

    private void Update() {
        float rotationStep = -360 / rotationTime * Time.deltaTime;
        character.Rotate(0, 0, rotationStep);

        float verticalInputAxis = Input.GetAxis("Vertical");
        if (verticalInputAxis == 0) {
            return;
        }

        float deltaDepth = Mathf.Abs(maxDepth - minDepth);
        float depthStep = deltaDepth / fullDepthCrossTime * Time.deltaTime;
        if (verticalInputAxis < 0) {
            depthStep *= -1;
        }

        Vector3 characterPosition = character.localPosition;
        characterPosition.y = Mathf.Clamp(characterPosition.y + depthStep, minDepth, maxDepth);
        character.localPosition = characterPosition;
    }
}

// this is a player controller
// the important part is depth, which is y coordinate of the sprite
// each runner game object has one, while we're passing through the object we comparing them
// if they are close, then we run the hit event