using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


namespace Eloi.PinchJoysticks
{
    public static class PinchJoystickUtility {

        public static void GetCartesianOf(Transform pointLeft, Transform pointRight, out STRUCT_CartesianTwoPoints state)
        {
            state.m_leftWorldPosition = pointLeft.position;
            state.m_rightWorldPosition = pointRight.position;
            Vector3 direction = pointRight.position - pointLeft.position;
            Vector3 up = Vector3.up;
            Vector3 forward = Vector3.Cross(direction, up);
            Vector3 right = Vector3.Cross(up, forward);


            Vector3 middlePoint = (pointLeft.position + pointRight.position) / 2f;
            //DebugDrawUility.DrawLine(middlePoint, middlePoint + up, Color.green);
            //DebugDrawUility.DrawLine(middlePoint, middlePoint + forward, Color.blue);
            //DebugDrawUility.DrawLine(middlePoint, middlePoint + right, Color.red);

            Quaternion rotation = Quaternion.identity;
            bool useLookAt = true;
            if (useLookAt)
            {
                rotation = Quaternion.LookRotation(forward, up);
            }
            else
            {
                rotation = GetQuaternionFromCartesianAxis(right, up, forward);
            }
            state.m_worldPosition = middlePoint;
            state.m_worldRotation = rotation;
        }
        public static Quaternion GetQuaternionFromCartesianAxis(Vector3 rightDirection, Vector3 upDirection, Vector3 forwardDirection)
        {
            rightDirection.Normalize();
            upDirection.Normalize();
            forwardDirection.Normalize();
            Matrix4x4 rotationMatrix = new Matrix4x4();
            rotationMatrix.SetColumn(0, rightDirection);
            rotationMatrix.SetColumn(1, upDirection);
            rotationMatrix.SetColumn(2, forwardDirection);
            Quaternion rotationQuaternion = rotationMatrix.rotation;
            return rotationQuaternion;
        }

        public static void GetLocalRollOf(Vector3 leftWorldPosition, Vector3 rightWorldPosition, out float roll)
        {
            Vector3 direction = rightWorldPosition - leftWorldPosition;
            Vector3 up = Vector3.up;
            Vector3 forward = Vector3.Cross(direction, up);
            Vector3 right = Vector3.Cross(up, forward);

            roll = Vector3.SignedAngle(right,direction, forward);
        }
    }
}