using System;
using UnityEngine;

public class HealthEventArgs : EventArgs
{
    public Vector2 DamagePosition { get; private set; }
    public int ScorePoints { get; private set; }
    public bool IsPlayer { get; private set; }

    public HealthEventArgs(Vector2 position, int scorePoints , bool isPlayer)
    {
        DamagePosition = position;
        ScorePoints = scorePoints;
        IsPlayer = isPlayer;
    }
}
