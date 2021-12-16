using UnityEngine;
using System.Collections;

public class buttonControl_script : MonoBehaviour 
{

	Animator anim;

	void Awake ()
	{
		anim = GetComponentInChildren<Animator>();
	}

	public void StartCrippledWalk ()
	{
		anim.SetBool("crippled", true);
		anim.SetBool("isIdle", false);
	}
	public void EndCrippledWalk()
	{
		anim.SetBool("crippled", false);
		anim.SetBool("isIdle", false);
	}

	public void Idle ()
	{
		anim.SetBool("isIdle", true);
		anim.SetBool("isRun", false);
		anim.SetBool("crippled", false);
		anim.SetBool("dancing", false);
	}

	public void StartRun ()
	{
		anim.SetBool("isRun",true);
		anim.SetBool("isIdle", false);

	}
	public void EndRun()
	{
		anim.SetBool("isRun", false);
		anim.SetBool("isIdle", false);

	}

	public void Dance()
	{
		anim.SetBool("isIdle", true);
		anim.SetBool ("dancing", true);
	}
}
