using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTrail : MonoBehaviour
{

	List<Vector3> ballPos = new List<Vector3>();
	LineRenderer lineRenderer;

	public Material lineMaterial;


	public bool useInterval = false;
	public float secondInterval;

	public Color lineColor;


    public void UpdateLines()
	{
		ballPos.Add(transform.position);
		DrawLine();
	}


	public void DrawLine()
	{

		lineRenderer.SetVertexCount(ballPos.Count);

		for (int i = 0; i < ballPos.Count; i++)
		{
			lineRenderer.SetPosition(i, ballPos[i]);
		}
	}



	// Start is called before the first frame update
	void Start()
    {
		lineRenderer = gameObject.AddComponent<LineRenderer>();

		lineRenderer.material = lineMaterial;
		Color red = lineColor;
		lineRenderer.SetColors(red, red);
		lineRenderer.SetWidth(0.05F, 0.05F);
	}

    public void SpawnTrail(List<Vector3> trailPositions)
	{
		lineRenderer.SetVertexCount(trailPositions.Count);

		for (int i = 0; i < trailPositions.Count; i++)
		{
			lineRenderer.SetPosition(i, trailPositions[i]);
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (useInterval)
		{

		}
		else
		{
			//UpdateLines();
		}
    }
}
