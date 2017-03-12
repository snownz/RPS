using System.Collections.Generic;
using System.Linq;

namespace R_P_S
{
    public class Key
    {
        public StrategyModel Player0 { get; set; }
        public StrategyModel Player1 { get; set; }
        public StrategyModel Winner { get; set; }
    }

    public class Group
    {
        public Key[] Keys { get; set; }
        public StrategyModel[] Winners { get { return Keys.Select(x => x.Winner).ToArray(); } }
        public StrategyModel Winner { get; set; }
    }
}
