using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty", menuName = "ScriptableObjects/Difficulty", order = 1)]
public class DifficultyLevel : ScriptableObject
{
    // Which obstacles will this difficulty spawn?
    [Tooltip("Which obstacles will this difficulty spawn?")]
    [EnumFlags] public ObstacleType obstacles;

    // Range of gap sizes between pipes (x is min, y is max)
    [Tooltip("Range of horizontal distance between pipes (x is min, y is max)")]
    public Vector2 pipeGapSizeRange;

    // Range of horizontal distance between pipes (x is min, y is max)
    [Tooltip("Range of horizontal distance between pipes (x is min, y is max)")]
    public Vector2 pipeDistanceRangeHorizontal;

    // Range of vertical distance between pipes (x is min, y is max)
    [Tooltip("Range of vertical distance between pipes (x is min, y is max)")]
    public Vector2 pipeDistanceRangeVertical;

    // Range of how many obstacles will be found between two pipe pairs (x is min, y is max)
    [Tooltip("Range of how many obstacles will be found between two pipe pairs (x is min, y is max)")]
    public Vector2Int extraObstacleDensityRange;

    // What is the velocity at each point in time?
    [Tooltip("What is the velocity at each point in time?")]
    public AnimationCurve velocityOnTime;

    // Is this difficulty easy, medium, hard, etc.
    [Tooltip("Is this difficulty easy, medium, hard, etc.")]
    public DifficultyTier difficultyTier;
}
