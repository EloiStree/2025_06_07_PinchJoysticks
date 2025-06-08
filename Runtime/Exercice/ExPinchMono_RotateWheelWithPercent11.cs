using UnityEngine;


namespace Eloi.PinchJoysticks
{
    public class ExPinchMono_RotateWheelWithPercent11 : MonoBehaviour
    {
        public Transform m_startDirection;
        public Transform m_wheelToMoveAndRotate;
        public float m_wheelRotationAngle = 180;
        public float m_currentPercent11 = 0f;
        public bool m_inverseRotation = false;

        public bool m_useUpdate = true;


        private void RefreshPosition()
        {
            Quaternion startDirectionRotation = m_startDirection.rotation;
            Quaternion rotationToApply = Quaternion.Euler(0, 0, m_currentPercent11 * -m_wheelRotationAngle * (m_inverseRotation ? -1:1));

            Quaternion targetRotation = startDirectionRotation * rotationToApply;

            m_wheelToMoveAndRotate.rotation = targetRotation;
            m_wheelToMoveAndRotate.position = m_startDirection.position;

        }
        public void SetPercent11(float percent11)
        {
            m_currentPercent11 = percent11;
            RefreshPosition();
        }
        public void Update()
        {
            if (m_useUpdate)
            {
                RefreshPosition();
            }
        
        }

    }
}