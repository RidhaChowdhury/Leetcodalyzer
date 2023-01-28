using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Leetcodalyzer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeetCodeProblemController : ControllerBase
    {
        private readonly ILogger<LeetCodeProblemController> _logger;

        public LeetCodeProblemController(ILogger<LeetCodeProblemController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public List<LeetCodeProblem> Get() {
            System.Diagnostics.Debug.WriteLine("Queried the problems");
            // Connecting with the SQL database
            using (var connection = new NpgsqlConnection(
                "Host=ruby.db.elephantsql.com;" +
                "Username=dqvfqhvx;" +
                "Password=6Ao49zHAdPUSRZxk0cXSY7-ZHFMr4ytf;" +
                "Database=dqvfqhvx;")) {
                connection.Open();

                // Create a command
                using (var command = new NpgsqlCommand()) {
                    command.Connection = connection;
                    command.CommandText = "select * from grind169_raw gr";

                    // Start reading the results of the command
                    using (var reader = command.ExecuteReader()) {
                        // Store the results in a list of LeetCodeProblems
                        var problemList = new List<LeetCodeProblem>();

                        // Start putting the problems in a list
                        while (reader.Read()) {
                            Console.Beep();
                            System.Diagnostics.Debug.WriteLine("Grabbing a SQL entry");
                            problemList.Add(new LeetCodeProblem { name = (string)reader["problem"] });
                        }
                        return problemList;
                    }
                }
            }
        }
    }
}