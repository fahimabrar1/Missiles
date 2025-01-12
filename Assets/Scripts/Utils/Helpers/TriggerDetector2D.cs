using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UnityColliderTrigger2DEvent : UnityEvent<Collider2D>
{
}

public class TriggerDetector2D : MonoBehaviour
{
    public UnityColliderTrigger2DEvent OnTriggerEnterEvent;
    public UnityColliderTrigger2DEvent OnTriggerExitEvent;
    public UnityColliderTrigger2DEvent OnTriggerStayEvent;


    /// <summary>
    ///     Sent when another object enters a trigger collider attached to this
    ///     object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }


    /// <summary>
    ///     Sent when another object leaves a trigger collider attached to
    ///     this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExitEvent?.Invoke(other);
    }

    /// <summary>
    ///     Sent each frame where another object is within a trigger collider
    ///     attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerStayEvent?.Invoke(other);
    }
}