using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LiteralActualMath3D : MonoBehaviour
{

	BallManager manager;


	public float timeModifier = 1;

	public bool hasStarted = false;

	public bool hasFinished = false;

	public float D;

	public GameObject ball;

	public float airDensity;
	public float dragCoefficient;
	public float areaOfBall;
	public float radiusOfBall = 0.03683f; // meters

	public float mass;


	public float timeInterval;

	public float maxTime;

	public float maxIntervals;

	public float initVelo;

	public float initAngle;


	public float unchangedInitVelo;


	public float liftCoefficient;
	public float RPM;

	float currentTime = 0;


	float x = 0;
	float y = 1.88f;


	float Vx;
	float Vy;

	public Vector2 strikeZoneY = new Vector2();



	float t = 0;


	float timeTaken;

	public void RotateByRotation(float RPM)

	{

		transform.Rotate(Vector3.forward, (60f * RPM) * Time.deltaTime);

	}



	public float CalculateSpinFactor(float translationalVelocity)
	{
		// s = Rw / Vt
		float s;
		float RadiansPerSecond = RPM * 0.10472f;
		s = (radiusOfBall * RadiansPerSecond) / translationalVelocity;
		return s;
	}

	public float CalculateLiftCoefficient(float currentVelocity)
	{

		float spinFactor = CalculateSpinFactor(currentVelocity);

		//print(spinFactor + "");

		float Lc;

		if (spinFactor > 0.0f && spinFactor < 0.1f)
		{

			Lc = 0.05f;

		}
		else if (spinFactor >= 0.1f && spinFactor < 0.13f)
		{
			Lc = 0.17f;
		}
		else if (spinFactor >= 0.13f && spinFactor < 0.35f)
		{
			Lc = 0.22f;
		}
		else if (spinFactor >= 0.35f && spinFactor < 0.45f)
		{
			Lc = 0.28f;
		}
		else if (spinFactor > 0.45f)
		{
			//Lc = 0.33f;
			Lc = spinFactor;
		}
		else
		{
			//Lc = 0.22f;
			Lc = 0;
		}

		//print("" + Lc);

		return Lc;


	}





	public void CalculatePositions()
	{

		x = 0;
		y = 1.88f;

		t = 0;


		currentTime = 0;


		Vx = initVelo * (float)Math.Cos(initAngle);
		Vy = initVelo * (float)Math.Sin(initAngle);

		List<Vector3> pointsOnCurve = new List<Vector3>();

		while (currentTime < timeTaken)
		{
			// calculate acceleration

			float Ax = (-1) * (calculatedD() / mass) * initVelo * Vx;

			float Ay = (-9.8f) - (calculatedD() / mass) * initVelo * Vy;

			liftCoefficient = CalculateLiftCoefficient(initVelo);


			float magnusForce = calculateMagnusForce(Vx, liftCoefficient);


			Ay += magnusForce;


			// plot

			//SpawnPoint(new Vector3(x, y, 0));
			pointsOnCurve.Add(new Vector3(x, y, 0));

			// new velocity

			Vx += Ax * timeInterval;
			Vy += Ay * timeInterval;

			// New coordinates

			x += Vx * timeInterval + (1 / 2) * Ax * (timeInterval * timeInterval);
			y += Vy * timeInterval + (1 / 2) * Ay * (timeInterval * timeInterval);

			// increment time
			currentTime += timeInterval;




		}

		for (int i = 0; i < pointsOnCurve.Count; i++)
		{
			if (pointsOnCurve[i].x > transform.position.x)
			{
				pointsOnCurve.Remove(pointsOnCurve[i]);
			}
		}

		gameObject.GetComponent<MovementTrail>().SpawnTrail(pointsOnCurve);
	}




	public void CalculatePositionLive()
	{



		timeInterval = Time.deltaTime * timeModifier;

		float Ax = (-1) * (calculatedD() / mass) * initVelo * Vx;

		float Ay = (-9.8f) - (calculatedD() / mass) * initVelo * Vy;



		//liftCoefficient = CalculateLiftCoefficient(Vx);
		liftCoefficient = CalculateLiftCoefficient(initVelo);

		//float magnusForce = calculateMagnusForce(Vx);
		//print("Sent value of " + Vx);

		float magnusForce = calculateMagnusForce(Vx, liftCoefficient);

		Ay += magnusForce;

		print("magnus force: " + magnusForce);


		// plot

		transform.position = new Vector3(x, y, 0);


		// new velocity

		Vx += Ax * timeInterval;
		Vy += Ay * timeInterval;

		// New coordinates

		x += Vx * timeInterval + (1 / 2) * Ax * (timeInterval * timeInterval);
		y += Vy * timeInterval + (1 / 2) * Ay * (timeInterval * timeInterval);


		// increment time
		currentTime += timeInterval;
	}



	public void SpawnPoint(Vector3 positionVector)
	{
		GameObject cBall = Instantiate(ball, positionVector, transform.rotation);
	}


	public float calculateMagnusForce(float vel, float liftCo)
	{
		float RadiansPerSecond = RPM * 0.10472f;
		float rotVelo = (radiusOfBall * RadiansPerSecond);

		//print("Recieved value of " + VELOCITY);
		//print("Radians per second: " + RadiansPerSecond + " Air Density: " + airDensity + " Area of the ball: " + areaOfBall + " Received Velocity: " + vel);


		float ah = (0.5f) * liftCo * airDensity * areaOfBall * (vel * vel) /* *rotVelo*VELOCITY)*/;


		// CONVERT FROM FORCE IN NEWTONS TO ACCELERATION AHAHAHHAHAHAHHAHAAAHAAHAAHAHHA
		ah /= mass;




		return ah;
	}


	public float calculatedD()
	{
		return (airDensity * dragCoefficient * areaOfBall) / 2;
	}


	void Start()
	{
		manager = gameObject.GetComponent<BallManager>();

		initAngle *= 180.0f / (float)Math.PI;


		initVelo = ConvertMPHtoMPS(initVelo);


		Vx = initVelo * (float)Math.Cos(initAngle);
		Vy = initVelo * (float)Math.Sin(initAngle);

		hasStarted = true;

	}


	public float ConvertMPHtoMPS(float mph)
	{
		float mps = mph / 2.237f;
		return mps;
	}

	public bool isBallAStrike()
	{
		if (transform.position.y > strikeZoneY.x && transform.position.y < strikeZoneY.y)
		{
			return true;
		}
		else
		{
			return false;
		}
	}


	void Update()
	{
		if (hasStarted)
		{
			if (manager.isPaused)
			{

			}
			else
			{
				if (transform.position.x < 18.44 && transform.position.y > 0.2f)
				{
					RotateByRotation(RPM);
					CalculatePositionLive();
				}
				else if (transform.position.x >= 18.44 || transform.position.y <= 0.2f)
				{
					if (!hasFinished)
					{
						timeTaken = currentTime;
						CalculatePositions();
						hasFinished = true;

						if (isBallAStrike())
						{
							print("STRIKE");
						}
						else
						{
							print("Ball");
						}

					}


				}

			}

		}




	}
}
