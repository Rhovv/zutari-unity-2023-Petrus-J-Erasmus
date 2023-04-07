using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class LevelOne : MonoBehaviour
{
    public Color colStill;
    public Color colUp;
    public Color colDown;
    public Color colLeft;
    public Color colRight;

    public Rigidbody cube;
    public BoxCollider box;
    public Material matCube;

    public TMP_InputField inSpeed;

    Vector3 direction = Vector3.zero;
    Vector3 velocity;
    Vector3 position;
    Vector3 minPos;
    Vector3 maxPos;
    int speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        // Use a boolean in the Controller script to allow the single instance setup where required
        if (!Controller.bLevelOneInit)
        {
            Controller.bLevelOneInit = true;
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
        }

        inSpeed.text = speed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Add the direction components as per the key presses
        direction.x = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1f;
        }

        direction.y = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            direction.y += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y -= 1f;
        }

        // Check the position of the cube +- its boundries, translated to a screen point, to see if it is still on the screen. Flip its position if required
        position = cube.transform.position;
        minPos = position - box.size / 2f;
        maxPos = position + box.size / 2f;
        if ((Camera.main.WorldToScreenPoint(maxPos).x < 0f) || (Camera.main.WorldToScreenPoint(minPos).x > Display.main.renderingWidth))
        {
            position.x = -position.x;
        }
        if ((Camera.main.WorldToScreenPoint(maxPos).y < 0f) || (Camera.main.WorldToScreenPoint(minPos).y > Display.main.renderingHeight))
        {
            position.y = -position.y;
        }
        cube.position = position;
    }

    // Unity prefers physics calculations to be made in the fixed update function
    private void FixedUpdate()
    {
        // Add the forces to a direction as calculated in the Update function
        cube.AddForce(direction * speed * Time.deltaTime);

        // This code ensures that the cube does come to rest without continuing to move in incredibly fine increments
        velocity = cube.velocity;
        if ((velocity.x > -0.001f) && (velocity.x < 0.001f)) { velocity.x = 0f; }
        if ((velocity.y > -0.001f) && (velocity.y < 0.001f)) { velocity.y = 0f; }
        cube.velocity = velocity;

        // Unless the cube is at rest, find the direction it is moving the fastest in and recolour the cube according to that
        if (cube.velocity.y != 0)
        {
            if ((cube.velocity.x != 0) && (Mathf.Abs(cube.velocity.x) > Mathf.Abs(cube.velocity.y)))
            {
                if (cube.velocity.x > 0) { matCube.color = colRight; }
                else { matCube.color = colLeft; }
            }
            else
            {
                if (cube.velocity.y > 0) { matCube.color = colUp; }
                else { matCube.color = colDown; }
            }
        }
        else
        {
            if (cube.velocity.x > 0) { matCube.color = colRight; }
            else if (cube.velocity.x < 0) { matCube.color = colLeft; }
            else { matCube.color = colStill; }
        }
    }

    // This function reads the speed that the user inputs, and also limits it between a minimum and a maximum value
    public void inSpeed_endEdit(string s)
    {
        if ((s == null) || (s == ""))
        {
            inSpeed.text = speed.ToString();
            return;
        }

        speed = int.Parse(s);
        if (speed < 0)
        {
            speed = 0;
        }
        if (speed > 1000)
        {
            speed = 1000;
        }
        inSpeed.text = speed.ToString();
    }

    // Return to the main screen
    public void btnMenu_click(string s)
    {
        Controller.loadScene(s);
    }
}
