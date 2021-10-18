using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that implements user control and rigidbody collision detection
// VALUE: To modify how quickly the bird falls, change the Gravity Scale in its Rigidbody2D component

public class Capybara : MonoBehaviour
{
     // Height of jump
    public float JUMP_AMOUNT = 100f;

    // Provide access to physics though the 'rigidbody2D' object
    private Rigidbody2D capybaraRigidbody2D;
    private void Awake()
    {
        capybaraRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // PLAYER CONTROLS
        // Jump: Space or Left Click (could be modified to work with touch)
        // Modifies upwards velocity according to 'JUMP_AMOUNT'
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            capybaraRigidbody2D.velocity = Vector2.up * JUMP_AMOUNT;
    }

    // Function triggered on collision (death state)
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Dead");
    }
}