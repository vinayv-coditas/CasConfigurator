
using ConnectConfigure.CheckSqlServer;

if(CheckSqlserver.IsSqlServerInstalled())
{
    Console.WriteLine("SQL server is installed");
}
bool exit = false;  
do
{
    Console.WriteLine("Enter 1 for Windows Authentication\nEnter 2 for Sql Server Authentication\nEnter 3 to updtate config files\nEnter 4 to exit");
    int AuthenticationType = Convert.ToInt32(Console.ReadLine());
    switch (AuthenticationType)
    {
        case 1:
            Console.WriteLine("Enter Data Source:");
            string? DataSource = Console.ReadLine();
            Console.WriteLine("Enter Initial Catalog:");
            string? InitialCatalog = Console.ReadLine();
            string connectionString = $"Data Source={DataSource};Initial Catalog={InitialCatalog};Integrated Security=True;";
            string dbName = CheckSqlserver.GetDbName(connectionString);
            if (dbName != null)
            {
                Console.WriteLine("Database Name: " + dbName);
                Console.WriteLine("SQL Server connected successfully.");
            }
            else
            {
                Console.WriteLine("Failed to connect to SQL Server.");
            }

            bool isSqlConnected = CheckSqlserver.CheckSqlConnectivity(connectionString);
            if (isSqlConnected)
            {
                Console.WriteLine("SQL Server connected successfully.");
            }
            else
            {
                Console.WriteLine("Failed to connect to SQL Server.");

            }
            break;
        case 2:
            Console.WriteLine("Enter Data Source:");
            string? DataSourceSqlServerAuth = Console.ReadLine();
            Console.WriteLine("Enter Initial Catalog:");
            string? InitialCatalogSqlServerAuth = Console.ReadLine();
            Console.WriteLine("Enter User Id:");
            string? UserId = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            string? Password = Console.ReadLine();
            string connectionStringSqlAUth = $"Data Source={DataSourceSqlServerAuth};Initial Catalog={InitialCatalogSqlServerAuth};User ID={UserId};Password={Password};";
            string dbNamesqlAuth = CheckSqlserver.GetDbName(connectionStringSqlAUth);
            if (dbNamesqlAuth != null)
            {
                Console.WriteLine("Database Name: " + dbNamesqlAuth);
                Console.WriteLine("SQL Server connected successfully.");
            }
            else
            {
                Console.WriteLine("Failed to connect to SQL Server.");
            }

            bool isSqlConnectedsqlAuth = CheckSqlserver.CheckSqlConnectivity(connectionStringSqlAUth);
            if (isSqlConnectedsqlAuth)
            {
                Console.WriteLine("SQL Server connected successfully.");
            }
            else
            {
                Console.WriteLine("Failed to connect to SQL Server.");

            }
            break;
        case 3:
            FileHandler fileHandler = new FileHandler();
            Console.WriteLine("INPUT: Please enter filepath which you want to modify");
            string filepath = Console.ReadLine();
            if (filepath.Contains("/"))
            {
                filepath = Path.Combine(filepath.Split("/"));
            }
            filepath = filepath.Replace("\\\\", "\\");
            Console.WriteLine("INPUT: Please enter modified value");
            string replaceText = Console.ReadLine();
            if (!string.IsNullOrEmpty(@filepath) && !string.IsNullOrEmpty(replaceText))
            {
                fileHandler.readAndUpdateFile(filepath, replaceText);
            }
            else
            {
                Console.WriteLine("ERROR: Filepath or replacement text is missing");
            }
            break;
        case 4:
            exit=true;
            break;
        default:
            Console.WriteLine("Enter Valid Choice");
            break;
    }
}  while (!exit) ;




// Replace with your actual connection string




