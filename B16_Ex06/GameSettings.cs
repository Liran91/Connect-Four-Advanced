using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex06
{
    public struct GameSettings
    {
        private string m_Player1Name;
        private string m_Player2Name;
        private int m_Rows;
        private int m_Cols;

        public int Rows
        {
            get
            {
                return m_Rows;
            }

            set
            {
                m_Rows = value;
            }
        }

        public int Cols
        {
            get
            {
                return m_Cols;
            }

            set
            {
                m_Cols = value;
            }
        }

        public string Player1Name
        {
            get
            {
                return m_Player1Name;
            }

            set
            {
                m_Player1Name = value;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_Player2Name;
            }

            set
            {
                m_Player2Name = value;
            }
        }
    }
}
