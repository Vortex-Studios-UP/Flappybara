using UnityEngine;

// FUNCTION: This script moves objects horizontally along the screen.
// NOTES:
// * This can be used for both background elements and game obstacles.

public class Scroll : MonoBehaviour
{
    // Variable that indicates how fast the object will scroll to the left.
    // Negative numbers will result in scrolling to the right.
    [SerializeField] private float speed = 2.5f;

    // Instance of the obect's physics component.
    private Rigidbody2D rb;

    // Start is called before the first frame update.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // The physics object's speed is set.
        rb.velocity = Vector2.left * speed;
    }

    // Update is called once per frame.
    void Update()
    {
        // Stops backround movement when game is over.
        if (GameManager.Instance.isGameOver)
            rb.velocity = Vector2.zero;
    }
}
