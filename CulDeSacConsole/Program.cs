using System.Diagnostics;
using CulDeSacConsole.Models.Students;
using RESTFulSense.Clients;

namespace CulDeSacConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString()
            };

            using (var activity = new Activity("CulDeSacConsole"))
            {
                activity.AddBaggage("StudentId", student.Id.ToString());
                activity.AddBaggage("Student", student.Name);
                activity.Start();

                var apiClient = new RESTFulApiClient();

                Student newstudent =
                    apiClient.PostContentAsync<Student>(
                        relativeUrl: "https://localhost:44363/api/students",
                        content: student).Result;

                activity.Stop();

                Console.WriteLine($"TraceId: {activity.TraceId}");
                Console.WriteLine($"SpanId: {activity.SpanId}");
                Console.WriteLine($"Id: {activity.ParentId}");
                Console.WriteLine($"ParentId: {activity.ParentId}");
                Console.ReadLine();
            }


        }
    }
}