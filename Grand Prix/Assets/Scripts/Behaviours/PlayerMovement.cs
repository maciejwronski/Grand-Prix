using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private float forwardForce = 800.0f; // Force which pushes player forward
    private float sideForce = 5f; // Force which pushes player on sides
    private float maxVelocity = 30.0f; // Max velocity which player can has
    Rigidbody rb;
    PlayerBehaviour player;
    void Start () {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerBehaviour>();
	}
	
    private void FixedUpdate() {
        if (TimeCounter.startTimer <= GameBehaviour.startCounter)
            return;
        if ((player.IsRotating() && player.CanMoveOnRotation()) || !player.IsRotating()) {
            UpdateMove();
            if (!player.HasNitro()) /*/ Checks if player has nitro, if no, player can't exceed maximum velocity. /*/
                rb.velocity = OutOfVelocityRange(); // If player velocity is too high, set it to maximum one
        }
    }
    private void UpdateMove() {
        if (rb.velocity.x < 0f) {
            rb.velocity = new Vector3(0.5f, rb.velocity.y, rb.velocity.z);
            return;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, RoadGenerator.defaultBounds[0], RoadGenerator.defaultBounds[1]));
        rb.AddForce(forwardForce * Time.deltaTime * Input.GetAxisRaw("Horizontal"), 0, 0, ForceMode.Acceleration);

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Input.GetAxisRaw("Vertical") * sideForce);

        if (RestrictMovement(rb.position)) {
            SetNewMovement();
        }
    }
    /*/ Restrict player movement on range /*/
    private bool RestrictMovement(Vector3 playerPos) {
        if (playerPos.z > RoadGenerator.bridgeBounds[1] || playerPos.z < RoadGenerator.bridgeBounds[0]) {
            return true;
        }
        return false;
    }
    /*/ Set new movement according to maximum z-range /*/
    private void SetNewMovement() {

        if ((rb.position.z > RoadGenerator.defaultBounds[1])) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            rb.position = new Vector3(rb.position.x, rb.position.y, RoadGenerator.defaultBounds[1]);
        }
        else if (rb.position.z < RoadGenerator.defaultBounds[0]) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            rb.position = new Vector3(rb.position.x, rb.position.y, RoadGenerator.defaultBounds[0]);
        }
    }

    /*/ Is player's velocity higher than possible? /*/
    private Vector3 OutOfVelocityRange() {
        if (rb.velocity.x > maxVelocity)
            return new Vector3(maxVelocity, rb.velocity.y, rb.velocity.z);
        return new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
    }
}
