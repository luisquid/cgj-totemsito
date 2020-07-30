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
#if  UNITY_STANDALONE || UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotateTot.SetRotationParameters(0f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotateTot.SetRotationParameters(0f, -1f);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rotateTot.SetRotationParameters(1f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rotateTot.SetRotationParameters(-1f, 0f);
        }
#elif UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
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
#endif
        rotateTot.UpdateTotem();
    }

    public void CheckDirection()
    {
        if (Mathf.Abs(startInput.x - endInput.x) > 20 && Mathf.Abs(startInput.x - endInput.x) > Mathf.Abs(startInput.y - endInput.y))
        {
            if (startInput.x < endInput.x)
            {
                rotateTot.SetRotationParameters(0f, -1f);
            }
            else
            {
                rotateTot.SetRotationParameters(0f, 1f);
            }
        }
        else if (Mathf.Abs(startInput.y - endInput.y) > 20 && Mathf.Abs(startInput.y - endInput.y) > Mathf.Abs(startInput.x - endInput.x))
        {
            if (startInput.y < endInput.y)
            {
                rotateTot.SetRotationParameters(1f, 0f);
            }
            else
            {
                rotateTot.SetRotationParameters(-1f, 0f);
            }
        }
    }
}
