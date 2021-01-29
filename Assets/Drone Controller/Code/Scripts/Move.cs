using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // public Quarternion target;
    public Vector3 rotTarget;
    //public Vector3 pos;
    public Vector3 posTarget;
    float speed = 1f;
    public GameObject WarwickDrone;
    float rotationleft = 360;
    float rotationspeed = 10;

    private Vector3 diff;

    // Update is called once per frame
    void Update()

    {

        if (posTarget != transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, posTarget, Time.deltaTime * speed);
        }
        else 
           Debug.Log("no update this frame");

        diff = posTarget - transform.position;

        
        if (diff.y < 0.1 && diff.x < 0.1 && diff.z < 0.1)
        {
            float rotation = rotationspeed * Time.deltaTime;
            if (rotationleft > rotation)
            {
                rotationleft -= rotation;
            }
            else
            {
                rotation = rotationleft;
                rotationleft = 0;
            }
            WarwickDrone.transform.Rotate(0, rotation, 0);

            //WarwickDrone.transform.Rotate(rotTarget, Space.Self);
        }

    }

    IEnumerator Rotate(float duration)
    {
        Quaternion startRot = transform.rotation;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.rotation = startRot * Quaternion.AngleAxis(t / duration * 360f, Vector3.right); //or transform.right if you want it to be locally based
            yield return null;
        }
        transform.rotation = startRot;
    }


}

