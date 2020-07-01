using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    const float RotateDegreesPerSecond = 30;
    const float ThrustForce = 1f;

    Vector2 thrustDirection = new Vector2(1, 0);
    Vector3 Rotation;
    Rigidbody2D myRigidbody2D;
    CircleCollider2D myCircleCollider2D;
    bool PreviousFrame = false;
    bool Previous = false;

    public float radius;

    
    // Start is RotateDegreesPerSecondcalled before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCircleCollider2D = GetComponent<CircleCollider2D>();
        radius = myCircleCollider2D.radius;
    }

    //FixedUpdate method
    private void FixedUpdate()
    {
        if (!PreviousFrame)
        {
            
            if (Input.GetAxis("Thrust") > 0)
            {

                myRigidbody2D.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
            }
            else if (Input.GetAxis("Thrust") == 0)
            {
                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = vel * 0.95f;
            }
            OnBecameInvisible();
        }
        else
        {
            PreviousFrame = false;
        }
    }

    //update method
    void Update()
    {
        float rotationInput = Input.GetAxis("Rotate");
        float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
        if(!Previous)
        {
            if (rotationInput == 0)
            {
                rotationAmount *= 0;
            }
            else if (rotationInput != 0)
            {
                if (rotationInput < 0)
                {
                    rotationAmount *= -1;
                }
            }
            transform.Rotate(Vector3.forward, rotationAmount);
            Rotation = transform.eulerAngles;
            float angle = Rotation.z;
            float rad = angle * Mathf.Deg2Rad;
            thrustDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            Previous = true;
        }
        else
        {
            Previous=false;
        }
    }

    

    //OnBecameInvisible method 
    private void OnBecameInvisible()
    {
        Vector2 position = transform.position;
        if (position.x - radius < ScreenUtils.ScreenLeft)
        {
            position.x = ScreenUtils.ScreenRight - radius;
        }
        else if (position.x + radius > ScreenUtils.ScreenRight)
        {
            position.x = ScreenUtils.ScreenLeft + radius;
        }
        if (position.y + radius > ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenBottom + radius;
        }
        else if (position.y - radius < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenTop - radius;
        }
        transform.position = position;
    }
    // Update is called once per frame
    
}
