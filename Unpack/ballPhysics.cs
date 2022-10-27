using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ballPhysics : MonoBehaviour
{

	public bool shouldUseMagnusForce = true;

	public float timeModifier = 1;

	public bool isSimPaused = true;

	public Vector3 movementVector;
	Vector3 previousMovementVector;

	public Vector3 velocityVector;


	public float dragIndicatorScale = 1;



	public float mass = 0.145f; // kg

	public float radius = 0.03683f; //m



	public float gravity = 9.8f;


	public float dragCoefficient = 0.3f; // https://www1.grc.nasa.gov/beginners-guide-to-aeronautics/drag-on-a-baseball/

	public float airDensity; 

	public float projectileSpeed; //miles per hour

	public float projVelocity;

	public float crossSectionalArea = 0.00426f; //m^2




	public float initialVelocity;

	public float airDrag;

	public float magnusForce; // 


	public float initialPositionX;

	public float initialPositionY;

	public float initialAngle;


	public float liftCoefficient = 0.22f;




	public worldValues world;

	public labelManager label;

	public lou manager;

	public float time = 0; // seconds

	public bool isInFlight = false;


	List<Vector3> ballPos = new List<Vector3>();
	LineRenderer lineRenderer;

	public Material lineMaterial;













	//

	public Vector3 positionVector = new Vector3(0, 0, 0);

    public double D()
	{
		double r;

		r = (airDensity * dragCoefficient * crossSectionalArea) / 2;

		return r;
	}


	public float savedVox;
	public float savedVoy;


    public Vector3 ReturnPositionUpdate()
	{
		double Fm = calculateMagnusForce();
		print("Magnus force: " + Fm);

		double g = -9.8;
		print("Gravity: " + g);

		double Ay = (Fm + g*mass) / mass;
		print("Ay: " + Ay);

		double Ax = -D() / mass;
		print("Ax: " + Ax);

		Vector2 initVeloAngle = new Vector2((float)Math.Cos(initialAngle), (float)Math.Sin(initialAngle));

		//double Vox = (initialVelocity * (float)Math.Cos(initialAngle)); //(savedVox + Ax * time) * initVeloAngle.x;
		double Vox = savedVox+  Ax * time; // yes
		print("Velocity of X: " + Vox + "D() = " + D());
		double Voy = (savedVoy + Ay * time); //


		double posX = Vox * time + (1 / 2) * Ax * (time * time); 
		double posY = Voy * time + (1 / 2) * Ay * (time * time);

		savedVox = (float)Vox;
		savedVoy = (float)Voy; // nope

		return new Vector3((float)posX -4.5f, /*transform.position.y*/(float)posY, 0);


	}









	//



	public void setPosition()
	{
		movementVector.x = xPosition();
		movementVector.y = yPosition();
		movementVector.z = 0;
	}


    public float xPosition()
	{
		float r = initialPositionX + (initialVelocity * time);
		return r;
	}

    public float yPosition()
	{
		float r = initialPositionY + (xPosition() * (float)Math.Tan(initialAngle)) +  ( (gravity * (xPosition() * xPosition())) / (2 * (initialVelocity*initialVelocity) * ((float)Math.Cos(initialAngle) * (float)Math.Cos(initialAngle))));

		return r;
	}





    public float forceGravity()
	{
		return (-1) * (mass * gravity);
	}
    

	public float calculateDragForce()
	{
		float ret;

		/*print("Drag coefficient: " + dragCoefficient);
		print("Air Density: " + airDensity + "kg/m^3");
		print("Area: " + crossSectionalArea + "m^2");
		print("Velocity: " + projVelocity + "m/s");*/

		ret = (0.5f) * dragCoefficient * airDensity * crossSectionalArea * (initialVelocity* initialVelocity);
		//print(ret + "");

		return ret;
	}


    public void applyMovementVector()
	{

		transform.position = ReturnPositionUpdate();
        
		//transform.position = movementVector;
        if(isInFlight)
		{
			ballPos.Add(transform.position);
			DrawLine();
		}
		
		
	}


    public void die()
	{

		manager.killMe(this.gameObject);
		
	}


    void FixedUpdate()
	{

	}


    // Start is called before the first frame update
    void Start()
    {

		

		projVelocity = projectileSpeed / 2.237f;
		initialVelocity = projVelocity;

		//print(initialPositionY + "ah"); Maybe?

		transform.position = new Vector3(initialPositionX, initialPositionY, 0); //but why not y aixs i gothcu it sets it to the correct place at the start, its in the math OH SHIT IM DUBM


		savedVox = (initialVelocity * (float)Math.Sin(initialAngle * (180/Math.PI)));
		savedVoy = (initialVelocity * (float)Math.Cos(initialAngle * (180 / Math.PI)));
		//print("" + (float)(Math.Sin(90))); //

		print("init velo: " + initialVelocity + " savedVox: " + savedVox + " savedVoy: " + savedVoy);//

        
		lineRenderer = gameObject.AddComponent<LineRenderer>();

		lineRenderer.material = lineMaterial;
		Color red = Color.red;
		lineRenderer.SetColors(red, red);
		lineRenderer.SetWidth(0.1F, 0.1F);

		/*lineRenderer.SetVertexCount(10);

		for (int i = 0; i < 10; i ++)
		{
			Vector3 t = CalculatePositionAtTime(i * 0.1f);
			lineRenderer.SetPosition(i, t);
			
		}*/
        

		//airDensity = world.airDensity;
		//print("Current drag force: " + calculateDragForce());
        
    }


    public Vector3 CalculatePositionAtTime(double time1)
	{
		time = (float)time1;
		float x = xPosition();
		float y = yPosition();

		print("At time " + time + " this ball is at " + x + "," + y);

		return new Vector3(x,y, 0);
	}



	/*public Vector3 CalculateDragAtTime(double time1)
	{
		time = (float)time1;
		float x = xPosition();
		float y = yPosition();

		print("At time " + time + " this ball is at " + x + "," + y);

		calculateDragForce();

		//return new Vector3(x, y, 0);
	}*/

	public Vector3 getIdentityVector(Vector3 velocityVector)
	{
		Vector3 vIdentity;

		if (velocityVector.x > 0)
		{
			vIdentity.x = 1;
		}
		else if (velocityVector.x < 0)
		{
			vIdentity.x = -1;
		}
		else
		{
			vIdentity.x = 0;
		}


		if (velocityVector.y > 0)
		{
			vIdentity.y = 1;
		}
		else if (velocityVector.y < 0)
		{
			vIdentity.y = -1;
		}
		else
		{
			vIdentity.y = 0;
		}


		if (velocityVector.z > 0)
		{
			vIdentity.z = 1;
		}
		else if (velocityVector.z < 0)
		{
			vIdentity.z = -1;
		}
		else
		{
			vIdentity.z = 0;
		}

		return vIdentity;



	}

    public void RestartSim()
	{
		//movementVector.x = initialPositionX;
		//movementVector.y = initialPositionY;
		//movementVector.z = 0;

		transform.position = new Vector3(initialPositionX, initialPositionY, 0);


		savedVox = (initialVelocity * (float)Math.Sin(initialAngle * (180 / Math.PI)));
		savedVoy = (initialVelocity * (float)Math.Cos(initialAngle * (180 / Math.PI)));

		//transform.position = Vector3.Zero;

		ballPos.Clear();

		//applyMovementVector();



	}


    public void DrawLine()
	{

		lineRenderer.SetVertexCount(ballPos.Count);

		for (int i = 0; i < ballPos.Count; i++)
		{
			//Change the postion of the lines
			lineRenderer.SetPosition(i, ballPos[i]);
		}
	}


    public void MainMovement()
	{

		


		if (movementVector.x > 18.44f && !isSimPaused)
		{
			//print("DIE"); dont worry about it
			//die();

		}
		else if(!isSimPaused)
		{
			applyMovementVector();
			setPosition();

			velocityVector = (movementVector - previousMovementVector) / Time.deltaTime;
			previousMovementVector = movementVector;

			Vector3 identity = getIdentityVector(velocityVector);


			//Vector3 dragVector = identity * calculateDragForce(); 



			// BACKSPIN MAGNUS VECTOR:
			Vector3 magnusVector = new Vector3(0, calculateMagnusForce(), 0);

			Vector2 initVeloAngle = new Vector2((float)Math.Cos(initialAngle), (float)Math.Sin(initialAngle));

			// acceleration:
			float accel = -calculateDragForce() / mass;
			float velo = initialVelocity + accel * time;


			float yAccel = (gravity - calculateMagnusForce()) / mass;
			float yVelo = initialVelocity + yAccel * time;

			Vector3 freeBody = new Vector3(velo, yVelo, 0);

			//movementVector += freeBody;

			//movementVector += dragVector;

			if (shouldUseMagnusForce)
			{
				//movementVector += magnusVector;
			}
			

            if(velocityVector.x > 0)
			{
				isInFlight = true;
			}
			else
			{
				isInFlight = false;
			}


			


			if (!isSimPaused)
			{

				Debug.DrawLine(movementVector, movementVector + velocityVector, Color.blue);
				//Debug.DrawLine(movementVector, movementVector + (dragVector * dragIndicatorScale), Color.red);
				//Debug.DrawLine(movementVector, movementVector + new Vector3(0, 1, 0) * calculateMagnusForce(), Color.black);
			}
			else
			{
				Debug.DrawLine(movementVector, movementVector + velocityVector, Color.blue);
				//Debug.DrawLine(movementVector, movementVector + (dragVector * dragIndicatorScale), Color.red);
				//Debug.DrawLine(movementVector, movementVector + new Vector3(0, 1, 0) * calculateMagnusForce(), Color.black);
			}


			label.UpdateLabel(movementVector, velocityVector);

		}


		
	}


    public float calculateMagnusForce()
	{
		// 1/2 * liftCoefficient * airDensity * cross-SectionalArea * velocity^2

		; // temp value, more calculations needed per http://spiff.rit.edu/richmond/baseball/traj/traj.html

		float mForce = (0.5f) * liftCoefficient * airDensity * crossSectionalArea * (projVelocity * projVelocity);

		return mForce;


	}

    // Update is called once per frame
    void Update()
    {
        

		if (transform.position.y > 0.3f || transform.position.x > 18.44f)
		{

			if (!isSimPaused)
			{
				time += Time.deltaTime * timeModifier;

				MainMovement();



			}

			


		}
		else
		{
			die();
		}

        
    }
}
