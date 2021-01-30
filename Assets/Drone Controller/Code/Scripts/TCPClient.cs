using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TCPClient : MonoBehaviour
{
    #region private members 	
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    #endregion

    public string ipAdress = "192.168.1.75";
    public int port = 1234;
    public string serverMessage;

    /// <summary> 	
    /// Setup socket connection. 	
    /// </summary> 	
    public void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }

    /// <summary> 	
    /// Runs in background clientReceiveThread; Listens for incomming data. 	
    /// </summary>     
    private void ListenForData()
    {
        try
        {
            socketConnection = new TcpClient(ipAdress, port);
            Byte[] bytes = new Byte[1024];
            
            while (true)
            {
                // Get a stream object for reading 				
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        // Convert byte array to string message. 						
                         serverMessage = Encoding.ASCII.GetString(incommingData);
                        //Debug.Log("server message received as: " + serverMessage);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    /// <summary> 	
    /// Send message to server using socket connection. 	
    /// </summary> 	
    public void SendTcpMessage(string clientMessage)
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                // Write byte array to socketConnection stream.                 
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Message sent " + clientMessage);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    public void SendTcpImage(RenderTexture rt)
    {
        var oldRT = RenderTexture.active;

        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {

                // Convert string message to byte array.
                var tex = new Texture2D(rt.width, rt.height);
                RenderTexture.active = rt;

                // reads pixels from C# ?
                tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
                tex.Apply();

                // Encodes texture2D to bytes and gets length 
                byte[] bytes = tex.EncodeToPNG();
                int intValue = bytes.Length;

                // Converts image array length into bytes 
                byte[] intBytes = BitConverter.GetBytes(intValue);
                Array.Reverse(intBytes);
                byte[] msgSize = intBytes;

                // Concatinates 
                byte[] data = new byte[msgSize.Length + bytes.Length];
                Buffer.BlockCopy(msgSize, 0, data, 0, msgSize.Length);
                Buffer.BlockCopy(bytes, 0, data, msgSize.Length, bytes.Length);

                // Write byte array to socketConnection stream.                 
                //stream.Write(bytes, 0, bytes.Length);
                stream.Write(data, 0, data.Length);

                //Debug.Log("Image sent, length = " + bytes.Length + " together with protocol," +
                 //  "total length: " + data.Length);

            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    public static byte[] Combine(byte[] first, byte[] second)
    {
        byte[] ret = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, ret, 0, first.Length);
        Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
        return ret;
    }
    
    public void CloseConnection()
    {
        if (socketConnection != null)
            socketConnection.Close();
    }

    private void OnDisable()
    {
        if(socketConnection != null)
            socketConnection.Close();
    }

    private void OnDestroy()
    {
        if (socketConnection != null)
            socketConnection.Close();
    }
}