using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float gravityMultiplyer = 1;

    public static float GravityMultiplier
    {
        get => Instance.gravityMultiplyer;
        set => Instance.gravityMultiplyer = value;
    }
}
