using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[RequireComponent(typeof(IP_Drone_Inputs))]

public class IP_Controller : IP_Base_Rigidbody
{

    #region Variable
    [Header("Control Properties")]
    [SerializeField] private float minMaxPitch = 30f;
    [SerializeField] private float minMaxRoll = 30f;
    [SerializeField] private float yawPower = 4f;
    [SerializeField] private float lerpSpeed = 2f;

    private IP_Drone_Inputs input;
    private List<WarwickIEngine> engines = new List<WarwickIEngine>();

    private float finalPitch;
    private float finalRoll;
    private float finalYaw;
    private float yaw;

    #endregion

    #region Main Method

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<IP_Drone_Inputs>();
        engines = GetComponentsInChildren<WarwickIEngine>().ToList <WarwickIEngine>();
    }

    #endregion

    #region Custom Methods
    protected override void HandlePhysics()
    {

        HandleEngines();
        HandleControls();

    }

    protected virtual void HandleEngines()
    {
        //rb.AddForce(Vector3.up * rb.mass * Physics.gravity.magnitude);

        foreach (WarwickIEngine engine in engines)
        {
            engine.UpdateEngine(rb, input);
        }
    }

    protected virtual void HandleControls()
    {
        float pitch = input.Cyclic.y * minMaxPitch;
        float roll = input.Cyclic.x * minMaxRoll;
        yaw += input.Pedals * yawPower;

        finalPitch = Mathf.Lerp(finalPitch, pitch, Time.deltaTime * lerpSpeed);
        finalRoll = Mathf.Lerp(finalRoll, roll, Time.deltaTime * lerpSpeed);
        finalYaw = Mathf.Lerp(finalYaw, yaw, Time.deltaTime * lerpSpeed);

        Quaternion rot = Quaternion.Euler(finalPitch, finalYaw, finalRoll);
        rb.MoveRotation(rot);
    }

    #endregion
}

