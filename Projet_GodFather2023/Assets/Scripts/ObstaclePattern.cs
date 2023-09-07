using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Shirotetsu;

public class ObstaclePattern : MonoBehaviour
{
    [Header("Patern")]
    [SerializeField] private GameObject m_allObstaclesHolder;
    [SerializeField] private List<GameObject> m_allObstaclesInParttern = new List<GameObject>();

    [Header("Debug / Editor")]
    [SerializeField] private List<GameObject> m_allPos = new List<GameObject>();

    private List<GameObject> m_allCubes = new List<GameObject>();

    private Sequence m_destroyObstaclesSequence;

    private void Start()
    {
        Map.Instance.AllObstacles.Add(this);
    }

    public void DestroyAllObstaclesByShockWave()
    {
        foreach (GameObject obstacles in m_allObstaclesInParttern)
        {
            m_destroyObstaclesSequence = DOTween.Sequence();

            m_destroyObstaclesSequence.Append(obstacles.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f));
            m_destroyObstaclesSequence.Append(obstacles.transform.DOScale(0, 0.1f).OnComplete(() => Destroy(obstacles)));
            m_destroyObstaclesSequence.Append(this.transform.DOScale(0, 0.01f).OnComplete(() => Destroy(this.gameObject)));
        }
    }

    public void SetUpAllObstacles()
    {
        m_allObstaclesInParttern.Clear();

        foreach (Transform transformChild in m_allObstaclesHolder.transform)
        {
            if (!m_allObstaclesInParttern.Contains(transformChild.gameObject))
            {
                m_allObstaclesInParttern.Add(transformChild.gameObject);
            }
        }
    }

    public void InstantiateAllCube()
    {
        foreach (GameObject gameObject in m_allPos)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = gameObject.transform.position;
            cube.transform.parent = gameObject.transform;

            cube.transform.localScale = Vector3.one * 0.9f;

            //allCubes.Add(cube);
        }
    }

    public void DeleteAllCube()
    {
        foreach (GameObject gameObject in m_allPos)
        {
            DestroyImmediate(gameObject.transform.GetChild(0));
        }
    }

    private void OnValidate()
    {
        SetUpAllObstacles();
    }
}

[CustomEditor(typeof(ObstaclePattern))]
public class ObstaclePaternEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ObstaclePattern obstaclePatern = (ObstaclePattern)target;

        if (GUILayout.Button("SetUpAllObstacles"))
            obstaclePatern.SetUpAllObstacles();

        /*EditorGUILayout.Space();

        if (GUILayout.Button("InstantiateAllCube"))
            obstaclePatern.InstantiateAllCube();

        if (GUILayout.Button("DeleteAllCube"))
            obstaclePatern.DeleteAllCube();*/
    }
}
