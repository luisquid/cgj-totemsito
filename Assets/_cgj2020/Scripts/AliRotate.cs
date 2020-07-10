using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliRotate : MonoBehaviour
{
    public static IEnumerator IERotate(GameObject _go, Quaternion _newRot, float _timeToMove)
    {
        Quaternion startRot = _go.transform.rotation;
        float timer = 0f;

        while(timer < _timeToMove)
        {
            _go.transform.rotation = Quaternion.Slerp(startRot, _newRot, timer / _timeToMove);
            timer += Time.deltaTime;
            yield return null;
        }

        _go.transform.rotation = _newRot;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            print(Quaternion.Euler(Vector3.up *90f));
            StartCoroutine(IERotate(gameObject, Quaternion.Euler(Vector3.up * 90f), 0.5f));
        }
    }
}
