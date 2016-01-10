using FileDbNs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//http://www.codeproject.com/Articles/159550/FileDb-A-Simple-NoSql-Database-for-Silverlight-Win
namespace NoSqlPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            NoSqlDb nosqldb = new NoSqlDb();
            nosqldb.CreateDb();
            var table = nosqldb.ReadRecord();
            Debug.Assert(table.Count == 0);
            nosqldb.AddRecords();
            table = nosqldb.ReadRecord();
            Debug.Assert(table.Count != 0);


            var query =
            from custRec in table
            //select custRec;
            //join orderRec in orders on custRec["CustomerID"] equals orderRec["CustomerID"]
            select new
            {
                FirstName = custRec["FirstName"],
                LastName = custRec["LastName"],
                BirthDate = custRec["BirthDate"],
                IsCitizen = custRec["IsCitizen"]
            };



            var d = nosqldb.DeleteRecord();

            table = nosqldb.ReadRecord();
            Debug.Assert(table.Count == 0);
        }
    }
}
