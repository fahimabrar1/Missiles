using Interfaces.Skills;
using UnityEngine;

namespace Skills.model
{
    public class SkillSo : ScriptableObject, ISkillBehavior
    {
        public SkillModel skillModel;

        public virtual void ApplyEffect(Plane target)
        {
        }
    }
}