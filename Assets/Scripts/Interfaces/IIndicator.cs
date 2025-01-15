using DefaultNamespace;
using UnityEngine;

namespace Interfaces
{
    public interface IIndicator
    {
        void Initialize(Transform missile, IndicatorManager indicatorManager, Camera mainCamera);
        void OnDestroyIndicatorTarget();
    }
}