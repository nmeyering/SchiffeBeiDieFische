using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float gearRatioAzimuth = 1;
    public float gearRatioElevation = 1;
    public bool Loaded { get; set; }
	
    public GameObject cannonballPrefab;
    public Transform ballStartPos;
    public float startVelocity;

    public void Shoot()
    {
        if (!Loaded)
        {
            return;
        }

        var go = Instantiate(cannonballPrefab, ballStartPos.position, ballStartPos.rotation);
        var rb = go.GetComponent<Rigidbody>();
        rb.AddForce(startVelocity * ballStartPos.forward, ForceMode.VelocityChange);
        Loaded = false;
    }
}