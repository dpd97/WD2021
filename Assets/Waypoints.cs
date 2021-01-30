using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Move move;
    public float waypoint;
    public Vector3 WP1;
    public Vector3 WP2;
    public Vector3 WP3;

    // Start is called before the first frame update
    void Start()
    {
        waypoint = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoint == 0)
        {
            move.posTarget = WP1;
            waypoint += move.waypoint;
            move.waypoint = 0;
        }
        else if (waypoint == 1)
        {
            move.posTarget = WP2;
            waypoint += move.waypoint;
            if (move.waypoint == 1)
            {
                move.rotationleft = 360;
            }   
            move.scanDetection = 0;
            move.waypoint = 0;
        }
        else if (waypoint == 2)
        {
            move.posTarget = WP3;
            waypoint += move.waypoint;
            if (move.waypoint == 1)
            {
                move.rotationleft = 360;
            }
            move.rotationleft = 360;
            move.scanDetection = 0;
            move.waypoint = 0;
        }
    }
}
