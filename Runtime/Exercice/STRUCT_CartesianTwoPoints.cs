using UnityEngine;


namespace Eloi.PinchJoysticks
{
    [System.Serializable]
    public struct STRUCT_CartesianTwoPoints {

        public Vector3 m_worldPosition;
        public Quaternion m_worldRotation;
        public Vector3 m_leftWorldPosition;
        public Vector3 m_rightWorldPosition;
    }
}