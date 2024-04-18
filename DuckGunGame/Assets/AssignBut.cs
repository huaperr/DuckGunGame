using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;

public class AssignButt : MonoBehaviour
{

    public Button createButt;
    public Button joinButt;
    // Start is called before the first frame update
    void Start()
    {
        createButt = GameObject.Find("Create room").GetComponent<Button>();
        joinButt = GameObject.Find("JoinRomm").GetComponent<Button>();

        createButt.clicked += method;
        joinButt.clicked += method;

    }

    void method()
    {
        PhotonNetwork.LoadLevel(4);
    }
}
