using System.Collections;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Plane : MonoBehaviour
{
    public float speed = 30f;
    public float speedMultiplier = 2f;
    public float steer = 20f;
    public float tiltAmount = 15f;
    public GameObject DestroyParticle;
    public VariableJoystick VariableJoystick;
    [Header("Shield")] public GameObject ShieldObject;

    public bool isShieldActivated;

    private PlayerInput _playerInput;
    private Rigidbody2D _rb;

    /// <summary>
    ///     Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
    }


    /// <summary>
    ///     This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    /// <summary>
    ///     This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        var input = GetInput();

        if (input != Vector2.zero)
        {
            // Calculate the target direction based on joystick input
            var targetDirection = input.normalized;

            // Smoothly rotate toward the target direction
            var targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
            var smoothedAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, steer * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);

            // Apply tilt for visual feedback
            var tilt = -input.x * tiltAmount; // Negative for correct tilt direction
            transform.rotation *= Quaternion.Euler(0, tilt, 0);
        }

        // Move forward in the current direction
        _rb.velocity = transform.up * (speed * speedMultiplier * Time.deltaTime);
    }
    // private void FixedUpdate()
    // {
    //     var input = GetInput();
    //
    //     // Apply forward movement
    //     _rb.velocity = speed * speedMultiplier * Time.deltaTime * transform.up;
    //
    //     // Calculate the rotation based on horizontal input
    //     var rotation = -input.x * steer * Time.deltaTime;
    //
    //     // Update the plane's overall rotation
    //     var currentRotation = transform.rotation;
    //
    //     // Apply tilt for visual effect
    //     var tilt = -input.x * tiltAmount; // Negative for correct tilt direction
    //     var tiltRotation = Quaternion.Euler(0, tilt, currentRotation.eulerAngles.z + rotation);
    //     MyDebug.Log("Roota:" + tilt);
    //     transform.rotation = tiltRotation;
    // }


    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
    }


    public void OnTriggerPlane(Collider2D collision)
    {
        MyDebug.Log("Collision detected with the plane");

        if (collision.CompareTag("Missile"))
        {
            if (isShieldActivated)
                // Shield is active, let the shield handle it
                return;

            // Trigger explosion effect
            if (DestroyParticle != null)
                Destroy(Instantiate(DestroyParticle, transform.position, Quaternion.identity), 5);

            LevelAudioPlayer.instance.OnPlayAudioByName("explosion-large");

            // Notify GameManager and destroy plane
            GameManager.Instance.OnGameOver();
            gameObject.SetActive(false);
        }
    }


    public void OnDetectOnShield(Collider2D collision)
    {
        MyDebug.Log("Collision detected with the shield");

        if (collision.CompareTag("Missile"))
            // Deactivate shield
            ToggleeShield(false);
    }


    public void ToggleeShield(bool val)
    {
        isShieldActivated = val;
        ShieldObject.SetActive(val);
    }

    private Vector2 GetInput()
    {
        return VariableJoystick.Direction.magnitude > VariableJoystick.MoveThreshold
            ? VariableJoystick.Direction.normalized
            : Vector2.zero;
    }

    public void SetSpeed(float speed, int duration)
    {
        StartCoroutine(setSpeed());

        IEnumerator setSpeed()
        {
            var tempSpeed = this.speed;
            this.speed = speed;
            yield return new WaitForSeconds(duration);
            this.speed = tempSpeed;
        }
    }

    public void GetSkillPoint(int pointValue)
    {
        GameManager.Instance.OnAddScore(pointValue);
    }
}