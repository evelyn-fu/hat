using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;

        PlayerName = "Steve#" + Random.Range(1000, 9999);
    }
}
