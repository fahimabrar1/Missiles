using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class IndicatorManager : MonoBehaviour
    {
        public Transform indicatorParent; // Parent transform for the indicators
        public Camera mainCamera; // Reference to the main camera
        public List<IIndicator> activeIndicators = new();

        private void Awake()
        {
            if (mainCamera == null)
                mainCamera = Camera.main; // Assign the main camera if not set
        }

        /// <summary>
        ///     Creates an indicator for the given target.
        /// </summary>
        /// <param name="target">The transform of the target object.</param>
        public IIndicator CreateIndicator(GameObject indicatorPrefab, Transform target)
        {
            if (indicatorPrefab == null || indicatorParent == null || mainCamera == null)
            {
                MyDebug.LogWarning("IndicatorManager: Missing references!");
                return null;
            }

            // Instantiate the indicator
            var indicator = Instantiate(indicatorPrefab, Vector3.zero, Quaternion.identity, indicatorParent);
            var indicatorScript = indicator.GetComponent<IIndicator>();
            activeIndicators.Add(indicatorScript);
            indicatorScript?.Initialize(target, mainCamera);

            return indicatorScript;
        }

        public void DestroyAllIndicators()
        {
            foreach (var t in activeIndicators)
                t.OnDestroyIndicatorTarget();
        }
    }
}