/*

This class embeds information which answers the question of which difficulty will be had at a particular point in time
This is accomplished through:
    GetDifficultyAtTime

Everything else is there to provide Unity Inspector commodities.

*/

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DifficultyRoutine
{
    // It's an array and not a list because the Unity inspector only supports arrays
    [SerializeField] private TimeDifficultyPair[] difficultyRoutine;

    // Sorts the routine elements by time, in order to fetch difficulties properly
    // This has to happen at least once before using GetDifficultyAtTime
    public void SortRoutineByTime()
    {
        // Turns the array into a list in order to sort it using C#'s IComparer and sorting algorithms
        List<TimeDifficultyPair> tempListToSortRoutine = new List<TimeDifficultyPair>(difficultyRoutine);
        tempListToSortRoutine.Sort(new TimeSort());
        difficultyRoutine = tempListToSortRoutine.ToArray();
    }

    // Fetches the difficulty at a given time (as specified by the routine)
    // 'difficultyStartingTime' helps make values such as velocityOverTime dependent only on the time elapsed since the difficulty changed
    public DifficultyLevel GetDifficultyAtTime(float time, out float difficultyStartingTime)
    {
        // Searches top down, given that the event that happens last is higher up in the array
        for(int i = difficultyRoutine.Length - 1; i >= 0; i--)
        {
            if(time > difficultyRoutine[i].time)
            {
                difficultyStartingTime = difficultyRoutine[i].time;
                return difficultyRoutine[i].difficulty;
            }
        }

        // If no difficulty is set at this point in time, it will default to the difficulty which happens first
        difficultyStartingTime = difficultyRoutine[0].time;
        return difficultyRoutine[0].difficulty;
    }

    // Encapsulates and shows these values in the inspector as single array element
    [Serializable]
    private class TimeDifficultyPair
    {
        [SerializeField] [Min(0)] public float time;
        [SerializeField] public DifficultyLevel difficulty;
    }

    // Helps with sorting time-difficulty pairs
    private class TimeSort : IComparer<TimeDifficultyPair>
    {
        public int Compare(TimeDifficultyPair a, TimeDifficultyPair b)
        {
            return Comparer<float>.Default.Compare(a.time, b.time);
        }
    }
}

