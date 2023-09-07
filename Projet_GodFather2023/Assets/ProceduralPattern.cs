using System.Collections;
using System.Collections.Generic;
using Shirotetsu;
using UnityEngine;

public class ProceduralPattern : MonoBehaviour
{
	[Header("All Pattern Prefabs")]
	[SerializeField] private List<ObstaclePattern> m_allEasyPatternPrefabs;
	[SerializeField] private List<ObstaclePattern> m_allMediumPatternPrefabs;
	[SerializeField] private List<ObstaclePattern> m_allHardPatternPrefabs;

	private float m_timerBeforeNewSpawn;
	[SerializeField] private float m_defaultTimeBeforeNewSpawn = 2;
	[SerializeField] private float m_offsetBetweenPattern = 1;

	private int m_patternSpawnedCount;

	private float m_randomChanceEasyPattern = 1f;
	private float m_randomChanceMediumPattern = 0.3f;
	private float m_randomChanceHardPattern = 0.1f;

	// Start is called before the first frame update
	void Start ()
	{
		m_timerBeforeNewSpawn = m_defaultTimeBeforeNewSpawn;
	}

	// Update is called once per frame
	void Update ()
	{
		if (m_timerBeforeNewSpawn > 0)
		{
			m_timerBeforeNewSpawn -= Time.deltaTime;

		}
		else
		{
			DeletePatternBehind();

			SpawnPatternInFront();

			m_patternSpawnedCount++;
			m_timerBeforeNewSpawn = m_defaultTimeBeforeNewSpawn;
		}
	}

	private void SpawnPatternInFront ()
	{
		Vector3 posToSpawn = this.transform.position + ((Vector3.forward * (m_patternSpawnedCount + 1)) * m_offsetBetweenPattern);

		ObstaclePattern obstaclePattern = Instantiate(
			ChoosePatternToSpawn(),
			this.transform.position + posToSpawn,
			Quaternion.identity,
			Map.Instance.ObstaclesHolder.transform);
	}

	private ObstaclePattern ChoosePatternToSpawn ()
	{
		float random = Random.Range(0f, 1f);

		m_randomChanceMediumPattern += 0.01f;
		m_randomChanceHardPattern += 0.01f;

		ObstaclePattern RandomPrefab ( List<ObstaclePattern> patternPrefabsToRandom )
		{
			int randomPrefab = Random.Range(0, patternPrefabsToRandom.Count);

			return patternPrefabsToRandom[randomPrefab];
		}

		if (random <= m_randomChanceHardPattern)
		{
			return RandomPrefab(m_allHardPatternPrefabs);
		}
		else if (random <= m_randomChanceMediumPattern)
		{
			return RandomPrefab(m_allMediumPatternPrefabs);
		}
		else
		{
			return RandomPrefab(m_allEasyPatternPrefabs);
		}
	}

	private void DeletePatternBehind ()
	{
		if (Map.Instance.AllObstacles.Count > 0)
		{
			if (Vector3.Dot(Camera.main.transform.forward,
					Map.Instance.AllObstacles[0].transform.position - Map.Instance.player.transform.position) < 0)
			{
				Map.Instance.AllObstacles[0].DestroyObstaclePattern();
			}
		}
	}
}
