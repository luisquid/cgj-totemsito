using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTotem : MonoBehaviour
{
    public float currentLerpTime;
    public float lerpTime;

    GameObject deadParent;
    Transform originalParent;
    Quaternion originalRotation;
    Quaternion targetRotation;
    bool isRotating = false;

    private void OnEnable()
    {
        deadParent = GameObject.FindGameObjectWithTag("WorldAxis");
    }

    public void SetRotationParameters(float _vertical, float _horizontal)
    {
        isRotating = true;
        currentLerpTime = 0f;
        originalParent = transform.parent;

        deadParent.transform.rotation = Quaternion.identity;
        deadParent.transform.position = transform.position;
        transform.parent = deadParent.transform;

        originalRotation = deadParent.transform.rotation;
        targetRotation = originalRotation;

        targetRotation *= Quaternion.Euler(new Vector3(_vertical, _horizontal, 0f) * 90f);
    }

    public void UpdateTotem()
    {
        if (isRotating)
        {
            if (currentLerpTime < lerpTime)
            {
                currentLerpTime += Time.fixedDeltaTime;
                float perc = currentLerpTime / lerpTime;

                deadParent.transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, perc);
            }

            else
            {
                deadParent.transform.rotation = targetRotation;
                transform.parent = originalParent;

                isRotating = false;
            }
        }
    }
}
