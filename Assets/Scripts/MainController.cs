using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//
// By John Boman, Martin Quach
// c18johbo       a18marqu
public class MainController : MonoBehaviour
{
    //ball object
    public GameObject ball;


    //Input values
    public Vector2 startPosition;
    public Vector2 startVelocity;

    //local variables
    [SerializeField] Vector2 position;
    [SerializeField] Vector2 velocity;
    Vector2 force;
    [SerializeField] private float mass;
    [SerializeField] private float inverseMass;
    public float radius;
    private bool grounded;
    float physicsUpdateTimer = 0;
    
    public float GetMass() { return mass; }

    //Used to calculate inverseMass
    public void SetMass(float value) {
        mass = value;

        if (mass < float.Epsilon){
            inverseMass = float.MaxValue;
        } else {
            inverseMass = 1.0f / mass;
        }
    }


    void Start() {
        Initialize();
    }

    //Reset Values
    void Initialize() { 
        grounded = false;
        velocity = startVelocity;
        position = startPosition;
    }


    void Update() {

        //check for Space to reset ball
        if (Input.GetKeyDown(KeyCode.Space)) {
            Initialize();
        }

        //Fixed Physics update timer
        physicsUpdateTimer += Time.deltaTime;
        if (physicsUpdateTimer > Constants.STEPVALUE) {
            physicsUpdateTimer -= Constants.STEPVALUE; // :/


            UpdatePhysics();
            CheckCollisions();

            //Update graphic of the ball
            ball.transform.position = position;
        }
    }
    private void UpdatePhysics() {

        //Add gravity force
        force += new Vector2(0, Constants.G * mass);

        //Check if it's on the ground
        if (grounded) {
            //Apply the Normal force from the floor (to contradict gravity)
            force += new Vector2(0, -Constants.G * mass);

            //Apply drag when on ground 
            force -= velocity * Constants.GROUNDDRAG;
        }
        else {
            //Apply drag^2 when in the air
            force -= new Vector2(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y)) * velocity * Constants.AIRDRAG;
        }

        //Update Position
        position += velocity * Constants.STEPVALUE;

        //Update Velocity
        velocity += force * (inverseMass * Constants.STEPVALUE);


        //Reset force
        force = Vector2.zero;
    }

    private void CheckCollisions() {
        //Check if the ball and the floor has collided
        //by calculating the distance between the objects we can check if it's lower than the size of the objects (radius of the ball)
        //Since we know the floor is on y=0 and doesn't move, we can use the balls y position instead of using expensive Vector2.Distance
        float dist = position.y - radius;

        //Check if they are closer than the radius of the ball
        if (dist < 0) {

            //Move the ball to the closest they can be
            position.y = radius;

            //We only handle collisions with the floor so we can use Vector2.up with the balls velocity vector 
            //Reflect the ball's direction with the normal of the floor to get the new velocity
            Vector2 newVelocity = Vector2.Reflect(velocity, Vector2.up);

            //Reduce velocity by the bounce multiplier
            velocity = newVelocity;
            velocity.y *= Constants.BOUNCEMULT;

            //Threshold to know if it's on the ground
            if (velocity.y < 0.5f) {
                velocity.y = 0;
                grounded = true;
            }

        }
    }


    void OnValidate() {

        
        //Update inverse mass
        SetMass(GetMass());
    }

}
