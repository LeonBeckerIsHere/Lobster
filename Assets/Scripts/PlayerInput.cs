using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	public Player player;

	void Start(){
		player = GetComponent<Player>();
	}

	void Update(){

        Vector2 directionalInput = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Space key pressed");
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            Debug.Log("Space key released");
        }
    }
}