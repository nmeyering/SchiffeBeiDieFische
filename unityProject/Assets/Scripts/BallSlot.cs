using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSlot : MonoBehaviour
{
    private Cannon cannon;
    private Collider coll;
    private Renderer rend;

    public float resetDelaySeconds = 1f;

    private void Awake()
    {
        cannon = GetComponentInParent<Cannon>();
        coll = GetComponent<Collider>();
        rend = GetComponentInChildren<Renderer>();

        Reset();
    }

    public void Reset()
    {
        coll.enabled = false;
        rend.enabled = false;
        
        StartCoroutine(ResetAfterDelay(resetDelaySeconds));
    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        coll.enabled = true;
        rend.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cannonball"))
        {
            cannon.Loaded = true;
            gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
