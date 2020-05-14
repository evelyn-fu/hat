using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    public void OnClickStartSync()
    {
        print("LOADLEVEL CALLED");
        if (!PhotonNetwork.automaticallySyncScene)
            print("not true");
        PhotonNetwork.LoadLevel(1);
    }
}
