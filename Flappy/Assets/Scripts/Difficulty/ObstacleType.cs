using System;

[Flags]
public enum ObstacleType
{
    Pipes = (1 << 0),
    ObstacleA = (1 << 1),
    ObstacleB = (1 << 2),
    ObstacleC = (1 << 3),
    ObstacleD = (1 << 4),
    ObstacleE = (1 << 5)
}