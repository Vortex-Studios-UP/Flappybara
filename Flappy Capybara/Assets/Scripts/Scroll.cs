using UnityEngine;

// NOTES
// * FUNCION: This script moves objects horizontally along the screen.
// * It can be used for both background elements and game obstacles.
// * IPooledObject forces the script to use OnPbjectSpawn();

public class Scroll : MonoBehaviour, IPooledObject
{
    // 'Speed' indicates how fast the object will scroll to the left.
    // Negative numbers will result in scrolling to the right.
    [SerializeField] private float speed = 2.5f;

    // Internal instance of the obect's physics component.
    private Rigidbody2D rb;

    // Start is called before the first frame update.
    public void OnObjectSpawn()
    {
        rb = GetComponent<Rigidbody2D>();

        // The physics object's speed is set.
        rb.velocity = new Vector2(-speed, 0f);
    }

    // Update is called once per frame.
    void Update()
    {
        // Stops backround movement when game is over.
        if (GameManager.Instance.isGameOver)
            rb.velocity = Vector2.zero;
    }
}
