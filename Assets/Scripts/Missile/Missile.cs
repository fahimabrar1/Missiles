using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    public float speed = 20f; // Missile's movement speed
    public float steerSpeed = 5f; // Steering speed
    public Transform target; // The target (e.g., the plane)

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Plane>().transform;
    }

    private void FixedUpdate()
    {
        if (target is null) return; // If no target, do nothing

        // Direction to the target
        var directionToTarget = (Vector2)target.position - _rb.position;
        directionToTarget.Normalize();
        
        // Current forward direction of the missile
        Vector2 forward = transform.up;
        // Use the cross product to determine steering direction
        var cross = Vector3.Cross(forward, directionToTarget).z; // Z-axis gives direction (-1 for left, 1 for right)
    MyDebug.Log($"Cross product:{cross}");
        // Apply rotation based on the cross product
        var rotation = steerSpeed * cross * Time.deltaTime;
        transform.Rotate(0, 0, rotation);

        // Move forward in the direction the missile is facing
        _rb.velocity = transform.up * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the missile hits the target
        if (collision.transform != target) return;
        Debug.Log("Missile hit the target!");
        Destroy(gameObject); // Destroy the missile on impact
    }
}
