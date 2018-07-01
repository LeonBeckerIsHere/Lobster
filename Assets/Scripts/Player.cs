using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjectCharacteristics))]
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {
    Controller2D controller;
    ObjectCharacteristics objCharacteristics;
    Vector3 velocity;
    Vector2 directionalInput;


    // Use this for initialization
    void Start () {
        controller = GetComponent<Controller2D>();
        objCharacteristics = GetComponent<ObjectCharacteristics>();
    }

    // Update is called once per frame
    void Update () {
        CalculateVelocity();
        HandleWallSliding();

        controller.Move(velocity*Time.deltaTime, directionalInput);
        if(controller.collisions.above || controller.collisions.below){
            velocity.y = 0;
        }

    }

    void CalculateVelocity(){
        velocity.x = directionalInput.x * objCharacteristics.moveSpeed;
        velocity.y += objCharacteristics.gravity * Time.deltaTime;
    }

    public void SetDirectionalInput(Vector2 input){
        directionalInput = input;
    }

    public void JumpInputDown(){
        if(objCharacteristics.wallSliding){
            Debug.Log(directionalInput.x);
            if(objCharacteristics.wallDirX == directionalInput.x){
                velocity.x = -objCharacteristics.wallDirX * objCharacteristics.wallLeap.x;
                velocity.y = objCharacteristics.wallJumpClimb.y;
            }
            else if (directionalInput.x == 0){
                velocity.x = -objCharacteristics.wallDirX * objCharacteristics.wallJumpOff.x;
                velocity.y = objCharacteristics.wallJumpOff.y;
            }
            else{
                velocity.x = -objCharacteristics.wallDirX * objCharacteristics.wallLeap.x;
                velocity.y = objCharacteristics.wallLeap.y;
            }
        }
        if(controller.collisions.below){
            velocity.y = objCharacteristics.maxJumpVelocity;
        }
    }
    public void JumpInputUp(){
        if(velocity.y > objCharacteristics.minJumpVelocity){
            velocity.y = objCharacteristics.minJumpVelocity;
        }
    }

    void HandleWallSliding(){
        objCharacteristics.wallDirX = (controller.collisions.left)?-1:1;
        objCharacteristics.wallSliding = false;
        if((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0){
            objCharacteristics.wallSliding = true;

            if(velocity.y < -objCharacteristics.wallSlideSpeedMax){
                velocity.y = -objCharacteristics.wallSlideSpeedMax;
            }

            if(objCharacteristics.timeToWallUnstick > 0){
                velocity.x = 0;

                if(directionalInput.x != objCharacteristics.wallDirX & directionalInput.x != 0){
                    objCharacteristics.timeToWallUnstick -= Time.deltaTime;
                }
                else{
                    objCharacteristics.timeToWallUnstick = objCharacteristics.wallStickTime;
                }
                
            }
            else{
                    objCharacteristics.timeToWallUnstick = objCharacteristics.wallStickTime;
            }
        }
    }
}
