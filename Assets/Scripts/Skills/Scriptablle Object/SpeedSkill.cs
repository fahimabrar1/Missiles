using System;
using UnityEngine;
using ISkillBehavior = Interfaces.Skills.ISkillBehavior;

namespace Skills.model
{
    [Serializable]
    [CreateAssetMenu(fileName = "Speed Skill", menuName = "skills/Speed Skill")]
    public class SpeedSkill : SkillSo, ISkillBehavior
    {
        public int speed = 220;
        public int speedDuration = 10;

        public override void ApplyEffect(Plane plane)
        {
            plane.SetSpeed(speed, speedDuration);
        }
    }
}