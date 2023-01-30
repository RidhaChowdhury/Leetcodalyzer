using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

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
            using (NpgsqlConnection connection = new NpgsqlConnection(
                "Host=ruby.db.elephantsql.com;" +
                "Username=dqvfqhvx;" +
                "Password=6Ao49zHAdPUSRZxk0cXSY7-ZHFMr4ytf;" +
                "Database=dqvfqhvx;")) {
                connection.Open();

                // Create a command
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM grind169_raw WHERE problem NOT IN (SELECT problem FROM neet307_raw) UNION SELECT * FROM neet307_raw", connection)) {
                    /*NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet);
                    List<LeetCodeProblem> problemList = new List<LeetCodeProblem>();
                    DataTable table = dataSet.Tables[0];

                    foreach (DataRow row in table.Rows) {
                        problemList.Add(new LeetCodeProblem {
                            name = (string)row["problem"]
                        });
                    }
                    return problemList;*/
                    // Start reading the results of the command
                    using (var reader = command.ExecuteReader()) {
                        // Store the results in a list of LeetCodeProblems
                        var problemList = new List<LeetCodeProblem>();
                        int problemNumber = 0;
                        // Start putting the problems in a list
                        while (reader.Read()) {
                            problemList.Add(new LeetCodeProblem { 
                                name = (string)reader["problem"],
                                number = problemNumber++,
                                difficulty = (string)reader["difficulty"],
                                category = (string)reader["category"]
                            });
                        }
                        return problemList;
                    }
                }
            }
        }
    }
}