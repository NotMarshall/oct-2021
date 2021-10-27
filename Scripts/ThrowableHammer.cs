using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableHammer : MonoBehaviour
{
    public Rigidbody axe;
    public float throwforce = 50f;

    // to return the hammer (with a curve)
    public Transform target, curve_point;
    private Vector3 old_pos;
    public bool isReturning = false;
    private float time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Fire1"))
        {
            ThrowAxe();
        }

        if(Input.GetButtonUp("Fire2"))
        {
            ReturnAxe();
        }

        if(isReturning)
        //Returning calculation
        {
            if(time < 1.0f)
            {
                axe.position = getBezierQuadraticCurvePoint(time, old_pos, curve_point.position, target.position);
                //for smooth rotation
                axe.rotation =Quaternion.Slerp(axe.transform.rotation, target.rotation, 50 * Time.deltaTime);
                time += Time.deltaTime;
            }
            else
            {
                ResetAxe();
            }
        }
    }
    //Throw
    void ThrowAxe()
    {
        isReturning = false;
        axe.transform.parent = null;
        axe.isKinematic = false;
        axe.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwforce, ForceMode.Impulse);
    }
    //Return
    void ReturnAxe()
    {
        time = 0.0f;
        old_pos = axe.position;
        isReturning = true;
        axe.velocity = Vector3.zero;
        axe.isKinematic = true;
    }
    //Reset
    void ResetAxe()
    {
        isReturning = false;
        axe.transform.parent = transform;
        axe.position = target.position;
        axe.rotation= target.rotation;
    }
    Vector3 getBezierQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1-t;
        float tt = t * t; 
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
}
