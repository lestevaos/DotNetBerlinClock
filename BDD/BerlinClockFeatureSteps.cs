using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BerlinClock
{
    [Binding]
    public class TheBerlinClockFeatureSteps
    {
        private ITimeConverter<string> _berlinClockTimeConverter = new BerlinClockTimeConverter();
        private Time _time;

        [When(@"the time is ""(.*)""")]
        public void WhenTheTimeIs(Time time)
        {
            _time = time;
        }

        [StepArgumentTransformation(@"(\d{2}:\d{2}:\d{2})")]
        public Time TimeTransform(string time)
        {
            return Time.Parse(time);
        }

        [Then(@"the clock should look like")]
        public void ThenTheClockShouldLookLike(string theExpectedBerlinClockOutput)
        {
            Assert.AreEqual(theExpectedBerlinClockOutput, _berlinClockTimeConverter.ConvertFrom(_time));
        }
    }
}
