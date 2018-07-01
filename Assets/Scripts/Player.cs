using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjectCharacteristics))]
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

    public GameObject bubbleRef;
    public float bubbleRate;
    float timeTillNextBubble;

    Controller2D controller;
    ObjectCharacteristics objCharacteristics;
    Vector3 velocity;
    Vector2 directionalInput;
    float dirX;

    // Use this for initialization
    void Start () {
        controller = GetComponent<Controller2D>();
        objCharacteristics = GetComponent<ObjectCharacteristics>();
        timeTillNextBubble = bubbleRate;
        dirX = 1;
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
        if(input.x != 0)
            dirX = input.x;
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

    public void ShootBubbles(){
        if(timeTillNextBubble <= 0){
            GameObject b1 = Instantiate(bubbleRef, transform.position, transform.rotation);
            GameObject b2 = Instantiate(bubbleRef, transform.position, transform.rotation);
            GameObject b3 = Instantiate(bubbleRef, transform.position, transform.rotation);

            b1.SendMessage("TrueStart", dirX);
            b2.SendMessage("TrueStart", dirX);
            b3.SendMessage("TrueStart", dirX);

            timeTillNextBubble = bubbleRate;
        }
        else{
            timeTillNextBubble -= Time.deltaTime;
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
