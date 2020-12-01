using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnightWeapon : MonoBehaviour
{

	// PUBLIC //
	public Transform hitPoint;
	public LayerMask whatIsPlayer;
	public float overlapDiameter = 1f;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Attack()
	{
		//does an overlap and checks if any other object collides with the circle
		//creates a circle at hitPoint.position of diameter .2f and checks if it collides with any object
		//on Player layer (whatIsPlayer)
		if (Physics2D.OverlapCircle(hitPoint.position, overlapDiameter, whatIsPlayer))
        {
			PlayerHealthController.instance.DamagePlayer();
        }

	}

	public void EnragedAttack()
	{
		//does an overlap and checks if any other object collides with the circle
		//creates a circle at hitPoint.position of diameter .2f and checks if it collides with any object
		//on Player layer (whatIsPlayer)
		if (Physics2D.OverlapCircle(hitPoint.position, overlapDiameter, whatIsPlayer))
		{
			PlayerHealthController.instance.DamagePlayer();
		}

	}

	
}
