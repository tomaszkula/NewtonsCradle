using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool usePhysics = false;

    private Vector3 mousePositionOffset = Vector3.zero;

    private Rigidbody rigidbody = null;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePositionOffset = mousePosition - GetScreenPosition();
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition - mousePositionOffset);

        if (usePhysics)
        {
            rigidbody.MovePosition(position);
        }
        else
        {
            transform.position = position;
        }
    }

    public Vector3 GetScreenPosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
}
