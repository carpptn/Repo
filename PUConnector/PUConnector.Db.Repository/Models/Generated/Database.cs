



















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `PUConnector`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=(local);Initial Catalog=PUConnector.Db;User Id=sa;Password=1q2w3e4r`
//     Schema:                 ``
//     Include Views:          `False`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace PUConnector.Db.Repository
{

	public partial class PUConnectorDB : Database
	{
		public PUConnectorDB() 
			: base("PUConnector")
		{
			CommonConstruct();
		}

		public PUConnectorDB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			PUConnectorDB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static PUConnectorDB GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new PUConnectorDB();
        }

		[ThreadStatic] static PUConnectorDB _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

		public class Record<T> where T:new()
		{
			public static PUConnectorDB repo { get { return PUConnectorDB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }

			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }

			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

		}

	}
	



    
	[TableName("PUOrder")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class PUOrder : PUConnectorDB.Record<PUOrder>  
    {



		[Column] public long Id { get; set; }





		[Column] public DateTime Created { get; set; }





		[Column] public DateTime? Updated { get; set; }





		[Column] public string ExtOrderId { get; set; }





		[Column] public string OrderId { get; set; }





		[Column] public string Status { get; set; }





		[Column] public string OrderObject { get; set; }



	}

    
	[TableName("__RefactorLog")]


	[PrimaryKey("OperationKey", autoIncrement=false)]

	[ExplicitColumns]
    public partial class __RefactorLog : PUConnectorDB.Record<__RefactorLog>  
    {



		[Column] public Guid OperationKey { get; set; }



	}

    
	[TableName("PURefund")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class PURefund : PUConnectorDB.Record<PURefund>  
    {



		[Column] public long Id { get; set; }





		[Column] public long PUOrderId { get; set; }





		[Column] public DateTime Created { get; set; }





		[Column] public DateTime? Updated { get; set; }





		[Column] public string ExtRefundId { get; set; }





		[Column] public string RefundId { get; set; }





		[Column] public string Status { get; set; }





		[Column] public string RefundObject { get; set; }



	}

    
	[TableName("sysdiagrams")]


	[PrimaryKey("diagram_id")]



	[ExplicitColumns]
    public partial class sysdiagram : PUConnectorDB.Record<sysdiagram>  
    {



		[Column] public string name { get; set; }





		[Column] public int principal_id { get; set; }





		[Column] public int diagram_id { get; set; }





		[Column] public int? version { get; set; }





		[Column] public byte[] definition { get; set; }



	}

    
	[TableName("PUCommLog")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class PUCommLog : PUConnectorDB.Record<PUCommLog>  
    {



		[Column] public long Id { get; set; }





		[Column] public long PUOrderId { get; set; }





		[Column] public string RequestType { get; set; }





		[Column] public DateTime? RequestDate { get; set; }





		[Column] public string RequestContent { get; set; }





		[Column] public string ResponseType { get; set; }





		[Column] public DateTime? ResponseDate { get; set; }





		[Column] public string ResponseContent { get; set; }



	}


}


