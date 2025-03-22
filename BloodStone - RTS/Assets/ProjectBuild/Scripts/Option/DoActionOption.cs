using System;

namespace Option
{
    public struct DoActionOption
    {
        public string Name;
        public Action Action;
        public ActionType myEnum;
    }
}