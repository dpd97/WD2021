using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]

public class IP_Base_Rigidbody : MonoBehaviour
{
    #region Variables
    [Header("Rigidbody Properties")]
    [SerializeField] private float weightInKg = 0.5f;

    protected Rigidbody rb;
    protected float startDrag;
    protected float startAngularDrag;

    #endregion


    #region Main Methods
    // Update is called once per frame

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.mass = weightInKg;
            startDrag = rb.drag;
            startAngularDrag = rb.angularDrag;
        }
    }

    void FixedUpdate()
    {
        if (!rb)
        {
            return;
        }

        HandlePhysics();
    }
    #endregion

    #region Custom Methods

    protected virtual void HandlePhysics() { }

    #endregion
}
