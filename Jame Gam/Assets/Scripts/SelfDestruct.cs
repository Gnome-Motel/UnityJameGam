using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
