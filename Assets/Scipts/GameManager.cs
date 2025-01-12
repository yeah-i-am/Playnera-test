using UnityEngine;

/* GameManager на данный момент нужен только для контроля гравитации.
 * Большой необходимости в этом нет, т.к. предметы и так используют параметр Physics2D.gravity,
 * но чисто в теории, может быть прок от разделения гравитации предметов и общей гравитации */
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float gravityMultiplyer = 1;

    public static float GravityMultiplier
    {
        get => Instance.gravityMultiplyer;
        set => Instance.gravityMultiplyer = value;
    }
}
