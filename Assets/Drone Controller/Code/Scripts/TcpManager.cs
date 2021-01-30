using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TcpManager : MonoBehaviour
{

    public TCPClient tcp;
    string message;
    public RenderTexture droneCam;
    public string[] msgparts = new string[1];
    public float detectionOutput;

    // Start is called before the first frame update
    void Start()
    {
        tcp.ConnectToTcpServer();

    }

    // Update is called once per frame
    void Update()
    {
        tcp.SendTcpImage(droneCam);
        DecodeMessage();

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            tcp.SendTcpImage(droneCam);
        }*/
    }

    void DecodeMessage()
    {
        message = tcp.serverMessage;

        msgparts = message.Split(',');

        if (msgparts.Length == 1)
        {
            detectionOutput = float.Parse(msgparts[0]);
        }

        /*if (msgparts.Length == 3)
        {
            theta = float.Parse(msgparts[0]);
            fi = float.Parse(msgparts[1]);
            detect = float.Parse(msgparts[2]);

        }
        else
        {
            Debug.Log("message not recieved");
        } */

    }
}
