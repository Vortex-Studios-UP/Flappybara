using UnityEngine;

// POINTERS
// * This component increases the score when the object is collided with.

public class Score : MonoBehaviour
{
    // OnTriggerEnter2D is called when colliding with another physics object.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.IncreaseScore();
    }
}
