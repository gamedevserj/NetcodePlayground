using UnityEngine;

namespace NetcodePlayground
{
    public class CameraController : MonoBehaviour
    {

        private Transform _transform;
        public Transform TargetTransform { get; set; }
        private Transform Transform
        {
            get
            {
                if (_transform == null)
                    _transform = transform;
                return _transform;
            }
        }

        void FixedUpdate()
        {
            if (TargetTransform == null)
                return;

            Transform.SetPositionAndRotation(TargetTransform.position, TargetTransform.rotation);
        }
    }
}
