
using System.Data.SqlClient;

internal class Program
{
    public static void Main(string[] args)
    {
        // Creating Connection 
        string connectionString = "data source=localhost,1450; database=Student; user id=sa; password=vasugiS123; MultiSubnetFailover=True;";
        SqlConnection sqlconnect = new SqlConnection(connectionString);
        using (sqlconnect)
        {
            SqlCommand cmd = new SqlCommand("select * from Student_Details", sqlconnect); // Creating SqlCommand objcet 
            //cmd.CommandText = "insert into Student_Details values('Mubeen')";
            //cmd.Connection = sqlconnect;
            sqlconnect.Open(); // Opening Connection 
            //int rowsAffected = cmd.ExecuteNonQuery(); // Define number of records affected. 
            SqlDataReader stu_details = cmd.ExecuteReader(); // Executing the SQL query
            while (stu_details.Read())
            {
                Console.WriteLine(stu_details["Student_Name"]);
            }
            //Console.WriteLine("Inserted Rows = " + rowsAffected);
            Console.WriteLine("Connection Established Successfully");
        }
    }
}

