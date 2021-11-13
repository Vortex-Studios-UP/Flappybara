using UnityEngine;
using Unity;

public class DifficultyManager : MonoBehaviour
{
    // The routine of difficulties which will dictate when will each difficulty play out and how
    public DifficultyRoutine difficultyRoutine;

    // The actual obstacle spawner whose properties will be modified here
    public RandomSpawner spawner;

    private void Awake()
    {
        // Sorts the routine elements by time, in order to fetch difficulties properly
        difficultyRoutine.SortRoutineByTime();
    }

    private void Update()
    {
        // Applies the effects of the difficulty
        ApplyDifficulty();
    }

    private void ApplyDifficulty()
    {
        // Obtain current time to get the difficulty
        float time = GameTimeManager.GetGameTime();
        // When did this difficulty begin?
        float timeOfDifficultyChange;
        // Getting the current difficulty and time of change
        DifficultyLevel currentDifficulty = difficultyRoutine.GetDifficultyAtTime(time, out timeOfDifficultyChange);
        // Given the time and time the difficulty changed, we can know for how long have we been in this difficulty
        float timeSinceDifficultyChange = Mathf.Max(0f, time - timeOfDifficultyChange);

        // Calculate velocity given the difficulty and how long have we been in this difficulty
        float velocity = currentDifficulty.velocityOnTime.Evaluate(timeSinceDifficultyChange);

        // Set the velocity in the actual spawner
        spawner.MovementSpeed = velocity;

        // Calculate generation frequency given the movement velocity and pipe distance desired by difficulty
        float minHorizontalDistance = currentDifficulty.pipeDistanceRangeHorizontal.x;
        float maxHorizontalDistance = currentDifficulty.pipeDistanceRangeHorizontal.y;
        spawner.GenerationFrequency = Random.Range(minHorizontalDistance, maxHorizontalDistance) / velocity; // v = d/t => t = d/v

        // Calculate the gap size of an obstacle
        float minGap = currentDifficulty.pipeGapSizeRange.x;
        float maxGap = currentDifficulty.pipeGapSizeRange.y;
        spawner.gapBetweenObstacles = Random.Range(minGap, maxGap)+100; // 100 is where it is closed precisely, so any addition to that is the gap size

        // Calculate the Y position of an obstacle given the a range of distances we want it to be in with respect to the last
        float minVerticaDistance = currentDifficulty.pipeDistanceRangeVertical.x;
        float maxVerticaDistance = currentDifficulty.pipeDistanceRangeVertical.y;
        spawner.distanceFromLastObstacleY = Random.Range(minVerticaDistance, maxVerticaDistance);
    }
}
