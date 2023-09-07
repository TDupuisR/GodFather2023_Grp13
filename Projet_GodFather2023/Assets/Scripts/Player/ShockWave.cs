using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Shirotetsu
{
    public class ShockWave : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ShockWaveUIManager m_shockWUIManager;

        [Header("Player Input")]
        [SerializeField] InputActionReference m_shockWaveButton;

        [Header("Parameters")]
        [SerializeField] private float m_shockWaveMaxRange = 5;

        [SerializeField] private int m_amountGainPickUpPowerUp = 20;

        public int m_currentPowerUpGauge = 0;

        [Header("Sounds")]
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_shockwaveSound;

        private Sequence m_destroyObstaclesSequence;

        public void Update()
        {
            if (m_shockWaveButton.action.ReadValue<float>() != 0)
            {
                if (m_currentPowerUpGauge >= 100)
                {
                    PlaySound(m_shockwaveSound);
                    ActivateShockWaves();
                }
            }
        }

        private void ActivateShockWaves()
        {
            foreach (ObstaclePattern obstaclesPattern in ObstaclesInShockWaveRange())
            {
                Map.Instance.AllObstacles.Remove(obstaclesPattern);

                obstaclesPattern.DestroyAllObstaclesByShockWave();
            }

            m_currentPowerUpGauge = 0;
            m_shockWUIManager.UpdateGauge(0);
        }

        private List<ObstaclePattern> ObstaclesInShockWaveRange()
        {
            List<ObstaclePattern> _ObstaclesInShowWaveRange = new List<ObstaclePattern>();

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
            m_shockWUIManager.UpdateGauge(m_currentPowerUpGauge);
        }

        public void IncreaseGaugePowerUpCustomAmount( int _customAmountToAdd )
        {
            m_currentPowerUpGauge += Mathf.Clamp(m_currentPowerUpGauge + _customAmountToAdd, 0, 100);
        }

        private void OnDestroy()
        {
            m_destroyObstaclesSequence.Kill();
        }

        void PlaySound(AudioClip Sound)
        {
            m_audioSource.clip = Sound;
            m_audioSource.Play();
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
