using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class lou : MonoBehaviour
{

	public TMP_InputField timeInput;

	public float timeModifer;


	public Image togglePlayButtonImage;

	public Sprite[] playPause;


	public Slider timeSlider;

    
	public TMP_InputField newBallSpeed;


	public Transform spawnTransform;

	public GameObject ball;

	public worldValues world;

	public float ballSpeedMPH;



	public float currentTime;



	public bool isSimPaused = true;



	public List<GameObject> ballsInSim = new List<GameObject>();

    public void killMe(GameObject toKill)
	{
		//ballsInSim.Remove(toKill);
		//Destroy(toKill);
	}


    public void AdjustTimeModifier()
	{
		timeModifer = timeSlider.value;
	}


    public void UpdateTime()
	{

		currentTime = float.Parse(timeInput.text)*timeModifer;
		for (int i = 0; i < ballsInSim.Count; i++)
		{


			ballsInSim[i].GetComponent<ballPhysics>().time = currentTime;
			ballsInSim[i].GetComponent<ballPhysics>().MainMovement();
		}
	}


    public void PauseSim()
	{
        for(int i = 0; i < ballsInSim.Count; i++)
		{
			ballsInSim[i].GetComponent<ballPhysics>().isSimPaused = true;
		}
	}

    public void UnpauseSim()
	{

		for (int i = 0; i < ballsInSim.Count; i++)
		{
			ballsInSim[i].GetComponent<ballPhysics>().isSimPaused = false;
		}

	}

    public void UpdateTimeModifier()
	{
		for (int i = 0; i < ballsInSim.Count; i++)
		{
			ballsInSim[i].GetComponent<ballPhysics>().timeModifier = timeModifer;
		}
	}


    public void spawnBall()
	{
		GameObject cBall = Instantiate(ball, spawnTransform.position, spawnTransform.rotation);
		ballPhysics ballP = cBall.GetComponent<ballPhysics>();

		ballP.airDensity = world.airDensity;

		ballP.initialPositionX = spawnTransform.position.x; // this sets it to be x = 0, y = 1.88 or about that
		ballP.initialPositionY = spawnTransform.position.y;

		ballP.projectileSpeed = float.Parse(newBallSpeed.text);

		ballP.manager = this.gameObject.GetComponent<lou>();


        

		ballsInSim.Add(cBall);
	}



    public void TogglePause()
	{
		if (isSimPaused)
		{
			UnpauseSim();
			togglePlayButtonImage.sprite = playPause[1];
            
		}
		else
		{
			PauseSim();
			togglePlayButtonImage.sprite = playPause[0];
		}

		isSimPaused = !isSimPaused;
	}

    public void restartSim(int numberOfBalls)
	{
		currentTime = 0;
		timeInput.text = currentTime + "";

		/*
        for(int i = 0; i < numberOfBalls; i++)
		{

			GameObject cBall = Instantiate(ball, spawnTransform.position, spawnTransform.rotation);
			ballPhysics ballP = cBall.GetComponent<ballPhysics>();

			ballP.airDensity = world.airDensity;

			ballP.initialPositionX = spawnTransform.position.x;
			ballP.initialPositionY = spawnTransform.position.y;

			ballP.projectileSpeed = ballSpeedMPH;
			ballP.manager = this.gameObject.GetComponent<lou>();

			ballsInSim.Add(cBall);

		}*/
		UpdateTimeModifier();
		UpdateTime();

		for (int i = 0; i < ballsInSim.Count; i++)
		{
			ballsInSim[i].GetComponent<ballPhysics>().RestartSim();
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		

	}

    // Update is called once per frame
    void Update()
    {
		timeInput.text = currentTime+"";
		if (!isSimPaused)
		{
			togglePlayButtonImage.sprite = playPause[1];
			currentTime += Time.deltaTime*timeModifer;

		}
		else
		{
			togglePlayButtonImage.sprite = playPause[0];
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			restartSim(1);
		} else if (Input.GetKeyDown(KeyCode.P))
		{
			if (isSimPaused)
			{
				UnpauseSim();
			}
			else
			{
				PauseSim();
			}

			isSimPaused = !isSimPaused;
		}
    }
}
