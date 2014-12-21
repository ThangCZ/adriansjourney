using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	private NavMeshAgent agent;
	public GameObject target;
	PlayerStatsHolder targetStats;
	public float distance = 30f;
	public float closeDistance = 2f;
	public float attackCooldown = 2f;
	public int damage = 5;
	private float currentAttackCooldown = 0f;
	private Vector3 spawn;
	public GameObject animatedObject;
	private Animator avatarAnimator;

	// Use this for initialization
	void Start () 
	{
		avatarAnimator = animatedObject.GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent>();
		spawn = transform.position;
		targetStats = (PlayerStatsHolder)target.GetComponent<PlayerStatsHolder>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		avatarAnimator.SetBool("Attack", false);
		avatarAnimator.SetBool("Forward", false);
		float curDistance = Vector3.Distance(target.transform.position, transform.position);
		if (curDistance <= closeDistance) {
						//Debug.Log("Enemy close");
						agent.Stop ();
						if (Time.time >= currentAttackCooldown)
								Attack ();
				} else if (curDistance < distance) {
			avatarAnimator.SetBool("Forward", true);
						agent.SetDestination (target.transform.position);
				} else {
			avatarAnimator.SetBool("Forward", true);
						agent.SetDestination (spawn);
				}
	}
	// Attack the target
	private void Attack ()
	{
		avatarAnimator.SetBool("Attack", true);
		currentAttackCooldown = Time.time + attackCooldown;
		targetStats.modifyHealth(damage * -1);
	}
}
