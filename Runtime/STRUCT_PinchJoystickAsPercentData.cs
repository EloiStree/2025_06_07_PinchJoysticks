namespace Eloi.PinchJoysticks
{
    [System.Serializable]
    public struct STRUCT_PinchJoystickAsPercentData : I_PinchJoystickAsPercentData
    {
        public float m_leftToRightPercent11;
        public float m_downToUpPercent11;
        public float m_backToFrontPercent11;
        public float m_rotatingLeftToRightPercent11;
        public float m_pitchingBackToFrontPercent11;
        public float m_rollingLeftToRightPercent11;
        public void GetPercentLeftToRight(out float leftToRightPercent11) { leftToRightPercent11 = this.m_leftToRightPercent11; }
        public void GetPercentDownToUp(out float downToUpPercent11) { downToUpPercent11 = this.m_downToUpPercent11; }
        public void GetPercentBackToFront(out float backToFrontPercent11) { backToFrontPercent11 = this.m_backToFrontPercent11; }
        public void GetPercentYawLeftToRight(out float rotatingLeftToRightPercent11) { rotatingLeftToRightPercent11 = this.m_rotatingLeftToRightPercent11; }
        public void GetPercentHorizontalPitchBackToFront(out float pitchingBackToFrontPercent11) { pitchingBackToFrontPercent11 = this.m_pitchingBackToFrontPercent11; }
        public void GetPercentHorizontalRollingLeftToRight(out float rollingLeftToRightPercent11) { rollingLeftToRightPercent11 = this.m_rollingLeftToRightPercent11; }
    }

}