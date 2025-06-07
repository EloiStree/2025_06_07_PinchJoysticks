using UnityEngine;

namespace Eloi.PinchJoysticks
{

    [System.Serializable]
    public struct STRUCT_PinchJoystickAsRawData : I_PinchJoystickAsRawData
    {
        [Header("World")]
        public Vector3 m_worldStartPoint;
        public Vector3 m_worldDirectionOfMiddlePoint;

        [Header("Local")]
        public Vector3 m_localDirectionOfMiddlePoint;
        public float m_angleFrontYaw;
        public float m_angleHorizontalPitch;
        public float m_angleHorizontalRoll;
        public void GetLocalDirectionOfMiddlePoint(out Vector3 localDirectionOfMiddlePoint) { localDirectionOfMiddlePoint = this.m_localDirectionOfMiddlePoint; }
        public void GetWorldDirectionOfMiddlePoint(out Vector3 worldDirectionOfMiddlePoint) { worldDirectionOfMiddlePoint = this.m_worldDirectionOfMiddlePoint; }
        public void GetWorldStartPoint(out Vector3 worldStartPoint) { worldStartPoint = this.m_worldStartPoint; }
        public void GetAngleFrontYaw(out float angleFrontYaw) { angleFrontYaw = this.m_angleFrontYaw; }
        public void GetAngleHorizontalPitch(out float angleHorizontalPitch) { angleHorizontalPitch = this.m_angleHorizontalPitch; }
        public void GetAngleHorizontalRoll(out float angleHorizontalRoll) { angleHorizontalRoll = this.m_angleHorizontalRoll; }
    }

}