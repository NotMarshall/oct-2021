using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;

    //to rotate player with mouse
    public Transform cam;

   
    public float speed = 6f;

    //to smooth the player rotation
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if(direction.magnitude >= 0.1f)
        {

            // <> to change the player face in the diretion of movement
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg  /* now to change the plager angle with mouse*/ + cam.eulerAngles.y;
                    // <>to smooth the rotation transtion
                     float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    // </>

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //</>

            //to move in the direction of camera
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);    
        }
    }
}
