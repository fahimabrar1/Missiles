using System;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour, IMissile
{
    public float speed = 20f; // Missile's movement speed
    public float steerSpeed = 5f; // Steering speed
    public Transform target; // The target (e.g., the plane)
    public GameObject explosionPrefab; // Particle effect for explosion
    [SerializeField] private float randomAreaRadius = 10f; // Radius for random position
    private Rigidbody2D _rb;
    public IIndicator Indicator;
    public Action<HomingMissile> OnMissileDestroyed;
    private Vector2 randomTargetPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        // Randomize speed and steering for variety
        speed += Random.Range(-2f, 2f);
        steerSpeed += Random.Range(-1f, 1f);
    }

    private void Start()
    {
        // Destroy the missile after 10 seconds if it doesn't hit anything
        Destroy(gameObject, 10f);
    }


    private void FixedUpdate()
    {
        var targetPos = Vector2.zero;
        try
        {
            targetPos = target.position;
        }
        catch (Exception)
        {
            targetPos = _rb.position + Random.insideUnitCircle * randomAreaRadius;
        }


        // Seek the assigned target
        var directionToTarget = targetPos - _rb.position;
        directionToTarget.Normalize();

        // Current forward direction of the missile
        Vector2 forward = transform.up;

        // Use the cross product to determine steering direction
        var cross = Vector3.Cross(forward, directionToTarget).z;

        // Apply rotation based on the cross product
        var rotation = steerSpeed * cross * Time.deltaTime;
        transform.Rotate(0, 0, rotation);

        // Move forward in the direction the missile is facing
        _rb.velocity = transform.up * (speed * Time.deltaTime);
    }


    private void OnDestroy()
    {
        Indicator?.OnDestroyIndicatorTarget();
        OnMissileDestroyed?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MyDebug.Log("HomingMissile OnTriggerEnter2D");
        if (collision.CompareTag("Player") || collision.CompareTag("Missile"))
        {
            // Trigger explosion effect
            if (explosionPrefab != null) Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            LevelAudioPlayer.instance.OnPlayAudioByName("explosion-small");


            Destroy(gameObject);
        }
    }


    public void Initialize(Transform target)
    {
        this.target = target;
    }
}