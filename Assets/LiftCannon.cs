using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(CircularDrive))]
public class LiftCannon : MonoBehaviour
{

	public Transform cannon;
	private CircularDrive drive;
	private float startRotation;

	// Use this for initialization
	void Start ()
	{
		drive = GetComponent<CircularDrive>();
		startRotation = cannon.localEulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
		cannon.localRotation = Quaternion.AngleAxis(startRotation + drive.outAngle,Vector3.right);
	}
}
