using System;

namespace Game.Gameplay.Options
{
    public struct DoActionOption
    {
        public string Name;
        public Action Action;
        public ActionType myEnum;
    }
}