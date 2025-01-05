using UnityEngine;

namespace Skills
{
    public class SkillPoint : MonoBehaviour
    {
        public enum SkillPointType
        {
            Speed,
            Shield,
            Point
        }

        public SkillPointType skillPointType = SkillPointType.Point;

        public SpriteRenderer bodySpriteRenderer;
        public SpriteRenderer iconSpriteRenderer;

        [Header("Indicators")] public GameObject speedIndicatorPrefab;

        public GameObject shieldIndicatorPrefab;
        public GameObject pointIndicatorPrefab;

        private GameObject indicatorInstance;

        private void OnEnable()
        {
            // Set colors based on type
            bodySpriteRenderer.color = skillPointType == SkillPointType.Point
                ? new Color(1, 1, 1, 1)
                : new Color(0, 0.3921569f, 0.3921569f, 1);

            iconSpriteRenderer.color = skillPointType == SkillPointType.Point
                ? new Color(0.6117647f, 0.427451f, 0.08627451f, 1)
                : new Color(1, 1, 1, 1);
        }

        private void OnDisable()
        {
            // Destroy indicator when this object is disabled
            if (indicatorInstance != null) Destroy(indicatorInstance);
        }
    }
}