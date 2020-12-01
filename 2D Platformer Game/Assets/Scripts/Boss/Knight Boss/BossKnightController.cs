using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnightController : MonoBehaviour
{
	// PUBLIC //

	public Transform player; //Player target

	public bool isFlipped = false;

	public bool isInvulnerable = false;


	// PRIVATE //

	//BossHealthController script attached to this object
	private BossHealthController bossHealthController;

	// Start is called before the first frame update
	void Start()
	{
		//find boss health controller
		bossHealthController = gameObject.GetComponent<BossHealthController>();
	}

	// Update is called once per frame
	void Update()
	{

	}


	public void TakeHit()
    {
		if(isInvulnerable)
        {
			return;
        }

		bossHealthController.TakeHit();

		if(bossHealthController.CurrentHealth() <= 4)
        {
			GetComponent<Animator>().SetBool("IsEnraged", true);
        }

    }

	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

	
}
