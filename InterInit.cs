using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class InterInit : MonoBehaviourPunCallbacks {
    private string gameVersion = "1.0";
    public string userId = "GO";
    public byte maxPlayer = 20;

    public GameObject enter;
    public Camera camera;

    public float sensitivityX = 15.0f;
    public float sensitivityY = 15.0f;

    public float minimumY = -60.0f;
    public float maximumY = 60;

    float rotationX = 0.0f;
    float rotationY = 0.0f;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()   
    {
        enter = GameObject.Find("Enter"); //Interface Buttonobject Enter
        ConnectToServer();
    }

    private void ConnectToServer()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    #region SELF_CALLBACK_FUNCTIONS
    void Update()
    {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            rotationX += Input.GetAxis("Mouse X") * sensitivityX;

            camera.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit_information;
            if (Physics.Raycast(ray, out hit_information, float.MaxValue))
            {
                if (hit_information.collider.name == "Enter")
                {
                    OnConnectedToMaster();
                    OnJoinedRoom();
                }
                else
                {
                    Application.Quit();
                }
            }
        }

    public void OnCreateRoomClick() 
    {
        PhotonNetwork.CreateRoom("01", new RoomOptions { MaxPlayers = this.maxPlayer });
    }

    public void OnJoinRandomRoomClick()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion

    #region PHOTON_CALLBACK_FUNCTIONS
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect To Master");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Connection Failed...");
        PhotonNetwork.CreateRoom("", new Photon.Realtime.RoomOptions { MaxPlayers = this.maxPlayer });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Gallery Connected!");
        PhotonNetwork.IsMessageQueueRunning = false;
        SceneManager.LoadScene("GalleryScene");
    }
    #endregion
}