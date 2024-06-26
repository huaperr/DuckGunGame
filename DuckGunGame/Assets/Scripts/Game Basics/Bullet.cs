using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bullet : MonoBehaviourPun
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
    }

 

    
    [PunRPC]
    void NetworkDestroy()
    {
        Destroy(this.gameObject);
    }
    [PunRPC]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerController>().Damage();
        pv.RPC("NetworkDestroy", RpcTarget.All);
        Destroy(gameObject);
    }

}

