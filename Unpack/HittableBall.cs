using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableBall : MonoBehaviour
{

	public bool canBeHit;

	public bool hasBeenRegistered = false;

    public void EndSim()
	{
		//gameObject.SetActive(false);
		Destroy(gameObject);
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

	private void OnTriggerEnter(Collider other)
	{

		if (!hasBeenRegistered)
		{
			if (other.gameObject.tag == "BatterWall")
			{
				print("Strike");
				GameObject.FindWithTag("Player").GetComponent<BatterController>().GainStrike();
			} else if(other.gameObject.tag == "Respawn")
			{
				print("Ball");
				GameObject.FindWithTag("Player").GetComponent<BatterController>().GainBall();
			} else
			{
				GameObject.FindWithTag("Davidson").GetComponent<Davidson>().SpawnHitBall(transform.position, 40, 45, gameObject.GetComponent<LiteralActualMath>().initVelo);
				gameObject.SetActive(false);

				print("HIT");
			}

			hasBeenRegistered = true;
		}
        

		
	}
}
