using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    void Update(){

        Vector2 directionalInput = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Debug.Log("Moving in direction: " + directionalInput.x + ", "+ directionalInput.y);
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Space key pressed");
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            Debug.Log("Space key released");
        }
    }
}