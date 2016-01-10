using FileDbNs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlPractice
{
    class NoSqlDb
    {

        const string DBFILE = "c:\\tempShortTerm\\MyDatabase.fdb";

        public void CreateDb()
        {

            FileDb _db = new FileDb();

            Field field;
            var fields = new List<Field>(20);
            field = new Field("ID", DataTypeEnum.Int32);
            field.AutoIncStart = 0;
            field.IsPrimaryKey = true;
            fields.Add(field);
            field = new Field("FirstName", DataTypeEnum.String);
            fields.Add(field);
            field = new Field("LastName", DataTypeEnum.String);
            fields.Add(field);
            field = new Field("BirthDate", DataTypeEnum.DateTime);
            fields.Add(field);
            field = new Field("IsCitizen", DataTypeEnum.Bool);
            fields.Add(field);
            field = new Field("DoubleField", DataTypeEnum.Double);
            fields.Add(field);
            field = new Field("ByteField", DataTypeEnum.Byte);
            fields.Add(field);

            // array types
            field = new Field("StringArrayField", DataTypeEnum.String);
            field.IsArray = true;
            fields.Add(field);
            field = new Field("ByteArrayField", DataTypeEnum.Byte);
            field.IsArray = true;
            fields.Add(field);
            field = new Field("IntArrayField", DataTypeEnum.Int32);
            field.IsArray = true;
            fields.Add(field);
            field = new Field("DoubleArrayField", DataTypeEnum.Double);
            field.IsArray = true;
            fields.Add(field);
            field = new Field("DateTimeArrayField", DataTypeEnum.DateTime);
            field.IsArray = true;
            fields.Add(field);
            field = new Field("BoolArrayField", DataTypeEnum.Bool);
            field.IsArray = true;
            fields.Add(field);
            _db.Create(DBFILE, fields.ToArray());

            _db.Dispose();
        }


        public void AddRecords()
        {
            FileDb _db = new FileDb();
            _db.Open(DBFILE, false);
            var record = new FieldValues();
            record.Add("FirstName", "Nancy");
            record.Add("LastName", "Davolio");
            record.Add("BirthDate", new DateTime(1968, 12, 8));
            record.Add("IsCitizen", true);
            record.Add("DoubleField", 1.23);
            record.Add("ByteField", 1);
            record.Add("StringArrayField", new string[] { "s1", "s2", "s3" });
            record.Add("ByteArrayField", new Byte[] { 1, 2, 3, 4 });
            record.Add("IntArrayField", new int[] { 100, 200, 300, 400 });
            record.Add("DoubleArrayField", new double[] { 1.2, 2.4, 3.6, 4.8 });
            record.Add("DateTimeArrayField", new DateTime[]
                { DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now });
            record.Add("BoolArrayField", new bool[] { true, false, true, false });
            _db.AddRecord(record);
            _db.Dispose();

        }

        public Table ReadRecord()
        {
            FileDb _db = new FileDb();
            _db.Open(DBFILE, false);
            // the easy way, using the expression parser
    //        FilterExpressionGroup srchExpGrp = FilterExpressionGroup.Parse
    //("(FirstName ~= 'andrew' OR FirstName ~= 'nancy') AND LastName = 'Fuller'");
            FilterExpressionGroup srchExpGrp = FilterExpressionGroup.Parse
    ("FirstName = 'Nancy'");

            Table table = _db.SelectRecords(srchExpGrp, null, null, false);

            //// equivalent building it manually
            //var fname1Exp = new FilterExpression
            //("FirstName", "andrew", ComparisonOperatorEnum.Equal, MatchTypeEnum.IgnoreCase);
            //var fname2Exp = new FilterExpression
            //("FirstName", "nancy", ComparisonOperatorEnum.Equal, MatchTypeEnum.IgnoreCase);
            //var lnameExp = new FilterExpression
            //("LastName", "Fuller", ComparisonOperatorEnum.Equal, MatchTypeEnum.UseCase);
            //var fnamesGrp = new FilterExpressionGroup();
            //fnamesGrp.Add(BoolOpEnum.Or, fname1Exp);
            //fnamesGrp.Add(BoolOpEnum.Or, fname2Exp);
            //var allNamesGrp = new FilterExpressionGroup();
            //allNamesGrp.Add(BoolOpEnum.And, lnameExp);
            //allNamesGrp.Add(BoolOpEnum.And, fnamesGrp);
            //// should get the same records
            //table = _db.SelectRecords(allNamesGrp, null, null, false);
            _db.Dispose();
            return table;

        }


        public int DeleteRecord()
        {
            FileDb _db = new FileDb();
            _db.Open(DBFILE, false);
            // the easy way, using the expression parser
            //        FilterExpressionGroup srchExpGrp = FilterExpressionGroup.Parse
            //("(FirstName ~= 'andrew' OR FirstName ~= 'nancy') AND LastName = 'Fuller'");
            FilterExpressionGroup srchExpGrp = FilterExpressionGroup.Parse
    ("FirstName = 'Nancy'");

            var ret = _db.DeleteRecords(srchExpGrp);
            _db.Dispose();
            return ret;

        }

    }
}
