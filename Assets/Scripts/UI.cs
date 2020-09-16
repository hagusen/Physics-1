using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public Text posX;
    public Text posY;
    public Text velX;
    public Text velY;

    public CameraFollow camFollow; 
    public MainController controller;

    Vector2 pos;
    Vector2 vel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check for Space to reset ball
        if (Input.GetKeyDown(KeyCode.Space)) {
            float x;

            if (float.TryParse(posX.text, out x)) { pos.x = x;}else {pos.x = 0;}
            if (float.TryParse(posY.text, out x)) { pos.y = x;}else {pos.y = 0;}
            if (float.TryParse(velX.text, out x)) { vel.x = x;}else {vel.x = 0;}
            if (float.TryParse(velY.text, out x)) { vel.y = x;}else {vel.y = 0;}


            controller.startPosition = pos;
            controller.startVelocity = vel;


        }
    }


    public void toggleScript() {

        camFollow.enabled = !camFollow.enabled;
    }


}
