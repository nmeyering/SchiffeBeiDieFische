using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Valve.VR.InteractionSystem;

public class RotateCannon : MonoBehaviour
{
	public CircularDrive drive;
	public Axis rotationAxis;
	
	private Quaternion startRotation;
	private Cannon cannon;

	void Start ()
	{
		startRotation = transform.localRotation;
		cannon = GetComponentInParent<Cannon>();
	}
	
	void Update () {
		transform.localRotation = 
			startRotation *
			Quaternion.AngleAxis(
				GearRatio(cannon, rotationAxis)
				* drive.outAngle,
				AxisVector(rotationAxis));
	}

	private static float GearRatio(Cannon cannon, Axis axis)
	{
		switch (axis)
		{
			case Axis.X:
				return 1 / cannon.gearRatioElevation;
			case Axis.Y:
				return 1 / cannon.gearRatioAzimuth;
			default:
                return 1;
		}

	}

	private static Vector3 AxisVector(Axis axis)
	{
		switch (axis)
		{
			case Axis.X:
				return Vector3.right;
			case Axis.Y:
				return Vector3.up;
			case Axis.Z:
				return Vector3.forward;
			case Axis.None:
			default:
				throw new ArgumentOutOfRangeException("axis", axis, null);
		}
	}
}
