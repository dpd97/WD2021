using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WarwickIEngine 
{
    void InitEngine();
    void UpdateEngine(Rigidbody rb, IP_Drone_Inputs input);
}
