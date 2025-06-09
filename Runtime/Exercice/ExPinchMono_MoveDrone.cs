using UnityEngine;


namespace Eloi.PinchJoysticks
{
    public class  ExPinchMono_MoveDrone : MonoBehaviour
    {

        public Transform m_droneToMove;
      

        public float m_rotateLeftToRightPercent11 = 0f;
        public Vector3 m_moveDirectionPercent11 = Vector3.zero;

        public float m_rotateLeftRightSpeedPerSecond = 180;

        public float m_downSpeedPerSecond = 1f;
        public float m_upSpeedPerSecond = 0.3f;
        public float m_backForwardSpeedPerSecond = 1f;
        public float m_leftRightSpeedPerSecond = 1f;
        public float m_globalMultiplicatorSpeedPercent = 1f;



        public void SetRotateLeftToRightPercent11(float percent11)
        {
            m_rotateLeftToRightPercent11 = percent11;
        }

        public void SetMoveDirectionPercent11(Vector3 percent11)
        {
            m_moveDirectionPercent11 = percent11;
        }

        void Update()
        {
            float deltaTime = Time.deltaTime;   
            Quaternion droneRotation = m_droneToMove.rotation;
            float angleToRotate = 
                m_rotateLeftToRightPercent11 
                * m_rotateLeftRightSpeedPerSecond
                * deltaTime ;
            Vector3 eulerRotation = new Vector3(0, angleToRotate, 0);

            // a quaternion is not commutative, so the order of multiplication matters
            // If you want rotation on y of UNTIY in world space
            Quaternion newRotation = Quaternion.Euler(eulerRotation) * droneRotation;

            // If you want rotation on y of the object himself
            //Quaternion newRotation = droneRotation * Quaternion.Euler(eulerRotation);
            m_droneToMove.rotation = newRotation;


            Vector3 moveThisFrame = Vector3.zero;
            if (m_moveDirectionPercent11.y>0)
                moveThisFrame.y = m_moveDirectionPercent11.y * m_upSpeedPerSecond * deltaTime;
            else if (m_moveDirectionPercent11.y < 0)
                moveThisFrame.y = m_moveDirectionPercent11.y * m_downSpeedPerSecond * deltaTime;
            moveThisFrame.x = m_moveDirectionPercent11.x * m_leftRightSpeedPerSecond * deltaTime;
            moveThisFrame.z = m_moveDirectionPercent11.z * m_backForwardSpeedPerSecond * deltaTime;
            //moveThisFrame *= m_globalMultiplicatorSpeedPercent;

            Vector3 newPosition = m_droneToMove.position + (m_droneToMove.rotation * moveThisFrame);
            m_droneToMove.position = newPosition;


            // JOB syste et compute shadre n ont pas ces methodes
            //m_droneToMove.Translate(direction, Space.Self);
            //m_droneToMove.Rotate(0, rotation, 0, Space.World);


        }


    }
}