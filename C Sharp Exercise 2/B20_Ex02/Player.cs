﻿namespace B20_Ex02
{
    public class Player
    {
        // MEMBER VARIABLES
        private readonly bool r_IsHuman;
        private readonly string r_PlayerName;
        private int m_Score;
        private PlayerStep m_FirstStep;
        private PlayerStep m_SecondStep;

        // CTOR
        public Player(string i_PlayerName, bool i_IsHuman)
        {
            this.r_PlayerName = i_PlayerName;
            this.r_IsHuman = i_IsHuman;
            this.m_Score = 0;
        }

        // PROPERTIES
        public int Score
        {
            get { return this.m_Score; }
            set { this.m_Score = value; }
        }

        public PlayerStep FirstStep
        {
            get { return this.m_FirstStep; }
            set { this.m_FirstStep = value; }
        }

        public PlayerStep SecondStep
        {
            get { return this.m_SecondStep; }
            set { this.m_SecondStep = value; }
        }

        public string PlayerName
        {
            get { return this.r_PlayerName; }
        }

        public bool IsHuman
        {
            get { return this.r_IsHuman; }
        }
    }
}