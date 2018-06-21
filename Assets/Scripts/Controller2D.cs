using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : RaycastController {

    public CollisionInfo collisions;
    [HideInInspector]
    public Vector2 playerInput;

    public override void Start(){
        base.Start();
        collisions.faceDir=1;
    }

    public void Move(Vector2 mA, bool grounded){

        Move(mA, Vector2.zero, grounded);
    }

    public void Move(Vector2 moveAmount, Vector2 input, bool grounded = false){
        UpdateRayOrigins();

        collisions.Reset();
        collisions.moveAmountOld = moveAmount;
        playerInput = input;

        if(moveAmount.x != 0){
            collisions.faceDir = (int)Mathf.Sign(moveAmount.x);
        }

        HorizontalCollisions(ref moveAmount);

        if(moveAmount.y != 0){
            VerticalCollisions(ref moveAmount);
        }

        transform.Translate(moveAmount);

        if(grounded){
            collisions.below = true;
        }
    }

    void HorizontalCollisions(ref Vector2 moveAmount){
        float directionX = collisions.faceDir;
        float magnitudeX = Mathf.Abs(moveAmount.x);
        float rayLength;

        if(magnitudeX < skinWidth){
            rayLength = 2*skinWidth;
        }
        else{
            rayLength = magnitudeX + skinWidth;
        }

        for(int i = 0; i < horizontalRayCount; ++i){
            Vector2 rayOrigin = (directionX == -1)? boundVertices.bottomLeft: boundVertices.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if(hit){
                if(hit.distance == 0){
                    continue;
                }

                moveAmount.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = (directionX == -1);
                collisions.right = (directionX == 1);
            }
        }
    }

    void VerticalCollisions(ref Vector2 moveAmount){
        float directionY = Mathf.Sign(moveAmount.y);
        float magnitudeY = Mathf.Abs(moveAmount.y);
        float rayLength = magnitudeY + skinWidth;
    
        for (int i = 0; i < verticalRayCount; ++i)
        {
            Vector2 rayOrigin = (directionY == -1) ? boundVertices.bottomLeft : boundVertices.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                if(hit.collider.tag == "Through"){
                    if(directionY == 1 || hit.distance == 0){
                        continue;
                    }
                    if(collisions.fallingThroughPlatform){
                        continue;
                    }
                    if(playerInput.y == -1){
                        collisions.fallingThroughPlatform = true;
                        Invoke("ResetFallingThroughPlatform",0.5f);
                        continue;
                    }
                }

                moveAmount.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }
    }

    void ResetFallingThroughPlatform(){
        collisions.fallingThroughPlatform = false;
    }

    public struct CollisionInfo{
        public bool above, below, left, right;
        public bool fallingThroughPlatform;

        public int faceDir;

        public Vector2 moveAmountOld;
		
        public void Reset(){
        above = below = left = right = false;
        }
    }
}
