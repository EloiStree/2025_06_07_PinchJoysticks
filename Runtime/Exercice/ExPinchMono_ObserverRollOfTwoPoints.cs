using System;
using UnityEngine;
using UnityEngine.Events;
namespace Eloi.PinchJoysticks
{


    [ExecuteInEditMode]
    public partial class ExPinchMono_ObserverRollOfTwoPoints : MonoBehaviour
    {

        public Transform m_leftPoint;
        public Transform m_rightPoint;

        public float m_rollAngle;
        public UnityEvent<float> m_onRollAngleChanged;
        public bool m_onlyOnChanged = true;


        private void Awake()
        {
            ComputeTheRoll();
        }

        void Update()
        {
            ComputeTheRoll();
        }

        private void ComputeTheRoll()
        {
            // Direction is destination less origin
            Vector3 destination = m_rightPoint.position;
            Vector3 origin = m_leftPoint.position;
            Vector3 direction = destination - origin;

            // Cross product allows to find the axis between two vectors
            Vector3 up = Vector3.up;
            Vector3 forward = Vector3.Cross(direction, up);
            Vector3 right = Vector3.Cross(up, forward);

            // To have the middle point we can add the two vectors and divide by two
            Vector3 middlePoint = (origin + destination) / 2f;


            up.Normalize();
            forward.Normalize();
            right.Normalize();

            float drawDistance = .05f;
            DebugDrawUility.DrawLine(middlePoint, middlePoint + forward * drawDistance, Color.blue);
            DebugDrawUility.DrawLine(middlePoint, middlePoint + up * drawDistance, Color.green);
            DebugDrawUility.DrawLine(middlePoint, middlePoint + right * drawDistance, Color.red);
            DebugDrawUility.DrawLine(middlePoint, middlePoint + direction.normalized * drawDistance, Color.red);

            float rollAngle = Vector3.SignedAngle(right, direction, forward);

            if (m_onlyOnChanged && rollAngle != m_rollAngle)
            {
                m_rollAngle = rollAngle;
                m_onRollAngleChanged?.Invoke(m_rollAngle);
                return;
            }
            m_rollAngle = rollAngle;
            m_onRollAngleChanged?.Invoke(m_rollAngle);

        }
    }
}