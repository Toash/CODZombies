using UnityEngine;

public class BaseAnimator : MonoBehaviour
{
	protected delegate void SetAnimBoolDelegate(bool a);
	protected delegate void SetAnimTriggerDelegate();

	[SerializeField]
    protected Animator anim;
	protected virtual void Awake()
	{

	}
}
