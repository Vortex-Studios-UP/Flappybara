using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    // Singleton pattern
    private static GameTimeManager s_Instance;

    private float _gameTime = 0.0f;
    private bool _countGameTime = false;

    private void Awake()
    {
        if(s_Instance == null)
            s_Instance = this;
    }

    // REMOVE THIS from here and start the timer when the player actually starts moving, probably from another script
    void Start()
    {
        StartGameTimer();
    }

    private void Update()
    {
        if(_countGameTime)
            _gameTime += Time.deltaTime;
    }

    public static void StartGameTimer() => s_Instance._countGameTime = true;
    public static void PauseGameTimer() => s_Instance._countGameTime = false;
    public static void ResetGameTimer() => s_Instance._gameTime = 0.0f;

    public static float GetGameTime() => s_Instance._gameTime;
}
