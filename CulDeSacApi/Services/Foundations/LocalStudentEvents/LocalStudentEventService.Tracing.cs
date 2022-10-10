using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace CulDeSacApi.Services.Foundations.LocalStudentEvents
{
    public partial class LocalStudentEventService
    {
        static readonly ActivitySource source = new ActivitySource("CulDeSacApi");
        private delegate ValueTask ReturningNothingAsyncTraceFunction();

        private async ValueTask Trace(
            ReturningNothingAsyncTraceFunction function,
            string activityName,
            Dictionary<string, string> tags = null,
            Dictionary<string, string> baggage = null,
            ActivityEvent? activityEvent = null
            )
        {
            using (var activity = source.StartActivity(activityName, ActivityKind.Internal)!)
            {
                SetupActivity(activity, tags, baggage, activityEvent);
                await function();
                activity.Stop();
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
        }

        private static string FormatTraceMessage(string message)
        {
            StringBuilder traceMessage = new StringBuilder();
            traceMessage.Append(message);
            traceMessage.AppendLine($"ParentSpanId: {Activity.Current.ParentSpanId}");
            traceMessage.AppendLine($"ParentId: {Activity.Current.ParentId}");
            traceMessage.AppendLine($"SpanId: {Activity.Current.SpanId}");
            traceMessage.AppendLine($"Id: {Activity.Current.Id}");

            return traceMessage.ToString();
        }
    }
}
