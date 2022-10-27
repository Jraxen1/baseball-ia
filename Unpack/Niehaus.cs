using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Niehaus : MonoBehaviour
{

	public GameObject ball;

	public Transform spawnTransform;


	public TMP_InputField newBallSpeed;

	public TMP_InputField newBallLiftCoefficient;

	public Slider timeSlider;

	public List<GameObject> ballsInScene = new List<GameObject>();

	public bool isSimPaused = true;
	public bool hasSimStarted = false;


	public int currentCam = 0;
	public GameObject[] cameras = new GameObject[2];


	public GameObject ThreeDVisuals;
	public GameObject TwoDVisuals;

	public float strikeZoneMinY;
	public float strikeZoneMaxY;


	public int currentSpin = 0; // 0 - backspin, 1 - frontspin



    public void SetSpin(int toSet)
	{
		//print("set to " + toSet);
		currentSpin = toSet;
	}



    // UNDER ASSUMPTION THAT THERE IS A MAX OF 5 BALLS
	public Color[] possibleColors = new Color[5];


    public void StartSim()
	{
		for (int i = 0; i < ballsInScene.Count; i++)
		{
			ballsInScene[i].GetComponent<BallManager>().StartSim();
		}
		hasSimStarted = true;
	}


    public void RestartSim()
	{
		isSimPaused = false;

		List<GameObject> tempStorage = new List<GameObject>();
		for (int i = 0; i < ballsInScene.Count; i++)
		{
			tempStorage.Add(ballsInScene[i]);
		}

		ClearSim();

        for(int i = 0; i < tempStorage.Count; i++)
		{
			RespawnBall(tempStorage[i].GetComponent<LiteralActualMath>().unchangedInitVelo, tempStorage[i].GetComponent<LiteralActualMath>().RPM, tempStorage[i].GetComponent<LiteralActualMath>().isBackSpin);
		}

		tempStorage.Clear();
	}


    public void PauseSim()
	{
        for(int i = 0; i < ballsInScene.Count; i++)
		{
			ballsInScene[i].GetComponent<BallManager>().PauseSim();
		}
	}

    public void ResumeSim()
	{
		for (int i = 0; i < ballsInScene.Count; i++)
		{
			ballsInScene[i].GetComponent<BallManager>().ResumeSim();
		}
	}



    public void ModifyTimeModifier()
	{
		for (int i = 0; i < ballsInScene.Count; i++)
		{

			ballsInScene[i].GetComponent<BallManager>().ModifyTimeModifier(timeSlider.value);
		}
	}



    public void SwitchCam()
	{

        // CAMERAS GREATER THAN 0 ARE 3D
        // 0 = 2D

		int nextCam = currentCam + 1;
        if(nextCam >= cameras.Length)
		{
			nextCam = 0;
		}

        if(nextCam != 0)
		{
			ThreeDVisuals.SetActive(true);
			TwoDVisuals.SetActive(false);
		}
		else
		{
			ThreeDVisuals.SetActive(false);
			TwoDVisuals.SetActive(true);
		}

		cameras[nextCam].SetActive(true);
		cameras[currentCam].SetActive(false);

		currentCam = nextCam;
	}



    public void RespawnBall(float ballSpeed, float RPM, bool isBackSpin)
	{
		GameObject cBall = Instantiate(ball, spawnTransform.position, spawnTransform.rotation);
		LiteralActualMath ballP = cBall.GetComponent<LiteralActualMath>();
		cBall.GetComponent<LiteralActualMath>().strikeZoneY = new Vector2(strikeZoneMinY, strikeZoneMaxY);

		cBall.GetComponent<LiteralActualMath>().isBackSpin = isBackSpin;

		//ballP.airDensity = world.airDensity;

		//ballP.initialPositionX = spawnTransform.position.x; // this sets it to be x = 0, y = 1.88 or about that
		//ballP.initialPositionY = spawnTransform.position.y;


		//ballP.liftCoefficient = liftCoefficient;

		ballP.RPM = RPM;

		ballP.initVelo = ballSpeed;
		ballP.unchangedInitVelo = ballSpeed;

		int colorCount = ballsInScene.Count;




		if (ballsInScene.Count > 4)
		{
			colorCount = ballsInScene.Count - 5;
		}

		cBall.GetComponent<MovementTrail>().lineColor = possibleColors[colorCount];

		if (hasSimStarted)
		{
			cBall.GetComponent<BallManager>().StartSim();
		}

		cBall.GetComponent<BallManager>().ModifyTimeModifier(timeSlider.value);

		//cBall.GetComponent<BallManager>().PauseSim();


		//ballP.manager = this.gameObject.GetComponent<lou>();


		ballsInScene.Add(cBall);
	}



    public void SpawnNewBall()
	{
		GameObject cBall = Instantiate(ball, spawnTransform.position, spawnTransform.rotation);
		LiteralActualMath ballP = cBall.GetComponent<LiteralActualMath>();

		//ballP.airDensity = world.airDensity;

		//ballP.initialPositionX = spawnTransform.position.x; // this sets it to be x = 0, y = 1.88 or about that
		//ballP.initialPositionY = spawnTransform.position.y;

		ballP.initVelo = float.Parse(newBallSpeed.text);
		ballP.unchangedInitVelo = float.Parse(newBallSpeed.text);
		cBall.GetComponent<LiteralActualMath>().strikeZoneY = new Vector2(strikeZoneMinY, strikeZoneMaxY);

		int colorCount = ballsInScene.Count;

        if(currentSpin == 0)
		{
			cBall.GetComponent<LiteralActualMath>().isBackSpin = true;
		}
		else
		{
			cBall.GetComponent<LiteralActualMath>().isBackSpin = false;
		}


        if(ballsInScene.Count > 4)
		{
			colorCount = ballsInScene.Count - 5;
		}

		cBall.GetComponent<MovementTrail>().lineColor = possibleColors[colorCount];

		/*if (colorCount > possibleColors.Length - 1)
		{
			
		}
		else
		{
			cBall.GetComponent<MovementTrail>().lineColor = possibleColors[0];
		}*/

		

		if (hasSimStarted)
		{
			cBall.GetComponent<BallManager>().StartSim();
		}

		/* if(float.Parse(newBallLiftCoefficient.text) != 0.22f)
		 {
			 cBall.GetComponent<LiteralActualMath>().liftCoefficient = float.Parse(newBallLiftCoefficient.text);
		 }*/

		cBall.GetComponent<LiteralActualMath>().RPM = float.Parse(newBallLiftCoefficient.text);



		cBall.GetComponent<BallManager>().ModifyTimeModifier(timeSlider.value);

		//cBall.GetComponent<BallManager>().PauseSim();


		//ballP.manager = this.gameObject.GetComponent<lou>();


		ballsInScene.Add(cBall);
	}




	public void SpawnNewBallFromCode(float initVelo, float rpm)
	{
		GameObject cBall = Instantiate(ball, spawnTransform.position, spawnTransform.rotation);
		LiteralActualMath ballP = cBall.GetComponent<LiteralActualMath>();

		//ballP.airDensity = world.airDensity;

		//ballP.initialPositionX = spawnTransform.position.x; // this sets it to be x = 0, y = 1.88 or about that
		//ballP.initialPositionY = spawnTransform.position.y;

		ballP.initVelo = initVelo;
		cBall.GetComponent<LiteralActualMath>().strikeZoneY = new Vector2(strikeZoneMinY, strikeZoneMaxY);

		int colorCount = ballsInScene.Count;

		if (currentSpin == 0)
		{
			cBall.GetComponent<LiteralActualMath>().isBackSpin = true;
		}
		else
		{
			cBall.GetComponent<LiteralActualMath>().isBackSpin = false;
		}


		if (ballsInScene.Count > 4)
		{
			colorCount = ballsInScene.Count - 5;
		}

		cBall.GetComponent<MovementTrail>().lineColor = possibleColors[colorCount];

		if (colorCount > possibleColors.Length - 1)
		{
			
		}
		else
		{
			cBall.GetComponent<MovementTrail>().lineColor = possibleColors[0];
		}



		if (hasSimStarted)
		{
			cBall.GetComponent<BallManager>().StartSim();
		}

		/* if(float.Parse(newBallLiftCoefficient.text) != 0.22f)
		 {
			 cBall.GetComponent<LiteralActualMath>().liftCoefficient = float.Parse(newBallLiftCoefficient.text);
		 }*/

		cBall.GetComponent<LiteralActualMath>().RPM = rpm;



		cBall.GetComponent<BallManager>().ModifyTimeModifier(timeSlider.value);

		//cBall.GetComponent<BallManager>().PauseSim();


		//ballP.manager = this.gameObject.GetComponent<lou>();
	}





	public void ClearSim()
	{
		for (int i = 0; i < ballsInScene.Count; i++)
		{
			Destroy(ballsInScene[i]);
		}

		ballsInScene.Clear();
	}


    

    public void TogglePause()
	{
		if (isSimPaused)
		{
			ResumeSim();
		}
		else
		{
			PauseSim();
		}

		isSimPaused = !isSimPaused;
	}




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

		if (Input.GetKeyDown(KeyCode.P))
		{
			TogglePause();
		} else if (Input.GetKeyDown(KeyCode.S))
		{
			StartSim();
		} else if (Input.GetKeyDown(KeyCode.R))
		{
			RestartSim();
		} else if (Input.GetKeyDown(KeyCode.C))
		{
			ClearSim();
		} else if (Input.GetKeyDown(KeyCode.O))
		{
			SwitchCam();
		}

        


    }
}
