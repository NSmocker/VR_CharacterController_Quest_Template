using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    public VRCharControllerMovementSystem movement_system;
    public Vector2 base_input;


    void ReadInput() 
    {
        base_input.x = Input.GetAxis("Horizontal");
        base_input.y = Input.GetAxis("Vertical");

    }
    void SendInput() 
    {
        movement_system.movement_input = base_input;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
        SendInput();

    }
}
