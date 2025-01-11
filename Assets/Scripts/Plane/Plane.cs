using System.Threading.Tasks;
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
        var tiltRotation = Quaternion.Euler(0, tilt, currentRotation.eulerAngles.z + rotation);
        transform.rotation = tiltRotation;
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
                Instantiate(DestroyParticle, transform.position, Quaternion.identity);

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
        return _playerInput.PlayerActions.Movement.ReadValue<Vector2>();
    }

    public async void SetSpeed(float speed, int duration)
    {
        var tempSpeed = this.speed;
        this.speed = speed;
        await Task.Delay(1000 * duration);
        this.speed = tempSpeed;
    }

    public void GetSkillPoint(int pointValue)
    {
        GameManager.Instance.OnAddScore(pointValue);
    }
}