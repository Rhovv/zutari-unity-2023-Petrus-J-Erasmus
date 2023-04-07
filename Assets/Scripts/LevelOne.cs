using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        inSpeed.text = speed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
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

    private void FixedUpdate()
    {
        //velocity = cube.velocity;
        //if (direction.x == 0f)
        //{
        //    velocity.x = 0f;
        //}
        //if (direction.y == 0f)
        //{
        //    velocity.y = 0f;
        //}

        cube.AddForce(direction * speed * Time.deltaTime);

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

    public void btnMenu_click(string s)
    {
        Controller.loadScene(s);
    }
}
