using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class DragAndDropHingeJoint : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float springForce = 100000f;

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

        (float min, float max) limits =
            hingeJoint.useLimits
            ? (hingeJoint.limits.min, hingeJoint.limits.max)
            : (-180f, 180f);

        JointSpring spring = hingeJoint.spring;
        spring.spring = springForce;
        float targetPosition = Vector3.SignedAngle(Vector3.down, direction, Vector3.forward);
        targetPosition = Mathf.Clamp(targetPosition, limits.min, limits.max);
        spring.targetPosition = targetPosition;
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
