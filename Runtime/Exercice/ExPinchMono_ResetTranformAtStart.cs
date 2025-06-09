using UnityEngine;


namespace Eloi.PinchJoysticks
{
    public class ExPinchMono_ResetTranformAtStart : MonoBehaviour
    {
        public Transform m_startPoint;
        public Transform m_transformToMove;


        [ContextMenu("Reset At Start Point")]
        public void ResetAtStartPoint()
        {
            if (m_startPoint == null || m_transformToMove == null)
            {
                return;
            }
            m_transformToMove.position = m_startPoint.position;
            m_transformToMove.rotation = m_startPoint.rotation;
        }

    }
}