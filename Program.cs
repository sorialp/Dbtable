using System;
using Microsoft.Data.SqlClient;
using static System.Console;

WriteLine("It runs!");

// Create the connection to the resource
// using (SqlConnection conn = new SqlConnection())
SqlConnection conn = new SqlConnection();

// Create the connectionString - USE YOUR CREDENTIALS
conn.ConnectionString = "Server=localhost\\SQLEXPRESS; Database=Users; Trusted_Connection=true;Trust Server Certificate=true";
conn.Open();

// Create the command
SqlCommand command = new SqlCommand("SELECT * FROM PeterTable", conn);
int max = 0;

// Read from the database
using (SqlDataReader reader = command.ExecuteReader())
{
    WriteLine("UserID\tUserName\tUserEmail");
    while (reader.Read())
    {
        WriteLine(String.Format("{0} \t | {1} \t | {2}", reader[0], reader[1], reader[2]));
        max = Convert.ToInt32(reader[0].ToString());
    }
}

WriteLine("Data displayed! Press enter to Proceed!");
WriteLine("Max ID is " + max + ".");
WriteLine("Select is successful. Press Enter to Proceed");
ReadLine();
Clear();

// Insert data into the database, the query uses parameters - secure way to insert
WriteLine("INSERT INTO command");
SqlCommand insertCommand = new SqlCommand("INSERT INTO PeterTable (UserId, UserName, UserEmail) VALUES (@0, @1, @2)", conn);
insertCommand.Parameters.Add(new SqlParameter("0", (max + 1)));
insertCommand.Parameters.Add(new SqlParameter("1", "New User"));
insertCommand.Parameters.Add(new SqlParameter("2", "newuser@kean.edu"));

// Execute the command, and print the values of the columns affected
// through the command executed.
WriteLine("Commands executed! Total rows affected are " + insertCommand.ExecuteNonQuery());
WriteLine("Insert is successful. Press Enter to Proceed");
ReadLine();
Clear();

// In this section, there is an example of the Exception / caught error case
WriteLine("Now the error trial! Press Enter to Complete.");
try
{
    // Create the command to execute! With the wrong name of the table (Depends on your Database tables)
    SqlCommand errorCommand = new SqlCommand("SELECT * FROM PeterTable", conn);
    errorCommand.ExecuteNonQuery();
}
catch (SqlException er)
{
    WriteLine("There was an error reported by SQL Server: " + er.Message);
}

// Final step, close the resources flush dispose them. ReadLine to prevent the console from closing.
WriteLine("All done! Good Job!");
ReadLine();
