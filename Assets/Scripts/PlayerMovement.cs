using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerMovement : Photon.MonoBehaviour
{
    private PhotonView PhotonView;
    private Vector3 TargetPosition;
    private Quaternion TargetRotation;
    bool isJumping = false;
    private bool hasHat = true;
    public Camera cam;
    GameObject hpBar;
    Vector3 localScale;
    private float hp=30;

    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
        hpBar = gameObject.transform.Find("hp bar/green square").gameObject;
        localScale = hpBar.transform.localScale;
        if (!PhotonView.isMine)
        {
            cam.enabled = false;
        }
        else
        {
            gameObject.transform.Find("hp bar").gameObject.SetActive(false);
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
            stream.SendNext(hasHat);
        }
        else
        {
            TargetPosition = (Vector3)stream.ReceiveNext();
            TargetRotation = (Quaternion)stream.ReceiveNext();
            hasHat = (bool)stream.ReceiveNext();
        }
    }

    private void SmoothMove()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.25f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, 500 + Time.deltaTime);
        transform.Find("Head/Hat").gameObject.SetActive(hasHat);
        localScale.x = 0.9f * hp / 30.0f;
        hpBar.transform.localScale = localScale;
    }

    private void checkInput()
    {
        float moveSpeed = 25f;
        float rotateSpeed = 200f;

        float vertical = Input.GetAxis("Vertical");
        float horizonal = Input.GetAxis("Horizontal");

        transform.position -= transform.forward * (vertical * moveSpeed * Time.deltaTime);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + horizonal * rotateSpeed * Time.deltaTime, 0);

        //jump
        if (Input.GetKeyDown("space") && !isJumping)
        {
            isJumping = true;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 8f, 0), ForceMode.Impulse);
        }

        //throw hat
        if (Input.GetKeyDown(KeyCode.Q) && hasHat)
        {
            hasHat = false;
            transform.Find("Head/Hat").gameObject.SetActive(hasHat);
            GameObject newHat = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Hat"), transform.position-transform.forward*2f+transform.up*1f, transform.rotation, 0);
            newHat.GetComponent<Rigidbody>().AddForce(-transform.forward * 50f, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            isJumping = false;
        }
        if(collision.gameObject.tag == "hat")
        {
            print("hit");
            PlayerManagement.Instance.ModifyHealth(photonView.owner, -10);
            hp -= 10;
            if(collision.gameObject != null)
                PhotonNetwork.Destroy(collision.gameObject);
        }
    }
}
