using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityStandardAssets.Utility;
using System;

public class Traversal : MonoBehaviourPunCallbacks, IPunObservable
{
    int speed = 2;
    int rotate = 120;

    private Transform tr;

    void Start()
    {
       tr = GetComponent<Transform>();
        if (photonView.IsMine)
        {
            Camera.main.GetComponent<SmoothFollow>().target = tr.Find("CamTarget").transform;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void Update()
    {
        if (photonView.IsMine) //자신의 플레이어만 키보드 조작허용
        {
            float move_speed = speed * Time.deltaTime;
            float rotate_speed = rotate * Time.deltaTime;

            float input_value_about_up_and_down = Input.GetAxis("Vertical");
            float input_value_about_left_and_right = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.forward * move_speed * input_value_about_up_and_down);
            transform.Rotate(Vector3.up * rotate_speed * input_value_about_left_and_right);
        }

        else
        {
            if ((tr.position - NowPos).sqrMagnitude >= 10.0f * 10.0f)
            {
                tr.position = NowPos;
                tr.rotation = NowRot;
            }
            else
            {
                tr.position = Vector3.Lerp(tr.position, NowPos, Time.deltaTime * 10.0f);
                tr.rotation = Quaternion.Slerp(tr.rotation, NowRot, Time.deltaTime * 10.0f);
            }
        }
    }

    private Vector3 NowPos;
    private Quaternion NowRot;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else
        {
            NowPos = (Vector3)stream.ReceiveNext();
            NowRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
