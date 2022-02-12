using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{

	public int mapWidth;
	public int mapHeight;
	public float noiseScale;

	public bool autoUpdate;

	public MapDisplay mapDisplay;

    public void Start()
    {
        GenerateMap();
    }
    public void GenerateMap()
	{
		float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

		mapDisplay = FindObjectOfType<MapDisplay>();
		mapDisplay.DrawNoiseMap(noiseMap);
	}

}