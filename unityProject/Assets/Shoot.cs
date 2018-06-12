using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public Transform startPos;
    [SerializeField] private float startVelocity;

    public void ShootCannon()
    {
        var go = GameObject.Instantiate(cannonballPrefab, startPos.position, startPos.rotation);
        var rb = go.AddComponent<Rigidbody>();
        rb.AddForce(startVelocity * startPos.forward, ForceMode.VelocityChange);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger " + other);
        if (other.gameObject.CompareTag("Torch"))
        {
            Invoke("ShootCannon", 1);
        }
    }
}
