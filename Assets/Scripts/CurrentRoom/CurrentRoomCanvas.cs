using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    public void OnClickStartSync()
    {
        print("LOADLEVEL CALLED");
        if (PhotonNetwork.automaticallySyncScene)
            print("true");
        PhotonNetwork.LoadLevel(1);
    }
}
