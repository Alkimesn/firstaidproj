using Assets.Scripts.newScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeadController : MonoBehaviour
{
    public float click_delay = 1;
    float current_delay = 0;
    public List<string> tools=new List<string>();
    int currentTool = 0;
    public Text text;

    Transform headTransform;
    PlayerController playerController;
    int layermask = 1 << 8;
    // Start is called before the first frame update
    void Start()
    {
        headTransform = GetComponent<Transform>();
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }

    public string ConnectToServer(string msg)
    {
        try
        {
            int port = 8081;
            string address = "127.0.0.1";
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // подключаемся к удаленному хосту
            socket.Connect(ipPoint);
            byte[] data = Encoding.Unicode.GetBytes(msg);
            socket.Send(data);

            // получаем ответ
            data = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт

            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);

            // закрываем сокет
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return builder.ToString();
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");
        headTransform.Rotate(0, horizontal, 0, Space.World);
        headTransform.Rotate(0, 0, vertical, Space.Self);
        playerController.faceDirection = Quaternion.Euler(0, horizontal, 0) * playerController.faceDirection;

        if(current_delay < Time.deltaTime) { current_delay = 0; }
        else { current_delay -= Time.deltaTime; }

        if(((Input.GetAxis("Fire1")!=0)) && current_delay == 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, layermask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
                Debug.Log("Did Hit: "+ hit.transform.name);
                PatientInputController click = hit.transform.gameObject.GetComponent<PatientInputController>();
                if (Input.GetAxis("Fire1") != 0)
                {
                    click.Activate(hit.distance,tools[currentTool]);
                }
                current_delay = click_delay;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            currentTool = (currentTool + 1) % tools.Count;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            currentTool = (currentTool+tools.Count - 1) % tools.Count;
        }
        text.text = tools[currentTool];
    }
}
