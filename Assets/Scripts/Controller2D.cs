using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : RaycastController {
    
    public float maxSlopeAngle = 80;

    public CollisionInfo collisions;

    [HideInInspector]
    public Vector2 playerInput;


	// Use this for initialization
	public override void Start () {
		base.Start();
        collisions.faceDir = 1;
	}

    public void Move(Vector2 moveAmount, bool standingOnPlatform){
        Move(moveAmount, Vector2.zero, standingOnPlatform);
    }

    public void Move(Vector2 moveAmount, Vector2 input, bool standingOnPlatform = false)
       
    }

    // Update is called once per frame
    void Update () {
		
	}

    public struct CollisionInfo{
    
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;
        public bool descendingSlope;
        public bool slidingDownMaxSlope;

        public float slopeAngle, slopeAngleOld;
        public Vector2 slopeNormal;
        public Vector2 moveAmountOld;
        public int faceDir;
        public bool fallingThroughPlatform;

        public void Reset(){
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;
            slidingDownMaxSlope = false;

            slopeNormal = Vector2.zero;
            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
}
