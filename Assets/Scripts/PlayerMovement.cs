using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Photon.MonoBehaviour
{
    private PhotonView PhotonView;

    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(PhotonView.isMine)
            checkInput();
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
