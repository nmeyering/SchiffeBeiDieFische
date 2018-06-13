using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fuse : MonoBehaviour
{
	public ParticleSystem sparks;
	public float burnTime = 1f;
	public UnityEvent Expire = new UnityEvent();
	public Transform startSparkPos;
	public Transform endSparkPos;
	
    public GameObject cannonballPrefab;
    public Transform ballStartPos;
    public float startVelocity;

	private float remainingBurnTime;
	private Renderer renderer;

	void Awake()
	{
		remainingBurnTime = burnTime;
		renderer = GetComponent<Renderer>();
		renderer.material.SetFloat("_FuseRemaining", 1);
		renderer.material.SetFloat("_Burning", 0);
	}

	private void Update()
	{
		sparks.transform.position =
			Vector3.Lerp(endSparkPos.position, startSparkPos.position, remainingBurnTime / burnTime - 0.3f);
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Light();
		}
		
	}

	public void Light()
	{
		if (remainingBurnTime > 0)
		{
			StartCoroutine(LightFuse());
		}
	}

	private IEnumerator LightFuse()
	{
		remainingBurnTime = burnTime;
		renderer.material.SetFloat("_Burning", 1);
		sparks.Play();
		while (remainingBurnTime > 0)
		{
			yield return null;
			remainingBurnTime -= Time.deltaTime;
            renderer.material.SetFloat("_FuseRemaining", (remainingBurnTime / burnTime) - 0.3f);
		}

		sparks.Stop();
		
		ShootCannon();
		Expire.Invoke();
		remainingBurnTime = burnTime;
	}
	
    public void ShootCannon()
    {
        var go = GameObject.Instantiate(cannonballPrefab, ballStartPos.position, ballStartPos.rotation);
        var rb = go.AddComponent<Rigidbody>();
        rb.AddForce(startVelocity * ballStartPos.forward, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Torch"))
        {
	        Light();
        }
    }
}
