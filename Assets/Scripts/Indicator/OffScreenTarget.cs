using System.Collections.Generic;
using UnityEngine;

namespace Indicator
{
  public class OffScreenTarget : MonoBehaviour
{
    public static OffScreenTarget Instance { get; private set; }
    private Camera _mainCamera;
    private List<MissileIndicatorData> _missileDataList = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        for (int i = 0; i < _missileDataList.Count; i++)
        {
            MissileIndicatorData data = _missileDataList[i];
            if (data.Missile is null)
            {
                Destroy(data.Indicator.gameObject);
                _missileDataList.RemoveAt(i);
                i--;
                continue;
            }

            Vector3 screenPoint = _mainCamera.WorldToScreenPoint(data.Missile.position);
            bool isOnScreen = screenPoint.z > 0 &&
                              screenPoint.x > 0 && screenPoint.x < Screen.width &&
                              screenPoint.y > 0 && screenPoint.y < Screen.height;

            data.Indicator.gameObject.SetActive(!isOnScreen);

            if (!isOnScreen)
            {
                UpdateIndicatorPosition(data.Indicator, screenPoint);
            }
        }
    }

    private void UpdateIndicatorPosition(RectTransform indicator, Vector3 screenPoint)
    {
        screenPoint.x = Mathf.Clamp(screenPoint.x, 0, Screen.width);
        screenPoint.y = Mathf.Clamp(screenPoint.y, 0, Screen.height);
        indicator.position = screenPoint;

        Vector3 direction = screenPoint - new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        indicator.rotation = Quaternion.Euler(0, 0, angle - 90); // Adjust for indicator orientation
    }

    public void RegisterMissile(Transform missile, RectTransform indicator)
    {
        _missileDataList.Add(new MissileIndicatorData { Missile = missile, Indicator = indicator });
    }

    public void UnregisterMissile(Transform missile)
    {
        MissileIndicatorData data = _missileDataList.Find(m => m.Missile == missile);
        if (data != null)
        {
            Destroy(data.Indicator.gameObject);
            _missileDataList.Remove(data);
        }
    }

    private class MissileIndicatorData
    {
        public Transform Missile;
        public RectTransform Indicator;
    }
}
}