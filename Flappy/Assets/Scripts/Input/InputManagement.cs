using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManagement : MonoBehaviour
{
    // Command pattern: a command gets assigned to an input
    private Action<GameInputState> click;
    private Action<GameInputState> jump;
    private Action<GameInputState> escape;

    public FlappyMovement playerMovement;
    public GameInputState inputState; // Keeps track, for example, of whether we're on menu or playing

    // Shorthand to pass a function that does nothing
    private static Action nothing = () => { };

    private void Awake()
    {
        // Assigns the action to be performed per input
        click = (state) => StateSwitchUtilty( () => playerMovement.Jump(), nothing );
        jump = (state) => StateSwitchUtilty( () => playerMovement.Jump(), nothing );
        escape = (state) => StateSwitchUtilty( 
            () => { playerMovement.Stop(); inputState = GameInputState.menu; }, 
            () => { playerMovement.UnStop(); inputState = GameInputState.playing; } 
        );
    }

    private void Update()
    {
        // Input checking

        if (Input.GetButtonDown("Jump"))
            jump(inputState);

        if(Input.GetButtonDown("Fire1"))
            click(inputState);

        if(Input.GetButtonDown("Cancel"))
            escape(inputState);
    }

    // Helps keep the code DRY
    // Assigns input to first action if playing and to second action if on menu
    // Change this and make necessary changes if we need more or less states
    private void StateSwitchUtilty(Action playing, Action menu)
    {
        switch (inputState)
        {
            case GameInputState.playing:
                playing();
                break;
            case GameInputState.menu:
                menu();
                break;
        }
    }
}
