using UnityEngine;

namespace Common
{
    public class CameraSizeCorrection : MonoBehaviour
    {
        [SerializeField] private float defaultFOV;
        [SerializeField] private float viewportRectW;

        private void Start()
        {
            float count = Mathf.Max(viewportRectW / Camera.main.aspect, 1f);
            float halfDefaultRad = defaultFOV * Mathf.Deg2Rad * 0.5f;
            float halfNewRad = Mathf.Atan(Mathf.Tan(halfDefaultRad) * count);
            Camera.main.fieldOfView = halfNewRad * 2f * Mathf.Rad2Deg;
            Application.targetFrameRate = int.MaxValue;
        }
        
    }
}