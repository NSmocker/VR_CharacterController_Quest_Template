using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCharControllerMovementSystem : MonoBehaviour
{

    public CharacterController character_controller;
    public float move_speed;

    public float jump_force;
    public Vector2 movement_input;
    public TwoDDirectionPointer direction_pointer;

    public float gravity;
    public float gravity_multi;

    public bool is_grounded;
    public void GroundCheck()
    {
        LayerMask layer_mask = LayerMask.GetMask("Default");
        is_grounded = Physics.CheckSphere(character_controller.transform.position + character_controller.center, character_controller.radius+ character_controller.skinWidth +  0.01f, layer_mask); // Physics.OverlapSphere(character_controller.transform.position + character_controller.center, character_controller.radius + 0.01f, layer_mask).Length > 1;
    }
    public void ApplyGravity()
    {
        GroundCheck();
        var direction_and_speed = new Vector3(0, gravity, 0) * Time.deltaTime;
        if (is_grounded) { if (Input.GetButtonDown("Jump")){ gravity = jump_force; } }
        if (!is_grounded) { gravity -= 9.8f*Time.deltaTime* gravity_multi; }


        character_controller.Move(direction_and_speed);

    }
    public void MoveByPointer() 
    {
        var direction_and_speed = direction_pointer.transform.TransformDirection(new Vector3(movement_input.x, 0, movement_input.y)*move_speed*Time.deltaTime);
        character_controller.Move(direction_and_speed);
    }
   
    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(character_controller.transform.position + character_controller.center, character_controller.radius + character_controller.skinWidth + 0.01f);
    }

    public void PositionController() 
    {
        float head_height = Mathf.Clamp(Camera.main.transform.localPosition.y, 1, 2);
        character_controller.height = head_height;
        Vector3 newCenter = Vector3.zero;
        newCenter.y = character_controller.height / 2;
        newCenter.y += character_controller.skinWidth;
        newCenter.x = Camera.main.transform.localPosition.x;
        newCenter.y = Camera.main.transform.localPosition.y;
        character_controller.center = newCenter;
    }


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        PositionController();
        if (movement_input != Vector2.zero) MoveByPointer();
     
    }
}
