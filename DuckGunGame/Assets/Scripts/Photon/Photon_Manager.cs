using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Photon_Manager : MonoBehaviourPunCallbacks 
{
    public static Photon_Manager _PHOTON_MANAGER;
    public string nickEnemy;
    private void Awake()
    {
        //Generamos singleton
        if (_PHOTON_MANAGER != null && _PHOTON_MANAGER != this)
        {
            
        }
        else
        {
            _PHOTON_MANAGER = this;
            

            //Realizo conexion
            PhotonConnect();

        }
    }

    public void PhotonConnect()
    {
        //Sincronizo la carga de la sala para todos los jugadores
        PhotonNetwork.AutomaticallySyncScene = true;

        //Conexion al servidor con la configuracion establecida
        PhotonNetwork.ConnectUsingSettings();
    }

    //Al conectarme al servidor
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conexion realizada correctamente");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    //Al desconectarme
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("He implosionado porque: " + cause);
    }

    //Al unirme el lobby
    public override void OnJoinedLobby()
    {
        Debug.Log("Accedido al Lobby");
       
            PhotonNetwork.LoadLevel("LoginRegister");
        
        
    }

    //Funcion para crear salas
    public void CreateRoom (InputField nameRoom)
    {
        PhotonNetwork.CreateRoom(nameRoom.text, new RoomOptions {MaxPlayers = 2});
        Debug.Log(nameRoom.text);   

    }

    //Funcion para unirme a salas
    public void JoinRoom (InputField nameRoom)
    {
        PhotonNetwork.JoinRoom(nameRoom.text);
    }

    //Al unirme a la sala
    public override void OnJoinedRoom()
    {
        Debug.Log("Me he unido a la Sala" + PhotonNetwork.CurrentRoom.Name + "con"
             + PhotonNetwork.CurrentRoom.PlayerCount + "Jugadores conectados a ellas");

        PhotonNetwork.LoadLevel(4);

        Debug.Log("Has joined: " + Network_Manager._NETWORK_MANAGER.GetCurrentUser().GetNickName());

        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (player.NickName != PhotonNetwork.NickName)
            {
                nickEnemy = player.NickName;

                
               
            }
        }
    }

    //Al no poderme conectar a una sala.
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("No me he podido conectar a la sala dado el error: " + returnCode + " que significa: " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       
        

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                nickEnemy = newPlayer.NickName;
                PhotonNetwork.LoadLevel("Gameplay");
            }
        }
    }
    public void LeaveCurrentRoom()
    {
        PhotonNetwork.LeaveRoom(true);
    }
}



