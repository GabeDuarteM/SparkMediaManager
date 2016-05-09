// Developed by: Gabriel Duarte
// 
// Created at: 30/04/2016 19:07

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparkMediaManager.Helpers.API;
using SparkMediaManager.Models;

namespace SparkMediaManager.Tests.Helpers.API
{
    [TestClass]
    public class TvdbTests
    {
        [TestMethod()]
        public void GetBuscaSeriesAsyncTest()
        {
            Dictionary<int, string> dicGot = Tvdb.GetBuscaSeriesAsync("Guerra dos tronos").Result;
            Assert.IsTrue(dicGot.ContainsKey(121361));

            Dictionary<int, string> dicHeisenberg = Tvdb.GetBuscaSeriesAsync("Breaking Bad").Result;
            Assert.IsTrue(dicHeisenberg.ContainsKey(81189));

            Dictionary<int, string> dicEuSegunda = Tvdb.GetBuscaSeriesAsync("The walking dead").Result;
            Assert.IsTrue(dicEuSegunda.ContainsKey(153021));
        }

        [TestMethod]
        public void GetSerieAsyncTest()
        {
            Serie objGot = Tvdb.GetSerieAsync(121361).Result;
            Assert.IsTrue(objGot.IntCodigoTvdb == 121361);

            Serie objHeisenberg = Tvdb.GetSerieAsync(81189).Result;
            Assert.IsTrue(objHeisenberg.IntCodigoTvdb == 81189);

            Serie objEuSegunda = Tvdb.GetSerieAsync(153021).Result;
            Assert.IsTrue(objEuSegunda.IntCodigoTvdb == 153021);
        }
    }
}
