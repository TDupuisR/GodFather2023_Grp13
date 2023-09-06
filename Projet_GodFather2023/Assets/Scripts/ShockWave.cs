using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Shirotetsu
{
    public class ShockWave : MonoBehaviour
    {
        [SerializeField] private float m_shockWaveMaxRange = 5;

        [SerializeField] private int m_amountGainPickUpPowerUp = 20;
        [SerializeField] private int m_currentPowerUpGauge = 0;

        private Sequence m_destroyObstaclesSequence;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (m_currentPowerUpGauge >= 100)
                {
                    ActivateShockWaves();
                }
            }
        }

        private void ActivateShockWaves()
        {
            foreach (GameObject obstacles in ObstaclesInShockWaveRange())
            {
                Map.Instance.AllObstacles.Remove(obstacles);

                m_destroyObstaclesSequence = DOTween.Sequence();

                m_destroyObstaclesSequence.Append(obstacles.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f));
                m_destroyObstaclesSequence.Append(obstacles.transform.DOScale(0, 0.1f).OnComplete(() => Destroy(obstacles)));
            }

            m_currentPowerUpGauge = 0;
        }

        private List<GameObject> ObstaclesInShockWaveRange()
        {
            List<GameObject> _ObstaclesInShowWaveRange = new List<GameObject>();

            for (int i = 0; i < Map.Instance.AllObstacles.Count; i++)
            {
                Vector3 PlayerToObstacles = Map.Instance.AllObstacles[i].transform.position - Map.Instance.player.transform.position;

                if (PlayerToObstacles.sqrMagnitude <= m_shockWaveMaxRange * m_shockWaveMaxRange)
                {
                    _ObstaclesInShowWaveRange.Add(Map.Instance.AllObstacles[i]);
                }   
            }

            return _ObstaclesInShowWaveRange;
        }

        public void IncreaseGaugePowerUp()
        {
            m_currentPowerUpGauge = Mathf.Clamp(m_currentPowerUpGauge + m_amountGainPickUpPowerUp, 0, 100);
        }

        public void IncreaseGaugePowerUpCustomAmount( int _customAmountToAdd )
        {
            m_currentPowerUpGauge += Mathf.Clamp(m_currentPowerUpGauge + _customAmountToAdd, 0, 100);
        }

        private void OnDestroy()
        {
            m_destroyObstaclesSequence.Kill();
        }
    }

    [CustomEditor(typeof(ShockWave))]
    public class ShockWaveEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ShockWave shockWave = (ShockWave)target;

            if (GUILayout.Button("IncreaseGaugePowerUp"))
                shockWave.IncreaseGaugePowerUp();
        }
    }
}
