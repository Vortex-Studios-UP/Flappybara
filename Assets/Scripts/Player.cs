using UnityEngine;

// POINTERS
// * This script handles player movement, animation and collision.
// * Physics are used to implement character movement.
// * [SerializeField] makes a private variable accesible on the Inspector.
// * Rigidbody2D is a component that allows a GameObject to interact with 2D physics.
// * Vector2 provides a collection of physics vectors por Rigidbody2D.

public class Player : MonoBehaviour
{
    [SerializeField] private float upForce; // The force of each jump.

    private bool isDead; // If true, indicates player has died.
    private Rigidbody2D playerRb; // Instances for 2D physics.
    private Animator playerAnimator; // Instance for animation.

    // Start is called before the first frame update.
    void Start()
    {
        // Declares local variables that access this GameObjet's components.
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame.
    void Update()
    {
        // Executes a flap when (1) the Left Mouse Button is clicked and (2) the player is not dead.
        if (Input.GetMouseButtonDown(0) && !isDead)
            Flap();
    }

    // Flap executes an upwards flap/jump.
    private void Flap()
    {
            // To ensure jump consistency, the RigidBody2D comes to a complete halt before the upwards force is applied.
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up * upForce);
            playerAnimator.SetTrigger("Flap");
    }

    // OnCollisionEnter2D is called when the Collider collides with another physics object.
    private void OnCollisionEnter2D()
    {
        isDead = true;
        playerAnimator.SetTrigger("Die");

        // Calls the Game Over logic from the Game Manager (text box)
        GameManager.Instance.GameOver();
    }
}
