
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UnityColliderTriggerEvent : UnityEvent<Collider> { }

public class TriggerDetector : MonoBehaviour
{
    public UnityColliderTriggerEvent OnTriggerEnterEvent;
    public UnityColliderTriggerEvent OnTriggerStayEvent;
    public UnityColliderTriggerEvent OnTriggerExitEvent;


    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }


    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {

        OnTriggerStayEvent?.Invoke(other);
    }



    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other);
    }
}
