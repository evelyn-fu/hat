using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Photon.MonoBehaviour
{
    private PhotonView PhotonView;
    private Vector3 TargetPosition;
    private Quaternion TargetRotation;
    bool isJumping = false;
    public Camera cam;

    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
        if (!PhotonView.isMine)
        {
            cam.enabled = false;
        }
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
            TargetPosition = (Vector3)stream.ReceiveNext();
            TargetRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    private void SmoothMove()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.25f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, 500 + Time.deltaTime);
    }

    private void checkInput()
    {
        float moveSpeed = 25f;
        float rotateSpeed = 200f;

        float vertical = Input.GetAxis("Vertical");
        float horizonal = Input.GetAxis("Horizontal");

        transform.position -= transform.forward * (vertical * moveSpeed * Time.deltaTime);

        //transform.Rotate(new Vector3(0, horizonal * rotateSpeed * Time.deltaTime, 0));
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + horizonal * rotateSpeed * Time.deltaTime, 0);

        //jump
        if (Input.GetKeyDown("space") && !isJumping)
        {
            isJumping = true;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 8f, 0), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            print("floor");
            isJumping = false;
        }
    }
}
