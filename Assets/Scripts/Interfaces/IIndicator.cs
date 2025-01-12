using UnityEngine;

namespace Interfaces
{
    public interface IIndicator
    {
        void Initialize(Transform missile, Camera mainCamera);
        void OnDestroyIndicatorTarget();
    }
}