using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RespawnTorch : MonoBehaviour
{
	public GameObject torchSceneReference;
	public Transform spawnPoint;

	public float margin = 0.3f;
	
	private Player playArea;

	public void Respawn()
	{
		var rb = torchSceneReference.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		rb.transform.position = spawnPoint.position;
		rb.transform.rotation = spawnPoint.rotation;
		rb.isKinematic = false;
	}

	private void Awake()
	{
		playArea = FindObjectOfType<Player>();
	}

	private void Update()
	{
		float width = 0;
		float depth = 0;
		OpenVR.Chaperone.GetPlayAreaSize(ref width, ref depth);

		var b = new Bounds(playArea.transform.position, new Vector3(width + margin * 2, 10, depth + margin * 2));

		if (!b.Contains(torchSceneReference.transform.position))
		{
			Respawn();
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == torchSceneReference)
		{
			Respawn();
		}
	}
}
