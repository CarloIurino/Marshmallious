using UnityEngine;
using System.Collections;


public class HamsterAnimation : MonoBehaviour {
	Animator animator;

	[SerializeField]
	ParticleSystem _zzzParticles;

	void Awake(){
		animator = GetComponent<Animator> ();
	}

	public void WalkAnimation(){
		animator.SetBool ("IsWalking", true);
	}

	public void StopWalkAnimation(){
		animator.SetBool ("IsWalking", false);
	}

	public void DieAnimation(){
		animator.SetBool ("Die", true);
	}

	public void StopDieAnimation(){
		animator.SetBool ("Die", false);
		_zzzParticles.Stop ();
		_zzzParticles.Clear ();
	}

	public void PlayZZZ(){
		_zzzParticles.Play ();
	}

	public void EatAnimation(){
		animator.SetTrigger ("Eat");
	}

	public void IdleAnimation(){
		animator.SetBool ("IsWalking", false);
		animator.SetBool ("Die", false);
	}

}
