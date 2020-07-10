using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemController : MonoBehaviour
{
    enum FaceDirection
    {
        top, //1
        bottom, //2
        front, //3
        back, //4
        left, //5
        right //6
    }

    public List<Transform> totems;

    public List<int> faceOrientation;

    public bool readOnly = false;
    public Transform activeTotem;

    public int totemIndex = 0;
    FaceDirection face;

    public void ResetTotemRotation()
    {
        for (int i = 0; i < totems.Count; i++)
        {
            totems[i].GetChild(0).rotation = Quaternion.identity;
            totems[i].GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    public void ResetPieceRotation(Transform _item)
    {
        _item.rotation = Quaternion.identity;
    }

    public void GoToNextTotem()
    {
        totemIndex++;

        if (totemIndex >= totems.Count)
        {
            if (!readOnly)
                GameManager.Instance.TotemCompleted();
        }

        else
        {
            activeTotem = totems[totemIndex];

            for (int i = 0; i < totems.Count; i++)
            {
                if (!readOnly)
                    totems[i].GetComponent<RotateAdd>().isActive = false;

                totems[i].GetComponentInChildren<ParticleSystem>().Stop();
                totems[totemIndex].GetComponentInChildren<ParticleSystem>().Play();

                if (!readOnly)
                    totems[totemIndex].GetComponent<RotateAdd>().isActive = true;

            }
        }
    }
    public void RandomizeFaces(TotemController guide = null)
    {
        ResetTotemRotation();

        for (int i = 0; i < totems.Count; i++)
        {
            Vector3 targetRotation = Vector3.zero;
            float rotationAngle = 0;

            face = (FaceDirection)Random.Range(1, 6);
            switch (face)
            {
                case FaceDirection.right:
                    targetRotation = Vector3.up;
                    rotationAngle = 90f;
                    break;
                case FaceDirection.left:
                    targetRotation = Vector3.up;
                    rotationAngle = -90f;
                    break;
                case FaceDirection.top:
                    targetRotation = Vector3.right;
                    rotationAngle = -90f;
                    break;
                case FaceDirection.bottom:
                    targetRotation = Vector3.right;
                    rotationAngle = 90f;
                    break;
                case FaceDirection.back:
                    targetRotation = Vector3.up;
                    rotationAngle = 180f;
                    break;
                case FaceDirection.front:
                    targetRotation = Vector3.up;
                    rotationAngle = 0f;
                    break;
                default:
                    print("What is going on here?");
                    break;
            }

            if (readOnly)
                totems[i].GetComponentInChildren<ChangeColorIfGuide>().ChangeColor();

            totems[i].RotateAround(totems[i].position, targetRotation, rotationAngle);
            faceOrientation.Add((int)face);
        }

        //WE CHECK IF THE TOTEMS MATCH IN A PIECE
        if(guide != null)
        {
            for(int i = 0; i < totems.Count; i++)
            {
                while(faceOrientation[i] == guide.faceOrientation[i])
                {
                    ResetPieceRotation(totems[i]);

                    Vector3 targetRotation = Vector3.zero;
                    float rotationAngle = 0;

                    face = (FaceDirection)Random.Range(0, 5);
                    switch (face)
                    {
                        case FaceDirection.right:
                            targetRotation = Vector3.up;
                            rotationAngle = 90f;
                            break;
                        case FaceDirection.left:
                            targetRotation = Vector3.up;
                            rotationAngle = -90f;
                            break;
                        case FaceDirection.top:
                            targetRotation = Vector3.right;
                            rotationAngle = -90f;
                            break;
                        case FaceDirection.bottom:
                            targetRotation = Vector3.right;
                            rotationAngle = 90f;
                            break;
                        case FaceDirection.back:
                            targetRotation = Vector3.up;
                            rotationAngle = 180f;
                            break;
                        case FaceDirection.front:
                            targetRotation = Vector3.up;
                            rotationAngle = 0f;
                            break;
                        default:
                            print("What is going on here?");
                            break;
                    }

                    totems[i].RotateAround(totems[i].position, targetRotation, rotationAngle);
                    faceOrientation[i] = (int)face;
                }
            }
        }

        //SET ACTIVE TOTEM
        activeTotem = totems[totemIndex];

        if (!readOnly)
            totems[totemIndex].GetComponent<RotateAdd>().isActive = true;

        totems[totemIndex].GetComponentInChildren<ParticleSystem>().Play();
    }

    public void InitTotem(TotemController guide = null)
    {
        activeTotem = totems[totemIndex];
        RandomizeFaces(guide);
    }
}
