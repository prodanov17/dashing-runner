using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;

	[Space]
	public float leftLimit;
	public float rightLimit;
	public float topLimit;
	public float bottomLimit;

	[Space]
	public Vector3 offset;
	[Range(1, 10)]
	public float smoothSpeed = 7f;

	void FixedUpdate()
	{
		Follow();




		transform.position = new Vector3
			(
				Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
				Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
				transform.position.z
			);
	}

	void Follow()
    {
        if (target != null)
        {
			Vector3 targetPosition = target.position + offset;
			Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);

			transform.position = smoothPosition;
		}
		
	}
}
