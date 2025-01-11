using System;
using Interfaces.Skills;
using UnityEngine;

namespace Skills.model
{
    [Serializable]
    [CreateAssetMenu(fileName = "Coin Skill", menuName = "skills/Coin  Skill")]
    public class CoinSkill : SkillSo, ISkillBehavior
    {
        public int pointValue = 1;
        public int pointMultiplier = 10;

        public override void ApplyEffect(Plane plane)
        {
            plane.GetSkillPoint(pointValue);
        }
    }
}