using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GalleryManage: MonoBehaviourPunCallbacks
{
    void Start()
    {
        CreateUser();
        PhotonNetwork.IsMessageQueueRunning = true;
    }

    void CreateUser()
    {
        Transform[] points = GameObject.Find("PointGroup").GetComponentsInChildren<Transform>();

        int Arr = Random.Range(1, points.Length);

        PhotonNetwork.Instantiate("User", points[Arr].position, Quaternion.identity);
    }

    public void onExitClick()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("InterfaceScene");
    }
}
