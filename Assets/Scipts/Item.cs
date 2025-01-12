using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(RectTransform))]
public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    /* Флаги состояния предмета. Свачен ли игроком, на полу или на полке. */
    private bool isCaptured = false;
    private bool isOnFloor = false;
    private bool isOnShelf = false;

    private Vector2 deltaPos; // Точка захвата предмета. Нужна для корректного перетаскивания

    /* Кэшированные компоненты для удобства */
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
        renderer.sortingOrder = -(int)transform.position.y; // Слой зависит от позиции объекта по оси y. Выше - глубже

        if (isCaptured || isOnFloor || isOnShelf) // Если объект лежит или свачен, ничего более делать не нужно
            return;

        rb.velocity += Physics2D.gravity * GameManager.GravityMultiplier * Time.fixedDeltaTime; // Гравитация
    }

    // Вспомогатлеьный метод для получения позиции указателя в мировых координатах
    private Vector2 GetPointerPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData data)
    {
        // При перетаскивании объекта, меняем его позицию соответсвенно
        rectTransform.position = GetPointerPosition() + deltaPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Когда хватаем объект - запоминаем где схватили, меняем состояние и останавливаем падение
        deltaPos = (Vector2)transform.position - GetPointerPosition();
        isCaptured = true;
        rb.velocity = Vector2.zero;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isCaptured = false; // Меняем состояние, когда отпустили объект
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor")) // При касании пола - меняем состояние, останавливаем падение
        {
            isOnFloor = true;
            rb.velocity = Vector2.zero;
        }
        else if (collision.CompareTag("Shelf") && isCaptured)
        {
            // При касании полки, если объект был положен игроком - меняем состояние
            isOnShelf = true;
        }
    }

    // Тут просто меняем состояние, если объект больше не на полу или не на полке
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
