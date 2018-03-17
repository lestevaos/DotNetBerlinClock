using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace BerlinClock
{
    [Binding]
    public class TimeDataStructureFeatureSteps
    {
        #region Parse

        private string _time;

        [When(@"the time is parsed as ""(.*)""")]
        public void WhenTheTimeIsParsedAs(string time)
        {
            _time = time;
        }

        [Then(@"the programmer should get a parse error in the (time) portion")]
        public void ThenTheProgrammerShouldGetAParseErrorInThePortion(string parameterName)
        {
            try
            {
                Time.Parse(_time);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(parameterName, e.ParamName);
                return;
            }

            Assert.Inconclusive();
        }

        [Then(@"the programmer should get a parsed instance that looks like ""(.*)""")]
        public void ThenTheProgrammerShouldGetAParsedInstanceThatLooksLike(string expectedTimeRepresentation)
        {
            Assert.AreEqual(expectedTimeRepresentation, Time.Parse(_time).ToString());
        }

        #endregion Parse

        #region Ctor

        int _hours, _minutes, _seconds;

        [When(@"the time is constructed using (-?\d+), (-?\d+), (-?\d+)")]
        public void WhenTheTimeIsConstructedUsing(int hours, int minutes, int seconds)
        {
            _hours = hours;
            _minutes = minutes;
            _seconds = seconds;
        }

        [Then(@"the programmer should get a constructor error in the (hours|minutes|seconds) portion")]
        public void ThenTheProgrammerShouldGetAConstructorErrorInThePortion(string parameterName)
        {
            try
            {
                new Time(_hours, _minutes, _seconds);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(parameterName, e.ParamName);
                return;
            }

            Assert.Inconclusive();
        }

        [Then(@"the programmer should get a constructed instance that looks like ""(.*)""")]
        public void ThenTheProgrammerShouldGetAConstructedInstanceThatLooksLike(string expectedStringRepresentation)
        {
            Assert.AreEqual(expectedStringRepresentation, new Time(_hours, _minutes, _seconds).ToString());
        }

        #endregion Ctor
    }
}
