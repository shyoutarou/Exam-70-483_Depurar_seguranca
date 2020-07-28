using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace SqlException_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            string queryString = "EXECUTE NonExistantStoredProcedure";
            StringBuilder errorMessages = new StringBuilder();
            string connectionString = ConfigurationManager.ConnectionStrings["PeopleConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                }

                Console.ReadKey();
            }


            try
            {
                // code here  
            }
            catch (SqlException odbcEx)
            {
                // Handle more specific SqlException exception here.  
            }
            catch (Exception ex)
            {
                // Handle generic ones here.  
            }

            try
            {
                // code here  
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    // Handle more specific SqlException exception here.  
                }
                else
                {
                    // Handle generic ones here.  
                }
            }

        }
    }
}
