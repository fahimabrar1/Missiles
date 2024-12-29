using System;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour,IMissile
{
     public float speed = 20f; // Missile's movement speed
    public float steerSpeed = 5f; // Steering speed
    public Transform target; // The target (e.g., the plane)
    public GameObject explosionPrefab; // Particle effect for explosion
    public AudioClip explosionSound; // Sound effect for explosion

    private Rigidbody2D _rb;
    public Action<HomingMissile> OnMissileDestroyed;

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
        if (target is null) return; // If no target, do nothing

        // Direction to the target
        var directionToTarget = (Vector2)target.position - _rb.position;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform != target) return;

        MyDebug.Log("Missile hit the target!");
        
        // Trigger explosion effect
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    
        // Play explosion sound
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        Destroy(gameObject);
    }

  
    public void Initialize(Transform target)
    {
        this.target = target;
    }
    

    private void OnDestroy()
    {
        OnMissileDestroyed?.Invoke(this);
    }
  
}
