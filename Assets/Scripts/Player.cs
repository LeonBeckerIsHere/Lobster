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
		controller.Move(velocity*Time.deltaTime, directionalInput);
	}

	void CalculateVelocity(){
		velocity.x = directionalInput.x * objCharacteristics.moveSpeed;
		velocity.y = objCharacteristics.gravity * Time.deltaTime;
	}

	public void SetDirectionalInput(Vector2 input){
		directionalInput = input;
	}
}
