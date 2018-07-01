﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Player))]
public class PlayerInput : MonoBehaviour{

    [HideInInspector]
    public Player player;



    void Start(){
        player = GetComponent<Player>();
    }

    void Update(){

        Vector2 directionalInput = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);

        if(Input.GetKeyDown(KeyCode.Space)){
            player.JumpInputDown();
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            player.JumpInputUp();
        }

        if(Input.GetKey(KeyCode.Q)){
            player.ShootBubbles();
        }
    }


}