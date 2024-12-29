using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Plane : MonoBehaviour
{
    public float speed = 30f;
    public float speedMultiplier = 2f;
    public float steer = 20f;
    public float tiltAmount = 15f; 
    private Rigidbody2D _rb;
    private PlayerInput _playerInput;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        var input = GetInput();
    
        // Apply forward movement
        _rb.velocity = speed * speedMultiplier * Time.deltaTime * transform.up;
       
        // Calculate the rotation based on horizontal input
        var rotation = -input.x * steer * Time.deltaTime;

        // Update the plane's overall rotation
        var currentRotation = transform.rotation;
        
        // Apply tilt for visual effect
        var tilt = -input.x * tiltAmount; // Negative for correct tilt direction
        var tiltRotation = Quaternion.Euler(0,tilt,currentRotation.eulerAngles.z + rotation);
        transform.rotation = tiltRotation;
    }

    private Vector2 GetInput()
    {
        return _playerInput.PlayerActions.Movement.ReadValue<Vector2>();
    }
}
