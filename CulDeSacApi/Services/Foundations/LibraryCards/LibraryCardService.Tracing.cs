using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryCards;

namespace CulDeSacApi.Services.Foundations.LibraryCards
{
    public partial class LibraryCardService
    {
        static readonly ActivitySource source = new ActivitySource("CulDeSacApi");
        private delegate ValueTask<LibraryCard> ReturningAsyncTraceFunction();

        private async ValueTask<LibraryCard> Trace(
            ReturningAsyncTraceFunction function,
            string activityName,
            Dictionary<string, string> tags = null,
            Dictionary<string, string> baggage = null,
            ActivityEvent? activityEvent = null)
        {
            using (var activity = new Activity(activityName)!)
            {
                SetupActivity(activity, tags, baggage, activityEvent);
                var result = await function();
                activity.Stop();

                return result;
            }
        }

        private static void SetupActivity(
            Activity activity,
            Dictionary<string, string> tags = null,
            Dictionary<string, string> baggage = null,
            ActivityEvent? activityEvent = null)
        {
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    activity.AddTag(tag.Key, tag.Value);
                }
            }

            if (baggage != null)
            {
                foreach (var baggageItem in baggage)
                {
                    activity.AddBaggage(baggageItem.Key, baggageItem.Value);
                }
            }

            if (activityEvent != null)
            {
                activity.AddEvent(activityEvent.Value);
            }

            activity.Start();
        }
    }
}
