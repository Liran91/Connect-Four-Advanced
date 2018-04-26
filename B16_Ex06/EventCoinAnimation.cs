using System;
using System.Drawing;

namespace B16_Ex06
{
    public class EventCoinAnimation : EventArgs
    {
        private readonly Point r_StartLoaction;
        private readonly int r_LowestThresehold;
        private readonly EventOnCoin r_CoinEvent;
        private readonly FormConnectFourGameBoard.GameState r_State;
        private EventHandler m_EventHandlerMethod;

        public EventCoinAnimation(Point startLocation, int lowestThresehold, EventOnCoin coinEvent, FormConnectFourGameBoard.GameState gState)
        {
            r_StartLoaction = startLocation;
            r_LowestThresehold = lowestThresehold;
            r_CoinEvent = coinEvent;
            r_State = gState;
        }

        public EventHandler EventHandlerMethod
        {
            get { return m_EventHandlerMethod; }
            set { m_EventHandlerMethod = value; }
        }

        public FormConnectFourGameBoard.GameState State
        {
            get { return r_State; }
        }

        public EventOnCoin CoinArgs
        {
            get { return r_CoinEvent; }
        }

        public Point StartLoaction
        {
            get { return r_StartLoaction; }
        }

        public int BotoomLimit
        {
            get { return r_LowestThresehold; }
        }
    }
}
