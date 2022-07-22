using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnDrawGizmos()
	{
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, .5f);
	}
}
