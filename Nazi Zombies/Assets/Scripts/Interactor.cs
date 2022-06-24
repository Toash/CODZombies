using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//defines stuff for things that can interact with IInteractable
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Interactor : MonoBehaviour
{
	private Entity entity;
	private SphereCollider sphereCollider;
	private Rigidbody rigidBody;

	private void Awake()
	{
		References();
		Setup();
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
			Gizmos.DrawWireSphere(transform.position, entity.getInteractRange());
		}
	}
	private void References()
	{
		entity = GetComponent<Entity>();
		sphereCollider = GetComponent<SphereCollider>();
		rigidBody = GetComponent<Rigidbody>();
	}
	private void Setup()
	{
		sphereCollider.isTrigger = true;
		sphereCollider.radius = entity.getInteractRange();
		rigidBody.isKinematic = true;
	}
}
