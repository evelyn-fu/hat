using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text _roomNameText;
    private Text RoomNameText
    {
        get { return _roomNameText; }
    }

    public string RoomName { get; private set; }
    
    public bool Updated { get; set; }
    // Start is called before the first frame update
    private void Start()
    {
        GameObject LobbyCanvasObject = MainCanvasManager.Instance.LobbyCanvas.gameObject;
        if(LobbyCanvasObject == null)
        {
            return;
        }

        LobbyCanvas lobbyCanvas = LobbyCanvasObject.GetComponent<LobbyCanvas>();

        Button button = GetComponent<Button>();
        if (button == null)
        {
            print("button is null");
        }
        button.onClick.AddListener(() => lobbyCanvas.OnClickJoinRoom(RoomNameText.text));
    }

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        
        //button.onClick.RemoveAllListeners();
    }
    
    public void SetRoomNameText(string text)
    {
        print("roomnametext call");
        RoomName = text;
        RoomNameText.text = RoomName;
    }

}
