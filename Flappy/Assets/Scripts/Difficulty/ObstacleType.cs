using System;

[Flags]
public enum ObstacleType
{
    pipes = (1 << 0),
    A = (1 << 1),
    B = (1 << 2),
    C = (1 << 3),
    D = (1 << 4),
    E = (1 << 5)
}