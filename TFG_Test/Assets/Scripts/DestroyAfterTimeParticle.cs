using System.Collections;
using UnityEngine;

public class DestroyAfterTimeParticle : MonoBehaviour {
	[SerializeField]
	private float timeToDestroy = 0.8f;

	void Start () {
		Destroy (gameObject, timeToDestroy);
	}

}
