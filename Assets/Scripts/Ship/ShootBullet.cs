using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;


public class ShootBullet : NetworkBehaviour
{

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    void Update()
    {
        if (isLocalPlayer && Input.GetKey(KeyCode.Space))
        {
            //this.RpcShoot();
            //supply the position is because of timing
            this.CmdShoot(transform.position);
        }
    }

    //commands only run ON server
    //we need RPC - remote procedure call - Server calls it to run ON client
    //remove bullet from registered spanable in netorkManger
    //bullet needs player authority

    [ClientRpc]
    void RpcClientShot(Vector3 position)
    {
        GameObject bullet = Instantiate(_bulletPrefab, position, Quaternion.identity);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * _bulletSpeed;
        Destroy(bullet, 1.0f);
    }


    [Command]
    void CmdShoot(Vector3 position)
    {
        //GameObject bullet = Instantiate(_bulletPrefab, this.transform.position, Quaternion.identity);
        //bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * _bulletSpeed;
        //NetworkServer.Spawn(bullet);
        //Destroy(bullet, 0.3f);

        //^^^^OLD^^^^^^^^^^^

        //we re-write this
        //tell client to spawn bullets
        RpcClientShot(position);
    }

    //[ClientRpc]
    //void RpcShoot()
    //{
    //    GameObject bullet = Instantiate(_bulletPrefab, this.transform.position, Quaternion.identity);
    //    bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * _bulletSpeed;
    //    //NetworkServer.Spawn(bullet);
    //    Destroy(bullet, 0.3f);
    //}
}
