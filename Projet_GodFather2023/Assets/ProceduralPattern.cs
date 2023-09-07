using System.Collections;
using System.Collections.Generic;
using Shirotetsu;
using UnityEngine;

public class ProceduralPattern : MonoBehaviour
{
	[SerializeField] private List<ObstaclePattern> m_allEasyPattern;
	 
	private float m_timerBeforeNewSpawn;
	[SerializeField] private float m_defaultTimeBeforeNewSpawn = 2;
	[SerializeField] private float m_offsetBetweenPattern = 1;

	private int m_patternSpawnedCount;

	// Start is called before the first frame update
	void Start()
	{
		m_timerBeforeNewSpawn = m_defaultTimeBeforeNewSpawn;
	}

    // Update is called once per frame
    void Update()
    {
		if (m_timerBeforeNewSpawn > 0)
	    {
		    m_timerBeforeNewSpawn -= Time.deltaTime;

	    }
	    else
	    {
			/*if (Map.Instance.AllObstacles.Count > 0)
			{
				if (Vector3.Dot(Camera.main.transform.forward,
					    Map.Instance.AllObstacles[0].transform.position - Map.Instance.player.transform.position) < 0)
				{
					Map.Instance.AllObstacles[0].DestroyObstaclePattern();
				}
			}*/

			Vector3 posToSpawn = this.transform.position + ((Vector3.forward * (m_patternSpawnedCount + 1)) * m_offsetBetweenPattern);

			ObstaclePattern obstaclePattern = Instantiate(m_allEasyPattern[0], this.transform.position + posToSpawn, Quaternion.identity, Map.Instance.ObstaclesHolder.transform);

			m_patternSpawnedCount++;
			m_timerBeforeNewSpawn = m_defaultTimeBeforeNewSpawn;
		}
    }
}
