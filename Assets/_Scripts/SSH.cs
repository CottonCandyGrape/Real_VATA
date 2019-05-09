using System.IO;
using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class SSH : MonoBehaviour
{
    public JsonSerializationManager jsonManager;
    //public bool realTimeStreaming = false;

    private float fps = 5f;
    private float targetFrameTime = 0f;
    private float elapsedTime = 0f;

    const string IP = "52.78.62.151";
    const int PORT = 5001;
    UdpClient udpClient = new UdpClient(IP, PORT);

    void Start()
    {
        targetFrameTime = 1f / fps;
    }

    void Update()
    {
        //if (realTimeStreaming)
        TimeCounter(targetFrameTime);
    }

    private void TimeCounter(float targetFrameTime)
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
        jsonManager.UpdateMotionData();
        Send("mot:raw(" + jsonManager.UpdateJsonString() + ")\n"); //모션 파일 저장시 공백줄이 생겨서 로봇으로 보낼때만 줄바꿈 추가.
    }

    private void Send(string rawMotion)
    {
        byte[] data = new byte[1024];
        data = Encoding.UTF8.GetBytes(rawMotion);
        udpClient.Send(data, data.Length);
    }
}
