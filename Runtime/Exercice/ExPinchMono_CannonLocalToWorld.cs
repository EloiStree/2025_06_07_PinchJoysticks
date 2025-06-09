using UnityEngine;


namespace Eloi.PinchJoysticks
{

    [ExecuteInEditMode]
    public class ExPinchMono_CannonLocalToWorld : MonoBehaviour
    { 
        public float m_horizontalAngle = 0f;
        public float m_verticalAngle = 0f;

        public float m_maxRotationHorizontalAngle = 180f;
        public float m_maxRotationVerticalUpAngle = 90;

        public Transform m_whereToSetTheCannon;
        public Transform m_cannonToMove;


        public void SetHorizontalAngle(float angle)
        {
            m_horizontalAngle = angle;
            ClampValue();
        }
        public void SetVerticalAngle(float angle)
        {
            m_verticalAngle = angle;
            ClampValue();
        }

        public void SetPercentHorizonal(float percent11)
        {
            m_horizontalAngle = percent11 * m_maxRotationHorizontalAngle;
            ClampValue();
        }
        public void SetPercentVertical(float percent01)
        {
            m_verticalAngle = percent01 * m_maxRotationVerticalUpAngle;
            ClampValue();
        }

        public void OnValidate()
        {
            ClampValue();
        }

        private void ClampValue()
        {
            m_horizontalAngle = Mathf.Clamp(m_horizontalAngle, -m_maxRotationHorizontalAngle, m_maxRotationHorizontalAngle);
            m_verticalAngle = Mathf.Clamp(m_verticalAngle, 0, m_maxRotationVerticalUpAngle);


        }

        private void Update()
        {
            ClampValue();
            Quaternion horizontalRotation = Quaternion.Euler(0, m_horizontalAngle, 0);
            Quaternion verticalRotation = Quaternion.Euler(-m_verticalAngle, 0, 0);
            Quaternion cannonRotation = horizontalRotation * verticalRotation;
            Vector3 localCannonDirection = cannonRotation * Vector3.forward;

            DebugDrawUility.DrawLine(localCannonDirection, Color.red);

            GetLocalToWorld_Point(
                
                localCannonDirection
                , m_whereToSetTheCannon.position
                , m_whereToSetTheCannon.rotation,out Vector3 cannonTipInWorldPosition
                );

            DebugDrawUility.DrawLine(m_whereToSetTheCannon.position,cannonTipInWorldPosition, Color.red);

            m_cannonToMove.position = m_whereToSetTheCannon.position;
            Quaternion tankRotation = m_whereToSetTheCannon.rotation;
            Quaternion tankRotationWithLocal = tankRotation * cannonRotation;
            m_cannonToMove.rotation = tankRotationWithLocal;

            //Quaternion droneRotation = m_whereToSetTheCannon.rotation;
            //// Translate then rotate
            //Vector3 newPosition = m_whereToSetTheCannon.position


        }

        // V new = Q rotation * V localPosition
        public static void GetLocalToWorld_Point(
            in Vector3 localPosition,
            in Vector3 positionReference,
            in Quaternion rotationReference,
            out Vector3 worldPosition) =>
            worldPosition = (rotationReference * localPosition) + (positionReference);

    }
}