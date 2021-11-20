using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that implements user control and rigidbody collision detection
// VALUE: To modify how quickly the bird falls, change the Gravity Scale in its Rigidbody2D component

public class Capybara : MonoBehaviour
{
     // Height of jump
    public float JUMP_AMOUNT = 100f;

    // Mask of what can the capybara collide with
    public ContactFilter2D collisionsFilter;

    // Provide access to physics though the 'rigidbody2D' object
    private Rigidbody2D capybaraRigidbody2D;

    // The player's collider
    private Collider2D capibaraCollider;

    // An array of collisions of fixed length to avoid GC
    private Collider2D[] cachedCollisions = new Collider2D[8];

    private void Awake()
    {
        capybaraRigidbody2D = GetComponent<Rigidbody2D>();
        capibaraCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // PLAYER CONTROLS
        // Jump: Space or Left Click (could be modified to work with touch)
        // Modifies upwards velocity according to 'JUMP_AMOUNT'
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            capybaraRigidbody2D.velocity = Vector2.up * JUMP_AMOUNT;
    }

    private void FixedUpdate()
    {
        // COLLISIONS
        int collisionCount = capibaraCollider.OverlapCollider(collisionsFilter, cachedCollisions);
        for(int i = 0; i < collisionCount; i++)
            DealWithCollision(cachedCollisions[i]);

    }

    // Function triggered on collision (death state)
    private void DealWithCollision(Collider2D other)
    {
        Debug.Log("Dead");
    }
}