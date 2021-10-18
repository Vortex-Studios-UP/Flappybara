using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public FlappyMovement player;
    public InputManagement input;
    private void Awake()
    {
        player = gameObject.GetComponent<FlappyMovement>();

    }
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collisioner"))
        {
            player.Stop();
            input.inputState = GameInputState.menu;
        }

    }

}
