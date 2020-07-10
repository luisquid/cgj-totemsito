using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputTest : MonoBehaviour
{



    public bool activeItem = false;

    public Transform objectToRotate;

    //LERP 
    float lerpTime = 0.3f;
    float currentLerpTime;
    Vector3 startRotation;
    Vector3 endRotation;
    Vector3 vectorToGo;
    
    TotemController controller;

    //SWIPES
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    //0 UP
    //1 DOWN
    //2 LEFT
    //3 RIGHT

    bool up, down, left, right;

    public delegate void InputAction();
    public static event InputAction onAction;

    private void Start()
    {
        controller = GetComponentInParent<TotemController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeItem || controller.readOnly)
            return;
        //LERP
        currentLerpTime += Time.deltaTime;

        if (currentLerpTime > lerpTime)
            currentLerpTime = lerpTime;

        float perc = currentLerpTime / lerpTime;

        transform.eulerAngles = Vector3.Slerp(startRotation, endRotation, perc);

        //TOUCHES
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                fingerUp = Input.touches[0].position;
                fingerDown = Input.touches[0].position;
            }

            //Detects Swipe while finger is still moving
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = Input.touches[0].position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                fingerDown = Input.touches[0].position;
                checkSwipe();
            }
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || up)
        {
            up = false;
            currentLerpTime = 0;

            vectorToGo = Vector3.right;
            
            transform.RotateAround(transform.position, vectorToGo, 90);

            startRotation = transform.eulerAngles;

            endRotation = transform.eulerAngles + (Vector3.right * 90f);

            /*if (onAction != null)
                onAction();*/
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || down)
        {
            down = false;
            currentLerpTime = 0;
            vectorToGo = Vector3.right;
            startRotation = transform.eulerAngles;

            //transform.RotateAround(transform.position, vectorToGo, -90);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || left)
        {
            left = false;
            currentLerpTime = 0;

            vectorToGo = Vector3.up;
            startRotation = transform.eulerAngles;
            
            //transform.RotateAround(transform.position, vectorToGo, 90);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || right)
        {
            right = false;
            currentLerpTime = 0;
            vectorToGo = Vector3.up;
            transform.RotateAround(transform.position, vectorToGo, -90);
        }
    }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        if (!up)
            up = true;

        Debug.Log("Swipe UP");
    }

    void OnSwipeDown()
    {
        if (!down)
            down = true;

        Debug.Log("Swipe Down");
    }

    void OnSwipeLeft()
    {
        if (!left)
            left = true;

        Debug.Log("Swipe Left");
    }

    void OnSwipeRight()
    {
        if (!right)
            right = true;

        Debug.Log("Swipe Right");
    }
}
