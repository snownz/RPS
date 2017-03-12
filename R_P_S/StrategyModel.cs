namespace R_P_S
{
    /// <summary>
    ///     Model of Strategy
    /// </summary>
    public class StrategyModel
    {
        /// <summary>
        ///     Player Name
        /// </summary>
        public string Player { get; set; }
        /// <summary>
        ///     Player Choice
        /// </summary>
        public Played Choice { get; set; }

        /// <summary>
        ///     New  func to more than
        /// </summary>
        /// <param name="ch1"></param>
        /// <param name="ch2"></param>
        /// <returns></returns>
        public static bool operator >(StrategyModel ch1, StrategyModel ch2)
        {
            return  (ch1.Choice > ch2.Choice && ch1.Choice - 2 != ch2.Choice) || ch1.Choice + 2 == ch2.Choice;
        }
        /// <summary>
        ///      New  func to less than
        /// </summary>
        /// <param name="ch1"></param>
        /// <param name="ch2"></param>
        /// <returns></returns>
        public static bool operator <(StrategyModel ch1, StrategyModel ch2)
        {
            return (ch2.Choice > ch1.Choice && ch2.Choice -2 != ch1.Choice)  || ch2.Choice + 2 == ch1.Choice;
        }
    }
}
