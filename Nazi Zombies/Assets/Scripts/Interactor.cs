using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//defines stuff for things that can interact with IInteractable
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Interactor : MonoBehaviour
{
	[SerializeField] private int interactRange;
	private SphereCollider sphereCollider;
	private Rigidbody rigidBody;

	private void Awake()
	{
		references();
		setup();
	}

	void OnTriggerEnter(Collider other)
	{
		//check if other is IInteractable
	}

	//draw the interact sphere
	void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, interactRange);
		}
	}
	private void references()
	{
		sphereCollider = GetComponent<SphereCollider>();
		rigidBody = GetComponent<Rigidbody>();
	}
	private void setup()
	{
		sphereCollider.isTrigger = true;
		rigidBody.isKinematic = true;
	}
}
