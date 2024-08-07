using System;
using UnityEngine;

public class HealthEventArgs : EventArgs
{
    public Vector2 DamagePosition { get; private set; }
    public int ScoreValue { get; private set; }
    public int HealthPoints { get; private set; }
    public bool IsPlayer { get; private set; }


    public HealthEventArgs(Vector2 position,int scoreValue, int healthPoints , bool isPlayer)
    {
        DamagePosition = position;
        ScoreValue = healthPoints;
        HealthPoints = healthPoints;
        IsPlayer = isPlayer;
    }
}
