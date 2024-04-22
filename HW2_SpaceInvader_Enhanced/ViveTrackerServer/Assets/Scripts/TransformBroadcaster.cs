using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class TransformBroadcaster : MonoBehaviour
{
    public int broadcastPort = 12345; 
    private UdpClient udpClient;
    private IPEndPoint broadcastEndPoint;
    public Transform[] trackerTransform;

    void Start()
    {
        udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;

        broadcastEndPoint = new IPEndPoint(IPAddress.Broadcast, broadcastPort);
    }

    void FixedUpdate()
    {
        SendTransformData();
    }

    private void SendTransformData(){
        string transformData = "";
        for(int i = 0; i < trackerTransform.Length; i++){
            transformData += trackerTransform[i].position.x + "," + trackerTransform[i].position.y + "," + trackerTransform[i].position.z + "," + trackerTransform[i].rotation.x  + "," + trackerTransform[i].rotation.y  + "," + trackerTransform[i].rotation.z  + "," + trackerTransform[i].rotation.w;
            if(i < trackerTransform.Length -1){
                transformData += ":";
            }
        }
        //Debug.Log(transformData);
        byte[] data = Encoding.ASCII.GetBytes(transformData);

        udpClient.Send(data, data.Length, broadcastEndPoint);
    }
}
