using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjectCharacteristics))]
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {
	Controller2D controller;
	Vector3 velocity;
	Vector2 directionalInput;
	float moveSpeed = 6f;


	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D>();
	}
	
	// Update is called once per frame
	void Update () {
		CalculateVelocity();
		controller.Move(velocity*Time.deltaTime, directionalInput);
	}

	void CalculateVelocity(){
		velocity.x = directionalInput.x * moveSpeed;
		velocity.y = directionalInput.y * moveSpeed;
	}

	public void SetDirectionalInput(Vector2 input){
		directionalInput = input;
	}
}
