﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Photon.MonoBehaviour
{
    private PhotonView PhotonView;
    private Vector3 TargetPosition;
    private Quaternion TargetRotation;

    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (PhotonView.isMine)
            checkInput();
        else
            SmoothMove();
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            TargetRotation = (Quaternion)stream.ReceiveNext();
            TargetPosition = (Vector3)stream.ReceiveNext();
        }
    }

    private void SmoothMove()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.25f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, 500 + Time.deltaTime);
    }

    private void checkInput()
    {
        float moveSpeed = 50f;
        float rotateSpeed = 400f;

        float vertical = Input.GetAxis("Vertical");
        float horizonal = Input.GetAxis("Horizontal");

        transform.position -= transform.forward * (vertical * moveSpeed * Time.deltaTime);

        transform.Rotate(new Vector3(0, horizonal * rotateSpeed * Time.deltaTime, 0));
    }
}
