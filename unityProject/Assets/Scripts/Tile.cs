using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Tile : MonoBehaviour {

    public int x;
    public int y;
    public Vector3 size;
    public Renderer renderer;

    private Battlefield battlefield;

    private void Awake()
    {
        battlefield = GetComponentInParent<Battlefield>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cannonball"))
        {
            battlefield.Hit(this);
            renderer.material.color = Color.red;
        }
    }
}
