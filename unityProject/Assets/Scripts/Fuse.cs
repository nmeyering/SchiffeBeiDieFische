using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fuse : MonoBehaviour
{
	public ParticleSystem sparks;
	public float burnTime = 1f;
	public float burnOutTime = 0.5f;
	public UnityEvent Expire = new UnityEvent();
	public Transform startSparkPos;
	public Transform endSparkPos;

	private float remainingBurnTime;
	private Renderer renderer;
	private Cannon cannon;

	void Awake()
	{
		cannon = GetComponentInParent<Cannon>();
		remainingBurnTime = 0;
		renderer = GetComponent<Renderer>();
		renderer.material.SetFloat("_FuseRemaining", 1);
		renderer.material.SetFloat("_Burning", 0);
	}

	private void Update()
	{
		sparks.transform.position =
			Vector3.Lerp(endSparkPos.position, startSparkPos.position,
				Mathf.Clamp01((remainingBurnTime - burnOutTime) / burnTime)
			);
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Light();
		}
		
	}

	public void Light()
	{
		if (remainingBurnTime <= 0)
		{
			StartCoroutine(LightFuse());
		}
	}

	private IEnumerator LightFuse()
	{
		remainingBurnTime = burnTime + burnOutTime;
		renderer.material.SetFloat("_Burning", 1);
		sparks.Play();
		while (remainingBurnTime > 0)
		{
			yield return null;
			remainingBurnTime -= Time.deltaTime;
			renderer.material.SetFloat("_FuseRemaining", Mathf.Clamp01((remainingBurnTime - burnOutTime) / burnTime));
		}

		sparks.Stop();
		
		cannon.Shoot();
		Expire.Invoke();
		remainingBurnTime = 0;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Torch"))
        {
	        Light();
        }
    }
}
