using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Shirotetsu
{
    public class Map : Singleton<Map>
    {
        public List<GameObject> AllObstacles => m_allObstacles;
        [SerializeField] private List<GameObject> m_allObstacles;

        [SerializeField] private GameObject m_obstaclesHolder;

        public GameObject player;

        public void SetUpAllObstacles()
        {
            foreach (Transform transformChild in m_obstaclesHolder.transform)
            {
                if (!m_allObstacles.Contains(transformChild.gameObject))
                {
                    m_allObstacles.Add(transformChild.gameObject);
                }
            }
        }
    }

    [CustomEditor(typeof(Map))]
    public class MapEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Map map = (Map)target;

            if (GUILayout.Button("SetUpAllObstacles"))
                map.SetUpAllObstacles();
        }
    }
}
