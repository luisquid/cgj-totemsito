using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAdd : MonoBehaviour
{
	public float rotationDuration = 0.5f;
    public bool isActive = false;
    
    private TotemController controller; 
    private bool isRotating;

    public float rotationSpeed;
	enum Direction
	{
		right=0,
		left=1,
		up=2,
		down=3
	}

    private void Start()
    {
        controller = GetComponentInParent<TotemController>();
    }

    void Update()
	{
        if (!isActive || !GameManager.Instance.gameStarted)
            return;

		if(Input.GetKey(KeyCode.RightArrow))
		{
			if(!isRotating)
			{
				StartCoroutine(RotateTheThing(Direction.right));
			}
		}
    	
		if(Input.GetKey(KeyCode.LeftArrow))
    	{
    		if(!isRotating)
    		{
    			StartCoroutine(RotateTheThing(Direction.left));
    		}
    	}
    	
		if(Input.GetKey(KeyCode.UpArrow))
		{
			if(!isRotating)
			{
				StartCoroutine(RotateTheThing(Direction.up));
			}
		}
    	
		if(Input.GetKey(KeyCode.DownArrow))
		{
			if(!isRotating)
			{
				StartCoroutine(RotateTheThing(Direction.down));
			}
		}       
    }

    IEnumerator RotateTheThing(Direction direction)
	{
        GameManager.Instance.fxSource.clip = GameManager.Instance.fxRotation;
        GameManager.Instance.fxSource.volume = 0.3f;
        GameManager.Instance.fxSource.Play();

        isRotating =true;
		
		float v = direction== Direction.up?1:direction== Direction.down?-1:0;
		float h = direction== Direction.right?-1:direction== Direction.left?1:0;
		
		//Life Hack
		Transform originalParent = transform.parent;
		GameObject go = new GameObject("tmp_holder");
		
		go.transform.position = transform.position;
		transform.parent = go.transform;
		
		Quaternion originalRotation = go.transform.rotation;
		Quaternion targetRotation = originalRotation;
		
		targetRotation *= Quaternion.Euler(new Vector3
		(
			v,
			h,
			0
		) * 90);

		float startTime = Time.time;
		yield return null;
		
		while(Time.time< startTime+rotationDuration)
		{
			go.transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, Mathf.InverseLerp(startTime, startTime+rotationDuration, Time.time));
			yield return null;
		}
		
		go.transform.rotation = targetRotation;
		yield return null;
		
		transform.parent = originalParent;
		Destroy(go);


        isRotating = false;

        GameManager.Instance.fxSource.volume = 1.0f;
        GameManager.Instance.CheckMatch();
    }
}
