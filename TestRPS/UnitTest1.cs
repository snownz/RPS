using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using R_P_S;

namespace TestRPS
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test01()
        {
            var str = "[ [\"Armando\", \"P\"], [\"Dave\", \"S\"] ]";
            var play = new RPS();
            var win = play.rps_game_winner(str);
            Assert.IsTrue("Dave" == win);
        }

        [TestMethod]
        public void Test01Lower()
        {
            var str = "[ [\"Armando\", \"p\"], [\"Dave\", \"s\"] ]";
            var play = new RPS();
            var win = play.rps_game_winner(str);
            Assert.IsTrue("Dave" == win);
        }

        [TestMethod]
        public void Test02()
        {
            var str = "[[ [ [\"Armando\", \"P\"], [\"Dave\", \"S\"] ], [[\"Richard\", \"R\"], [\"Michael\", \"S\"] ],  ],  [  [ [\"Allen\", \"S\"], [\"Omer\", \"P\"] ],  [ [\"David E.\", \"R\"], [\"Richard X.\", \"P\"] ]  ] ] ";
            var play = new RPS();
            var win = play.rps_tournament_winner(str);
            Assert.IsTrue("Richard" == win);
        }

        [TestMethod]
        public void Test02CaseInsensitive()
        {
            var str = "[[ [ [\"Armando\", \"p\"], [\"Dave\", \"s\"] ], [[\"Richard\", \"r\"], [\"Michael\", \"s\"] ],  ],  [  [ [\"Allen\", \"s\"], [\"Omer\", \"p\"] ],  [ [\"David E.\", \"r\"], [\"Richard X.\", \"P\"] ]  ] ] ";
            var play = new RPS();
            var win = play.rps_tournament_winner(str);
            Assert.IsTrue("Richard" == win);
        }
    }
}
