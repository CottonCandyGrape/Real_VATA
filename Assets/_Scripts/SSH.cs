using System.IO;
using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class SSH : MonoBehaviour
{
    const string IP = "52.78.62.151";
    const int PORT = 5001;
    UdpClient udpClient = new UdpClient(IP, PORT);

    public JsonSerializationManager jsonManager;

    private float fps = 5f;
    private float targetFrameTime = 0f;
    private float elapsedTime = 0f;

    void Start()
    {
        targetFrameTime = 1f / fps;
    }

    void Update()
    {
        if (StateUpdater.isRealTimeMode)
            TimerForSimulator(targetFrameTime); //실시간으로 실물로봇으로 보낼때 
    }

    private void TimerForSimulator(float targetFrameTime)
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= targetFrameTime)
        {
            SendMotionDataWithSSH();
            elapsedTime = 0f;
        }
    }

    private void SendMotionDataWithSSH()
    {
        jsonManager.UpdateMotionDataForRobot();
        Send("mot:raw(" + jsonManager.GetJsonStringMotionDataForRobot() + ")\n"); //실시간으로 실물에 보낼때 포맷
    }

    public void Send(string rawMotion)
    {
        byte[] data = new byte[1024];
        data = Encoding.UTF8.GetBytes(rawMotion);
        udpClient.Send(data, data.Length);
    }
}
