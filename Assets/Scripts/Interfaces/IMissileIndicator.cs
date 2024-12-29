using UnityEngine;

namespace Interfaces
{
    public interface IMissileIndicator
    {
        void Initialize(Transform missile, Camera mainCamera);
        void OnDestroyMissile();
    }

}