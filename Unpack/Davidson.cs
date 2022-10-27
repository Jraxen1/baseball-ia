using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Davidson : MonoBehaviour
{

	public GameObject hitBall;
	public GameObject niehaus;
	public Hernandez hernandez;

	public float timeBetweenPitches;

	public float cTime = 0;

	public float offSpeedChance;


	public float CalculateExitVelocity(float pitchSpeed1, float batEfficiency, float batSpeed)
	{
		float ret = 0;

		ret = (batEfficiency * pitchSpeed1) + (1 + batEfficiency) * batSpeed;

		return ret;
	}


    public void SpawnHitBall(Vector3 SpawnLoc, float spawnVelo, float spawnAngle, float pitchSpeed)
	{


		// I GOT A HIT

		hernandez.GetHit();



		GameObject hit = Instantiate(hitBall, SpawnLoc, transform.rotation);
		float randomBatSpeed = Random.Range(30.0f, 40.01f);
		hit.GetComponent<LiteralActualMath>().initVelo = CalculateExitVelocity(pitchSpeed,0.15f,randomBatSpeed);
		hit.GetComponent<LiteralActualMath>().initAngle = spawnAngle;
		hit.GetComponent<LiteralActualMath>().x = SpawnLoc.x;
		hit.GetComponent<LiteralActualMath>().y = SpawnLoc.y;


	}

    // Start is called before the first frame update
    void Start()
    {
		niehaus = GameObject.FindWithTag("Niehaus");
		timeBetweenPitches = 3;

	}

    // Update is called once per frame
    void Update()
    {
		//int pitchesPerFrame = 0;



        if(cTime >= timeBetweenPitches)
		{
			//print("AHHHHH");
			float randomSpeed = Random.Range(80.01f, 100.01f);
			float randomSpin = Random.Range(1000, 4000);


			int perc = Random.Range(0, 100);

            if(perc < offSpeedChance*100)
			{
				//randomSpeed -= 10;
				print("OFFSPEED");
				randomSpin = 0;
			}

			niehaus.GetComponent<Niehaus>().SpawnNewBallFromCode(randomSpeed, randomSpin);
			//pitchesPerFrame++;

			cTime = 0;
		}
		else
		{
			cTime += Time.deltaTime;
		}
		
    }
}
