using UnityEngine;


namespace Eloi.PinchJoysticks
{
    public class ExPinchMono_UpdateMiddleOfTwoPoints : MonoBehaviour
    {
        public Transform m_pointLeft;
        public Transform m_pointRight;
        public Transform m_pointToMoveAtMiddle;
        public Vector3 m_middlePoint;
        public void Update()
        {
            if (m_pointLeft == null || m_pointRight == null)
            {
                return;
            }
            m_middlePoint = (m_pointLeft.position + m_pointRight.position) / 2f;
            Vector3 direction = m_pointRight.position - m_pointLeft.position;
            Vector3 forward = Vector3.Cross(Vector3.up, direction).normalized;
            Vector3 right = Vector3.Cross(forward, Vector3.up).normalized;
            Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

            m_pointToMoveAtMiddle.position = m_middlePoint;
            m_pointToMoveAtMiddle.rotation = rotation;
        }
    }
}