using UnityEngine;

public class DragAndDropHingeJoint : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool usePhysics = false;
    [SerializeField] private float speed = 1f;

    private bool isDragging = false;
    private Vector3 mousePositionOffset = Vector3.zero;

    private HingeJoint hingeJoint = null;

    private void Awake()
    {
        hingeJoint = GetComponent<HingeJoint>();
    }

    private void FixedUpdate()
    {
        if (!isDragging)
            return;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 endPosition = Camera.main.ScreenToWorldPoint(mousePosition - mousePositionOffset);
        Vector3 basePosition = transform.TransformPoint(hingeJoint.anchor);
        Vector3 direction = endPosition - basePosition;

        JointSpring spring = hingeJoint.spring;
        spring.spring = 100000;
        spring.targetPosition = Vector3.SignedAngle(Vector3.down, direction, Vector3.forward);
        hingeJoint.spring = spring;
    }

    private void OnMouseDown()
    {
        isDragging = true;

        hingeJoint.useSpring = true;

        Vector3 mousePosition = Input.mousePosition;
        mousePositionOffset = mousePosition - GetScreenPosition();
    }

    private void OnMouseUp()
    {
        isDragging = false;

        hingeJoint.useSpring = false;
    }

    public Vector3 GetScreenPosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
}
