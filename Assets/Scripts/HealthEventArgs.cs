using System;
using UnityEngine;

public class HealthEventArgs : EventArgs
{
    public Vector2 Position { get; private set; }
    public bool IsPlayer { get; private set; }

    public HealthEventArgs(Vector2 position, bool isPlayer)
    {
        Position = position;
        IsPlayer = isPlayer;
    }
}
