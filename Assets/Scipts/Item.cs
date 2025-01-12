using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(RectTransform))]
public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    /* ����� ��������� ��������. ������ �� �������, �� ���� ��� �� �����. */
    private bool isCaptured = false;
    private bool isOnFloor = false;
    private bool isOnShelf = false;

    private Vector2 deltaPos; // ����� ������� ��������. ����� ��� ����������� ��������������

    /* ������������ ���������� ��� �������� */
    private Rigidbody2D rb;
    private RectTransform rectTransform;
    private new Renderer renderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rectTransform = transform as RectTransform;
        renderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        renderer.sortingOrder = -(int)transform.position.y; // ���� ������� �� ������� ������� �� ��� y. ���� - ������

        if (isCaptured || isOnFloor || isOnShelf) // ���� ������ ����� ��� ������, ������ ����� ������ �� �����
            return;

        rb.velocity += Physics2D.gravity * GameManager.GravityMultiplier * Time.fixedDeltaTime; // ����������
    }

    // ��������������� ����� ��� ��������� ������� ��������� � ������� �����������
    private Vector2 GetPointerPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData data)
    {
        // ��� �������������� �������, ������ ��� ������� �������������
        rectTransform.position = GetPointerPosition() + deltaPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // ����� ������� ������ - ���������� ��� ��������, ������ ��������� � ������������� �������
        deltaPos = (Vector2)transform.position - GetPointerPosition();
        isCaptured = true;
        rb.velocity = Vector2.zero;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isCaptured = false; // ������ ���������, ����� ��������� ������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor")) // ��� ������� ���� - ������ ���������, ������������� �������
        {
            isOnFloor = true;
            rb.velocity = Vector2.zero;
        }
        else if (collision.CompareTag("Shelf") && isCaptured)
        {
            // ��� ������� �����, ���� ������ ��� ������� ������� - ������ ���������
            isOnShelf = true;
        }
    }

    // ��� ������ ������ ���������, ���� ������ ������ �� �� ���� ��� �� �� �����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            isOnFloor = false;
        }
        else if (collision.CompareTag("Shelf"))
        {
            isOnShelf = false;
        }
    }
}
