
using System;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class UnityColliderCollisionEvent : UnityEvent<Collision> { }
public class ColliderDetector : MonoBehaviour
{

    public UnityColliderCollisionEvent OnCollisionEnterEvent;
    public UnityColliderCollisionEvent OnCollisionStayEvent;
    public UnityColliderCollisionEvent OnCollisionExitEvent;



    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        OnCollisionEnterEvent?.Invoke(other);
    }



    /// <summary>
    /// Sent each frame where a collider on another object is touching
    /// this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay(Collision other)
    {
        OnCollisionStayEvent?.Invoke(other);

    }



    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit(Collision other)
    {
        OnCollisionEnterEvent?.Invoke(other);

    }
}
