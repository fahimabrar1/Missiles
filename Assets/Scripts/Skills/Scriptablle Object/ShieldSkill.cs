using System;
using Interfaces.Skills;
using UnityEngine;

namespace Skills.model
{
    [Serializable]
    [CreateAssetMenu(fileName = "Shield Skill", menuName = "skills/Shield Skill")]
    public class ShieldSkill : SkillSo, ISkillBehavior
    {
        public override void ApplyEffect(Plane plane)
        {
            plane.ToggleeShield(true);
        }
    }
}