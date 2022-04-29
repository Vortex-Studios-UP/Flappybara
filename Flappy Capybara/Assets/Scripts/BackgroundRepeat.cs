using UnityEngine;

// NOTES
// * FUNCION: This script repositions a background element to the right whenever it leaves the screen.
// * It requires (1) a BoxCollider2D and (2) constant horizontal scrolling.

public class BackgroundRepeat : MonoBehaviour
{
    private float spriteWidth;

    // Start is called before the first frame update
    void Start()
    {
        // Obtains the sprite width from BoxCollider2D
        BoxCollider2D groundCollider = GetComponent<BoxCollider2D>();
        spriteWidth = groundCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Triggers a position reset when the sprite is out of bounds.
        if(transform.position.x < -spriteWidth)
            ResetPosition();
    }

    // ResetPosition is called to reset the position of a sprite to the right of the game screen.
    private void ResetPosition()
    {
        transform.Translate(new Vector3(2 * spriteWidth, 0.0f, 0.0f));
    }
}