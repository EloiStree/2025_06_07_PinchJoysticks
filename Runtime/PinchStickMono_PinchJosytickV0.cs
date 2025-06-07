using UnityEngine;
using UnityEngine.UIElements;

namespace Eloi.PinchJoysticks
{

    [ExecuteInEditMode]
    public class PinchStickMono_PinchJosytickV0:MonoBehaviour {
        
        public PinchStickMono_AbstractSource m_source;
        public STRUCT_PinchJoystickAsRawData m_rawData;
        public float m_maxValueToYawPercent = 110f;
        public float m_maxValueToPitchPercent = 30f;
        public float m_maxValueToRollPercent = 30f;
        public STRUCT_PinchJoystickAsPercentData m_percentData;


        [Header("For Debug")]
        public float m_playerHeadPitchAngle = 0f;



        void Update()
        {
            if (m_source == null) return;
            ComputeSourceToJosytickValue();
        }
        public void ComputeSourceToJosytickValue() {

            if (m_source == null) return;
            m_source.GetHeadCenterInfo(out Vector3 headCenter, out Quaternion headRotation);
            // To move to a zero point we need to remove the vector of the head to the hands
            Vector3 headTranslationToZero = -headCenter;
            // Then we will need to rotate in the opposed rotation of the current head
            Quaternion headRotationToZero = Quaternion.Inverse(headRotation);

            // Lets get the info of the hand
            m_source.GetLeftAnchorPoint(out Vector3 leftAnchorPoint, out Quaternion leftAnchorRotation);
            m_source.GetRightAnchorPoint(out Vector3 rightAnchorPoint, out Quaternion rightAnchorRotation);
            //First we need to bring the hand to a local position
            Vector3 localLeftAnchor = leftAnchorPoint - headCenter;
            Vector3 localRightAnchor = rightAnchorPoint - headCenter;

            // Then we need to rotate the to the inverse of the head rotation
            // but we have a problem this is not the head rotation we need but the axis Y at the head rotation
            // To have the direction of the head you can use
            // Vector3(directionofrotation) = Quaternion(currentrotation) * Vector3.forward;
            Vector3 directionOfPlayerHead = headRotation * Vector3.forward;
            // Now we need to flat the data on the xz plane
            Vector3 flatDirectionOfLayerHead = new Vector3(directionOfPlayerHead.x, 0f, directionOfPlayerHead.z);

            // For debug purpose we can draw player head and store the info
            // We need to find the right axis of the direction of the head and the flat forward line we found
            Vector3 rightAxisOfFlatAndDirection = Vector3.Cross(flatDirectionOfLayerHead, Vector3.up);
            // Then ask for the angle between the head angle and the flat surface compare to the right axis of them
            float pitchHeadLocal = Vector3.SignedAngle(flatDirectionOfLayerHead,directionOfPlayerHead , rightAxisOfFlatAndDirection);
            m_playerHeadPitchAngle = pitchHeadLocal;

            // Lets draw that for the debug
            bool debugLocalHeadDirectionAndFlat = false;
            if (debugLocalHeadDirectionAndFlat) { 
                DebugDrawUility.DrawLine(directionOfPlayerHead, Color.yellow * 0.3f);
                DebugDrawUility.DrawLine(flatDirectionOfLayerHead, Color.yellow * 0.9f);
            }
            Vector3 localheadDirection  = Quaternion.Euler(-m_playerHeadPitchAngle, 0f, 0f) * Vector3.forward;
            DebugDrawUility.DrawLine(localheadDirection, Color.yellow );

            // Now we can rotate the local anchor to the inverse of the head rotation y
            // Look rotation give us the angle from forward in unity to the flat direction of the head we computed
            // But we dont want to go there but in the opposite direction to rotate around Y axis at zero
            // A quaternion multiply by it direction give a the new direction of the point on local space
            localLeftAnchor = Quaternion.Inverse(Quaternion.LookRotation(flatDirectionOfLayerHead)) * localLeftAnchor;
            localRightAnchor = Quaternion.Inverse(Quaternion.LookRotation(flatDirectionOfLayerHead)) * localRightAnchor;

            // Lets check that with some draw
            DebugDrawUility.DrawLine(localLeftAnchor, Color.red* 0.9f);
            DebugDrawUility.DrawLine(localRightAnchor, Color.red);

            // Ok the hand are now in a local space, lets get the middle point
            // To do that you add the two vectors then divide the result by two
            Vector3 localMiddlePoint = (localLeftAnchor + localRightAnchor) /2f;
            // To be able to show him we need the forward direction of it from left to right
            // Lets use the cross product using the Unity local up as start point
            Vector3 unityUp = Vector3.up;
            // We can have the direction of an object by doing destination less origin
            Vector3 leftToRigthHandsDirection = localRightAnchor - localLeftAnchor;
            // Now that we have two direciton we can use cross product
            Vector3 handMidForward = Vector3.Cross(leftToRigthHandsDirection, unityUp);
          
            // Tthe UnityForward is the forward flat to Y
            Vector3 handMidUnityForward = new Vector3(handMidForward.x, 0f, handMidForward.z);

            // We can now comput the up in world space
            Vector3 handMidUp = Vector3.Cross(handMidForward, leftToRigthHandsDirection);
            Vector3 handMidUnityUp = unityUp;

            // As we have the forward and the up can we compute the right direction
            Vector3 handMidRight = Vector3.Cross(handMidForward, handMidUp);
            Vector3 handMidUnityRight = Vector3.Cross(unityUp, handMidForward);

            // Lets normalize the direction to have a good length
            handMidForward.Normalize();
            handMidUp.Normalize();
            handMidRight.Normalize();
            handMidUnityForward.Normalize();
            handMidUnityUp.Normalize();
            handMidUnityRight.Normalize();


            // Lets check that we succeed
            float handMiddleCartesianDistance =0.1f;
            DebugDrawUility.DrawLine(localMiddlePoint, localMiddlePoint + handMidForward * handMiddleCartesianDistance, Color.blue);
            DebugDrawUility.DrawLine(localMiddlePoint, localMiddlePoint + handMidRight * handMiddleCartesianDistance, Color.red);
            DebugDrawUility.DrawLine(localMiddlePoint, localMiddlePoint + handMidUp * handMiddleCartesianDistance, Color.green);

            DebugDrawUility.DrawLine(localMiddlePoint, localMiddlePoint + handMidUnityForward * handMiddleCartesianDistance, Color.blue*0.8f);
            DebugDrawUility.DrawLine(localMiddlePoint, localMiddlePoint + handMidUnityRight * handMiddleCartesianDistance, Color.red * 0.8f);
            DebugDrawUility.DrawLine(localMiddlePoint, localMiddlePoint + handMidUnityUp * handMiddleCartesianDistance, Color.green * 0.8f);

            DebugDrawUility.DrawLine(localLeftAnchor , localRightAnchor, Color.yellow);

            // To compute the yaw  on the Y axis
            // Then the roll on the forward direction of the player
            // Then the pitch on the right direction of the player
            // We need to flat those direction

            Vector3 flatHandMidForward = new Vector3(handMidForward.x, 0f, handMidForward.z);
            Vector3 flatHandRollXY = new Vector3(handMidRight.x, handMidRight.y, 0f);
            Vector3 flatHandPitchZY = new Vector3(0f, handMidUp.y, handMidUp.z); // Hum 

            // Lets look at the result
            DebugDrawUility.DrawLine(flatHandMidForward, Color.green);
            DebugDrawUility.DrawLine(flatHandRollXY, Color.blue );
            DebugDrawUility.DrawLine(flatHandPitchZY, Color.red );


            float angleFrontYaw = Vector3.SignedAngle(Vector3.forward, flatHandMidForward, Vector3.up);
            m_rawData.m_angleFrontYaw = angleFrontYaw;
            float angleRoll = Vector3.SignedAngle(Vector3.up, flatHandRollXY, Vector3.up);

            // To comput the roll angle we need to flat the forward direction to the front of the player direction
            //Vector3 flat 








        }
    }

}