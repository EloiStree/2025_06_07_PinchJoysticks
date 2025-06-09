
using System;
using UnityEngine;


namespace Eloi.PinchJoysticks
{

    [ExecuteInEditMode]
    public class ExPinchMono_LocalComputeForCartesianTwoPoints : MonoBehaviour
    {
        public STRUCT_CartesianTwoPointsWorldToLocal m_worldToLocal;


        public float m_joystickDistanceToPercent = 0.3f;
        public bool m_clampValue = true;

        public void PushIn(STRUCT_CartesianTwoPoints from , STRUCT_CartesianTwoPoints to)
        {
            m_worldToLocal.m_fromWorldSpace = from;
            m_worldToLocal.m_toWorldSpace = to;
            ComputeData();

        }



        public void Update()
        {

            ComputeData();

            DebugDrawUility.DrawLine(m_worldToLocal.m_fromLocalSpace.m_leftWorldPosition,
                m_worldToLocal.m_fromLocalSpace.m_rightWorldPosition,
                Color.yellow);
            DebugDrawUility.DrawLine(m_worldToLocal.m_toLocalSpace.m_leftWorldPosition,
                m_worldToLocal.m_toLocalSpace.m_rightWorldPosition,
                Color.yellow);
            DebugDrawUility.DrawLine(m_worldToLocal.m_fromLocalSpace.m_worldPosition,
                m_worldToLocal.m_toLocalSpace.m_worldPosition,
                Color.yellow);

            Vector3 toDirection = m_worldToLocal.m_toLocalSpace.m_worldRotation * Vector3.forward;
            DebugDrawUility.DrawLine(m_worldToLocal.m_toLocalSpace.m_worldPosition,
                m_worldToLocal.m_toLocalSpace.m_worldPosition + toDirection * 0.2f,
                Color.blue);

            Vector3 toDireciontOnXY = Quaternion.Euler(0f, 0f, m_flatRollAngleTo) * Vector3.right;
            float halfDistantOfTo = m_betweenTwoPointsToDistance * 0.5f;
            DebugDrawUility.DrawLine(Vector3.zero,
                toDireciontOnXY * halfDistantOfTo,
                Color.magenta);

        }

        private void ComputeData()
        {
            STRUCT_CartesianTwoPoints from = m_worldToLocal.m_fromWorldSpace;
            STRUCT_CartesianTwoPoints to = m_worldToLocal.m_toWorldSpace;

            Vector3 translateToZero = -from.m_worldPosition;
            Quaternion rotateToZero = Quaternion.Inverse(from.m_worldRotation);
            Quaternion localRotationOfTo = rotateToZero * to.m_worldRotation;

            m_worldToLocal.m_fromLocalSpace.m_worldRotation=Quaternion.identity;
            m_worldToLocal.m_toLocalSpace.m_worldRotation = localRotationOfTo;

            RelocateToZeroPoint(from.m_leftWorldPosition, translateToZero, rotateToZero,
                out m_worldToLocal.m_fromLocalSpace.m_leftWorldPosition);
            RelocateToZeroPoint(from.m_rightWorldPosition, translateToZero, rotateToZero,
                out m_worldToLocal.m_fromLocalSpace.m_rightWorldPosition);


            RelocateToZeroPoint(to.m_leftWorldPosition, translateToZero, rotateToZero,
                out m_worldToLocal.m_toLocalSpace.m_leftWorldPosition);
            RelocateToZeroPoint(to.m_rightWorldPosition, translateToZero, rotateToZero,
                out m_worldToLocal.m_toLocalSpace.m_rightWorldPosition);

            RelocateToZeroPoint(to.m_worldPosition, translateToZero, rotateToZero,
                out m_worldToLocal.m_toLocalSpace.m_worldPosition);


            // Compute the flat yaw 
            Vector3 toDirection = m_worldToLocal.m_toLocalSpace.m_worldRotation * Vector3.forward;
            toDirection.y = 0f; // Flatten the direction to the XZ plane
            m_flatToForwardYawDirectionAngle = Vector3.SignedAngle(Vector3.forward, toDirection, Vector3.up);
            m_middleLocalDirection = m_worldToLocal.m_toLocalSpace.m_worldPosition;
            m_middleDistanceFromTo = Vector3.Distance(
                m_worldToLocal.m_fromLocalSpace.m_worldPosition,
                m_worldToLocal.m_toLocalSpace.m_worldPosition
            );

            m_betweenTwoPointsFromDistance = Vector3.Distance(
                m_worldToLocal.m_fromLocalSpace.m_leftWorldPosition,
                m_worldToLocal.m_fromLocalSpace.m_rightWorldPosition
            );
            m_betweenTwoPointsToDistance = Vector3.Distance(
                m_worldToLocal.m_toLocalSpace.m_leftWorldPosition,
                m_worldToLocal.m_toLocalSpace.m_rightWorldPosition
            );
            m_betweenTwoPointsDeltaDistance = m_betweenTwoPointsToDistance - m_betweenTwoPointsFromDistance;

            if (m_betweenTwoPointsFromDistance == 0)
            { 
                m_betweenTwoPointsPercentRatio = 0f;
            }
            else
            {
                m_betweenTwoPointsPercentRatio = m_betweenTwoPointsToDistance / m_betweenTwoPointsFromDistance;
                
            }

            m_flatRollAngleFrom = Vector3.SignedAngle(
               Vector3.right, m_worldToLocal.m_fromLocalSpace.m_rightWorldPosition
                , Vector3.forward);

            PinchJoystickUtility.GetLocalRollOf(
                m_worldToLocal.m_toLocalSpace.m_leftWorldPosition,
                m_worldToLocal.m_toLocalSpace.m_rightWorldPosition,
                out m_flatRollAngleTo);
            m_flatRollAngleDetla = m_flatRollAngleTo - m_flatRollAngleFrom;

            PinchJoystickUtility.GetJoystickPercentFromLocal(
                m_middleLocalDirection,
                m_joystickDistanceToPercent,
                m_clampValue,
                out m_percentJoystickDirection
                );

            m_maxFlatRollToPercent11 = Mathf.Clamp(m_flatRollAngleTo / 180f, -1f, 1f);
            m_yawPercent11 = Mathf.Clamp(m_flatToForwardYawDirectionAngle / m_maxYawAngle, -1f, 1f);
        }

        [Tooltip("The yaw local direcion of the forward in the middle of the to point flat on XZ plan")]
        public float m_flatToForwardYawDirectionAngle = 0f;
        public float m_maxYawAngle = 180;
        public float m_yawPercent11 = 0f; // -1 to 1, -1 is left, 1 is right, 0 is center
        public Vector3 m_middleLocalDirection = Vector3.zero;
        public Vector3 m_percentJoystickDirection;
        public float m_middleDistanceFromTo;

        public float m_betweenTwoPointsFromDistance;
        public float m_betweenTwoPointsToDistance;
        public float m_betweenTwoPointsDeltaDistance;
        public float m_betweenTwoPointsPercentRatio;

        public float m_flatRollAngleFrom;
        public float m_flatRollAngleTo;
        public float m_flatRollAngleDetla;
        public float m_maxflatRollToAngle = 85f;
        public float m_maxFlatRollToPercent11 = 0;


        public void GetLocalRollTo(out float roll)
        {
            roll = m_flatRollAngleTo;
        }
        public void GetLocalYawTo(out float yaw)
        {
            yaw = m_flatToForwardYawDirectionAngle;
        }
        public void GetBothLocalRoll(out float rollFrom, out float rollTo)
        {
            rollFrom = m_flatRollAngleFrom;
            rollTo = m_flatRollAngleTo;
        }
        public void GetRollDelta(out float rollDelta)
        {
            rollDelta = m_flatRollAngleDetla;
        }
        public void GetInspectorJoystickFromMiddlePoint(out Vector3 joystick3D)
        {

            PinchJoystickUtility.GetJoystickPercentFromLocal(
              m_middleLocalDirection,
              m_joystickDistanceToPercent,
              m_clampValue,
              out joystick3D
              );
        }

        public void GetJoystickFromMiddlePoint(float ratioDistanceToPercent, bool clamp,out Vector3 joystick3D) {

            PinchJoystickUtility.GetJoystickPercentFromLocal(
              m_middleLocalDirection,
              m_joystickDistanceToPercent,
              m_clampValue,
              out joystick3D
              );
        }

        public void RelocateToZeroPoint(
            Vector3 worldPoints,
            Vector3 translationToZero, 
            Quaternion rotationToZero, 
            out Vector3 localPoints)
        {
            localPoints = rotationToZero * (worldPoints + translationToZero);
        }



    }
}