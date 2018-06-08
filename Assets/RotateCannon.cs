using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RotateCannon : MonoBehaviour
{
	public CircularDrive azimuth;
	private Quaternion startRotation;

	void Start ()
	{
		startRotation = transform.localRotation;
	}
	
	void Update () {
		transform.localRotation = 
			startRotation *
			Quaternion.AngleAxis(azimuth.outAngle, Vector3.up);
	}
}
