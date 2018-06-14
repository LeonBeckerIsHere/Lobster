using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour {

	[HideInInspector]
	public Vector2 playerInput;

	public void Move(Vector2 mA, Vector2 input)
	{
		transform.Translate(mA);
	}
}
