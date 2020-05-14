using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    public void OnClickStartSync()
    {
        print("LOADLEVEL CALLED");
        PhotonNetwork.LoadLevel(1);
    }
}
