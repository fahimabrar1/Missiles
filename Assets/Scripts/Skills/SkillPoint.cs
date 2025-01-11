using Interfaces;
using Interfaces.Skills;
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
        private ISkillBehavior _skillBehavior;

        private IIndicator indicatorInstance;

        private void OnDisable()
        {
            // Destroy indicator when this object is disabled
            if (indicatorInstance != null) indicatorInstance.OnDestroyMissile();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                MyDebug.Log("SkillPoint OnTriggerEnter2D");
                ApplySkill(other.GetComponentInParent<Plane>());
                Destroy(gameObject);
            }
        }

        // private void SetSillGUI()
        // {
        //     // Set colors based on type
        //     bodySpriteRenderer.color = skillPointType == SkillPointType.Point
        //         ? new Color(1, 1, 1, 1)
        //         : new Color(0, 0.3921569f, 0.3921569f, 1);
        //
        //     iconSpriteRenderer.color = skillPointType == SkillPointType.Point
        //         ? new Color(0.6117647f, 0.427451f, 0.08627451f, 1)
        //         : new Color(1, 1, 1, 1);
        // }

        public void Initialize(IIndicator indicator, ISkillBehavior skillBehavior)
        {
            indicatorInstance = indicator;
            _skillBehavior = skillBehavior;
            // SetSillGUI();
        }


        public void ApplySkill(Plane plane)
        {
            _skillBehavior.ApplyEffect(plane);
        }
    }
}