#if UNITY_EDITOR

using UnityEngine;
using System.Collections.Generic;

namespace Utility
{
    [ExecuteInEditMode]
    public class SnapPointGenerator : MonoBehaviour
    {
        // the prefab of the snap point to be instantiated
        [SerializeField] private Transform snapPointPrefab;
        [SerializeField] private int numSnapPoints = 6;
        // distance between the snap point's center and the snap node's center.
        [SerializeField] private float snapPointDist = 0.35f;

        [SerializeField] private List<Transform> snapPoints = new();

        [ContextMenu("Generate Snap Points")]
        public void GenerateSnapPoints()
        {
            // clear the previous snap points
            DestroySnapPoints();
            // calculate the rotation interval of the snap points based on the total number of snap points
            float degreeInterval = 360f / numSnapPoints;
            // starting from the right of the node, instantiate the snap points counterclockwise with equal rotation intervals
            for (int i = 0; i < numSnapPoints; i++)
            {
                // create a new snap point
                Transform snapPoint = Instantiate(snapPointPrefab, transform);
                // calculate the direction of the snap point, each point is fixed degrees from the previous one
                Vector3 dir = Quaternion.Euler(0, 0, i * degreeInterval) * transform.right;
                // calculate the position of the snap point
                snapPoint.position = transform.position + snapPointDist * dir;
                
                // add the snap point to the list
                snapPoints.Add(snapPoint);
            }
        }

        [ContextMenu("DestroySnapPoints")]
        public void DestroySnapPoints()
        {
            for (int i = 0; i < snapPoints.Count; i++)
            {
                if (snapPoints[i] == null) continue;
                DestroyImmediate(snapPoints[i].gameObject);
            }

            snapPoints.Clear();
        }
    }

}
#endif