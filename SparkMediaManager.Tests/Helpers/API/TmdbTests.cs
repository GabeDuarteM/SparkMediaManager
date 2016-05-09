// Developed by: Gabriel Duarte
// 
// Created at: 30/04/2016 00:34

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparkMediaManager.Helpers.API;
using SparkMediaManager.Models;

namespace SparkMediaManager.Tests.Helpers.API
{
    [TestClass()]
    public class TmdbTests
    {
        [TestMethod()]
        public void GetBuscaFilmesAsyncTest()
        {
            Dictionary<int, string> dicTiaMay = Tmdb.GetBuscaFilmesAsync("Guerra civil").Result;
            Assert.IsTrue(dicTiaMay.ContainsKey(271110));

            Dictionary<int, string> dicFumiga = Tmdb.GetBuscaFilmesAsync("Homem Formiga").Result;
            Assert.IsTrue(dicFumiga.ContainsKey(102899));

            Dictionary<int, string> dicLeviosaaa = Tmdb.GetBuscaFilmesAsync("Harry Potter Pedra Filosofal").Result;
            Assert.IsTrue(dicLeviosaaa.ContainsKey(671));
        }

        [TestMethod()]
        public void GetFilmeAsyncTest()
        {
            Filme objTiaMay = Tmdb.GetFilmeAsync(271110).Result;
            Assert.IsNotNull(objTiaMay);
            Assert.IsFalse(string.IsNullOrWhiteSpace(objTiaMay.StrTitulo));
            Assert.IsTrue(objTiaMay.IntCodigoTmdb == 271110);

            Filme objFumiga = Tmdb.GetFilmeAsync(102899).Result;
            Assert.IsNotNull(objFumiga);
            Assert.IsFalse(string.IsNullOrWhiteSpace(objFumiga.StrTitulo));
            Assert.IsTrue(objFumiga.IntCodigoTmdb == 102899);

            Filme objLeviosaa = Tmdb.GetFilmeAsync(671).Result;
            Assert.IsNotNull(objLeviosaa);
            Assert.IsFalse(string.IsNullOrWhiteSpace(objLeviosaa.StrTitulo));
            Assert.IsTrue(objLeviosaa.IntCodigoTmdb == 671);
        }
    }
}
