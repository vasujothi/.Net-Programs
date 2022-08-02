
using System.Data.SqlClient;

internal class Program
{
    public static async Task Main(string[] args)
    {
        StudentService? s = null;      
        try
        {
            s = new StudentService();
            s.DisplayName();
            Console.WriteLine("TextContent");
            await foreach (string text in s.GetStudentFileDetails())
            {
                Console.WriteLine(text);
            }
        }
        finally
        {
            if (s != null)
            {
                s.Dispose();
                s = null;
            }
        }     
    }
}

public class StudentService : IDisposable
{
    private const string connectionString = "data source=localhost,1450; database=Student; user id=sa; password=vasugiS123; MultiSubnetFailover=True;";
    private readonly SqlConnection sqlconnect = default;
    private bool disposedValue;
    private string[]? outPutLines = null;

    public StudentService()
    {
        sqlconnect = new SqlConnection(connectionString);
    }

    // Return the data with asynchronous execution
    public async IAsyncEnumerable<string> GetStudentFileDetails()
    {    
        using (StreamReader file = new StreamReader(@"C:\Users\vsivajothi\OneDrive - Blackboard\Documents\DataBaseConnection\StudentTextFile.txt"))
        {
            string? ln;
            while ((ln = await file.ReadLineAsync()) != null)
            {
                yield return ln;                
            }
            file.Close();
        }
    }

    public void DisplayName()
    {
            SqlCommand cmd = new SqlCommand("select * from Student_Details", sqlconnect); // Creating SqlCommand objcet 
            sqlconnect.Open(); // Opening Connection 
            SqlDataReader stu_details = cmd.ExecuteReader(); // Executing the SQL query
            while (stu_details.Read())
            {
                Console.WriteLine(stu_details["Student_Name"]);
            }
            Console.WriteLine("Connection Established Successfully");
        
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }
            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            if (sqlconnect != null && sqlconnect.State == System.Data.ConnectionState.Open)
                {
                  sqlconnect.Close();
                  sqlconnect.Dispose();
                }
            if(outPutLines != null)
            {
                outPutLines = null;
            }
                // TODO: set large fields to null
                disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~StudentService()
    {
    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
         Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

