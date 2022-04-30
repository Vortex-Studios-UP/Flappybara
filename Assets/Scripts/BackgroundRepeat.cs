using UnityEngine;

// * FUNCTION: This script repositions a background element to the right whenever it leaves the screen.
// * REQUIREMENTS:
//      1) BoxCollider2D component
//      2) Constant horizontal scrolling

 public class BackgroundRepeat : MonoBehaviour
{
    // Variable to store sprite width.
    private float spriteWidth;

    // Start is called before the first frame update
    void Start()
    {
        // Obstains the sprite width from BoxCollider2D.
        BoxCollider2D groundCollider = GetComponent<BoxCollider2D>();
        spriteWidth = groundCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Triggers a position reset when the sprite is out of bounds.
        if(transform.position.x < -spriteWidth*2)
            ResetPosition();
    }

    // ResetPosition is called to reset the position of a sprite to the right of the game screen.
    private void ResetPosition()
    {
        transform.Translate(new Vector3(4 * spriteWidth, 0.0f, 0.0f));
    }
}