using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class MoveShip : NetworkBehaviour
{

    [SerializeField] private float speed;

    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            float movement = Input.GetAxis("Horizontal");
            GetComponent<Rigidbody2D>().velocity = new Vector2(movement* speed, 0f);
        }
    }

}
