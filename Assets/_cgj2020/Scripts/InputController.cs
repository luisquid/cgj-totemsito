using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Vector2 startInput, endInput;

    public RotateTotem rotateTot;

    bool hasTouchReleased = true;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotateTot.SetRotationParameters(0f, 1f);
        }

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startInput = touch.position;
                endInput = touch.position;
            }

            else if(touch.phase == TouchPhase.Ended)
            {
                if(hasTouchReleased)
                {
                    endInput = touch.position;
                    CheckDirection();
                }
            }
        }

        rotateTot.UpdateTotem();
    }

    public void CheckDirection()
    {
        if (Mathf.Abs(startInput.x - endInput.x) > 20 && Mathf.Abs(startInput.x - endInput.x) > Mathf.Abs(startInput.y - endInput.y))
        {
            print("Horizontal Input");
            if (startInput.x < endInput.x)
            {
                print("SWIPE RIGHT");
                rotateTot.SetRotationParameters(0f, -1f);
            }

            else
            {
                print("SWIPE LEFT");
                rotateTot.SetRotationParameters(0f, 1f);
            }
        }
        else if (Mathf.Abs(startInput.y - endInput.y) > 20 && Mathf.Abs(startInput.y - endInput.y) > Mathf.Abs(startInput.x - endInput.x))
        {
            print("Vertical Input");
            if (startInput.y < endInput.y)
            {
                print("SWIPE UP");
                rotateTot.SetRotationParameters(1f, 0f);
            }

            else
            {
                print("SWIPE DOWN");
                rotateTot.SetRotationParameters(-1f, 0f);
            }
        }
    }
}
