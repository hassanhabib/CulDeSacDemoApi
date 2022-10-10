using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryAccounts;

namespace CulDeSacApi.Services.Orchestrations.LibraryAccounts
{
    public partial class LibraryAccountOrchestrationService
    {
        static readonly ActivitySource source = new ActivitySource("CulDeSacApi");
        private delegate void ReturningNothingTraceFunction();
        private delegate ValueTask<LibraryAccount> ReturningAsyncTraceFunction();

        private async ValueTask<LibraryAccount> Trace(
            ReturningAsyncTraceFunction function,
            string activityName,
            Dictionary<string, string> tags = null,
            Dictionary<string, string> baggage = null,
            ActivityEvent? activityEvent = null)
        {
            using (var activity = new ActivitySource("CulDeSacApi")
                .StartActivity(activityName, ActivityKind.Internal)!)
            {
                SetupActivity(activity, tags, baggage, activityEvent);
                var result = await function();
                activity.Stop();

                return result;
            }
        }

        private void Trace(
            ReturningNothingTraceFunction function,
            string activityName,
            Dictionary<string, string> tags = null,
            Dictionary<string, string> baggage = null,
            ActivityEvent? activityEvent = null)
        {
            using (var activity = source.StartActivity(activityName, ActivityKind.Internal)!)
            {
                SetupActivity(activity, tags, baggage, activityEvent);
                function();
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
