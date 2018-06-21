using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCharacteristics : MonoBehaviour {

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;

    public float moveSpeed = 6;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;

    public float gravity;
    public float minJumpVelocity;
    public float maxJumpVelocity;

    void Start(){
        gravity = -(2*maxJumpHeight) / Mathf.Pow(timeToJumpApex,2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2*Mathf.Abs(gravity) *minJumpHeight);
    }


}



