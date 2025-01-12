using UnityEngine;

/* GameManager �� ������ ������ ����� ������ ��� �������� ����������.
 * ������� ������������� � ���� ���, �.�. �������� � ��� ���������� �������� Physics2D.gravity,
 * �� ����� � ������, ����� ���� ���� �� ���������� ���������� ��������� � ����� ���������� */
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float gravityMultiplyer = 1;

    public static float GravityMultiplier
    {
        get => Instance.gravityMultiplyer;
        set => Instance.gravityMultiplyer = value;
    }
}
