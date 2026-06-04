using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugPro;
using System;

namespace BugTests
{
    [TestClass]
    public class BugTests
    {
        [TestMethod]
        public void InitialState_ShouldBeNewDefect()
        {
            var bug = new Bug();

            Assert.AreEqual(BugState.NewDefect, bug.CurrentState, "Начальное состояние должно быть NewDefect");
        }

        [TestMethod]
        public void StartAnalysis_FromNewDefect_GoesToAnalysis()
        {
            var bug = new Bug();

            bug.StartAnalysis();

            Assert.AreEqual(BugState.Analysis, bug.CurrentState, "После StartAnalysis состояние должно быть Analysis");
        }

        [TestMethod]
        public void FromAnalysis_MarkAsFixed_GoesToFixed()
        {
            var bug = new Bug();
            bug.StartAnalysis();

            bug.MarkAsFixed();

            Assert.AreEqual(BugState.Fixed, bug.CurrentState, "После MarkAsFixed состояние должно быть Fixed");
        }

        [TestMethod]
        public void FromAnalysis_MarkAsNotDefect_GoesToNotDefect()
        {
            var bug = new Bug();
            bug.StartAnalysis();

            bug.MarkAsNotDefect();

            Assert.AreEqual(BugState.NotDefect, bug.CurrentState, "После MarkAsNotDefect состояние должно быть NotDefect");
        }

        [TestMethod]
        public void FromAnalysis_MarkAsWonTFix_GoesToWonTFix()
        {
            var bug = new Bug();
            bug.StartAnalysis();

            bug.MarkAsWonTFix();

            Assert.AreEqual(BugState.WonTFix, bug.CurrentState, "После MarkAsWonTFix состояние должно быть WonTFix");
        }

        [TestMethod]
        public void FromAnalysis_MarkAsDuplicate_GoesToDuplicate()
        {
            var bug = new Bug();
            bug.StartAnalysis();

            bug.MarkAsDuplicate();

            Assert.AreEqual(BugState.Duplicate, bug.CurrentState, "После MarkAsDuplicate состояние должно быть Duplicate");
        }

        [TestMethod]
        public void FromAnalysis_MarkAsNotReproducible_GoesToNotReproducible()
        {
            var bug = new Bug();
            bug.StartAnalysis();

            bug.MarkAsNotReproducible();

            Assert.AreEqual(BugState.NotReproducible, bug.CurrentState, "После MarkAsNotReproducible состояние должно быть NotReproducible");
        }

        [TestMethod]
        public void FromAnalysis_NeedMoreInfo_GoesToNeedMoreInfo()
        {
            var bug = new Bug();
            bug.StartAnalysis();

            bug.NeedMoreInfo();

            Assert.AreEqual(BugState.NeedMoreInfo, bug.CurrentState, "После NeedMoreInfo состояние должно быть NeedMoreInfo");
        }

        [TestMethod]
        public void FromAnalysis_MarkAsSeparateSolution_GoesToSeparateSolution()
        {
            var bug = new Bug();
            bug.StartAnalysis();

            bug.MarkAsSeparateSolution();

            Assert.AreEqual(BugState.SeparateSolution, bug.CurrentState, "После MarkAsSeparateSolution состояние должно быть SeparateSolution");
        }

        [TestMethod]
        public void FromAnalysis_MarkAsNoTimeNow_GoesToNoTimeNow()
        {
            var bug = new Bug();
            bug.StartAnalysis();

            bug.MarkAsNoTimeNow();

            Assert.AreEqual(BugState.NoTimeNow, bug.CurrentState, "После MarkAsNoTimeNow состояние должно быть NoTimeNow");
        }

        [TestMethod]
        public void FromAnalysis_MarkAsOtherProduct_GoesToOtherProduct()
        {
            var bug = new Bug();
            bug.StartAnalysis();

            bug.MarkAsOtherProduct();

            Assert.AreEqual(BugState.OtherProduct, bug.CurrentState, "После MarkAsOtherProduct состояние должно быть OtherProduct");
        }

        [TestMethod]
        public void FromNeedMoreInfo_ProvideInfo_ReturnsToAnalysis()
        {
            var bug = new Bug();
            bug.StartAnalysis();
            bug.NeedMoreInfo();

            bug.ProvideInfo();

            Assert.AreEqual(BugState.Analysis, bug.CurrentState, "После ProvideInfo состояние должно вернуться в Analysis");
        }

        [TestMethod]
        public void FromNewDefect_MarkAsFixed_ThrowsInvalidOperationException()
        {
            var bug = new Bug();

            Assert.ThrowsException<InvalidOperationException>(
                () => bug.MarkAsFixed(),
                "Из NewDefect нельзя перейти в Fixed"
            );
        }

        [TestMethod]
        public void FromNewDefect_MarkAsDuplicate_ThrowsInvalidOperationException()
        {
            var bug = new Bug();

            Assert.ThrowsException<InvalidOperationException>(
                () => bug.MarkAsDuplicate(),
                "Из NewDefect нельзя перейти в Duplicate"
            );
        }

        [TestMethod]
        public void FromFixed_StartAnalysis_ThrowsInvalidOperationException()
        {
            var bug = new Bug();
            bug.StartAnalysis();
            bug.MarkAsFixed();

            Assert.ThrowsException<InvalidOperationException>(
                () => bug.StartAnalysis(),
                "Из Fixed нельзя перейти в Analysis"
            );
        }

        [TestMethod]
        public void FromFixed_MarkAsFixed_ThrowsInvalidOperationException()
        {
            var bug = new Bug();
            bug.StartAnalysis();
            bug.MarkAsFixed();

            Assert.ThrowsException<InvalidOperationException>(
                () => bug.MarkAsFixed(),
                "Из Fixed нельзя повторно перейти в Fixed"
            );
        }

        [TestMethod]
        public void FromAnalysis_ProvideInfo_ThrowsInvalidOperationException()
        {
            var bug = new Bug();
            bug.StartAnalysis();

            Assert.ThrowsException<InvalidOperationException>(
                () => bug.ProvideInfo(),
                "Из Analysis нельзя вызвать ProvideInfo (только из NeedMoreInfo)"
            );
        }

        [TestMethod]
        public void FromNeedMoreInfo_MarkAsFixed_ThrowsInvalidOperationException()
        {
            var bug = new Bug();
            bug.StartAnalysis();
            bug.NeedMoreInfo();

            Assert.ThrowsException<InvalidOperationException>(
                () => bug.MarkAsFixed(),
                "Из NeedMoreInfo нельзя перейти в Fixed (сначала нужно ProvideInfo)"
            );
        }

        [TestMethod]
        public void FromNotDefect_StartAnalysis_ThrowsInvalidOperationException()
        {
            var bug = new Bug();
            bug.StartAnalysis();
            bug.MarkAsNotDefect();

            Assert.ThrowsException<InvalidOperationException>(
                () => bug.StartAnalysis(),
                "Из NotDefect нельзя перейти в Analysis"
            );
        }

        [TestMethod]
        public void FromSeparateSolution_StartAnalysis_ThrowsInvalidOperationException()
        {
            var bug = new Bug();
            bug.StartAnalysis();
            bug.MarkAsSeparateSolution();

            Assert.ThrowsException<InvalidOperationException>(
                () => bug.StartAnalysis(),
                "Из SeparateSolution нельзя перейти в Analysis"
            );
        }

        [TestMethod]
        public void AfterInvalidTransition_StateRemainsUnchanged()
        {
            var bug = new Bug();

            try
            {
                bug.MarkAsFixed();
            }
            catch (InvalidOperationException)
            {
            }

            Assert.AreEqual(BugState.NewDefect, bug.CurrentState, "После невалидного перехода состояние не должно измениться");
        }

        [TestMethod]
        public void FullValidFlow_NewDefect_Analysis_Fixed()
        {
            var bug = new Bug();

            bug.StartAnalysis();
            bug.MarkAsFixed();

            Assert.AreEqual(BugState.Fixed, bug.CurrentState, "Полный путь NewDefect → Analysis → Fixed");
        }

        [TestMethod]
        public void FullValidFlow_NewDefect_Analysis_NeedMoreInfo_ProvideInfo_Fixed()
        {
            var bug = new Bug();

            bug.StartAnalysis();
            bug.NeedMoreInfo();
            bug.ProvideInfo();
            bug.MarkAsFixed();

            Assert.AreEqual(BugState.Fixed, bug.CurrentState, "Путь с запросом информации и возвратом должен завершиться в Fixed");
        }
    }
}