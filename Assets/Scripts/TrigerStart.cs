using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class TrigerStart : MonoBehaviour
{
    [SerializeField] UnityEvent<Collider> OnEnter, OnExit, OnStay;
    private void OnTriggerEnter(Collider other)
    {
        OnEnter.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnStay.Invoke(other);
    }
}
