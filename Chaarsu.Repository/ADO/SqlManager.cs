using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Chaarsu.Repository.ADO
{
    public enum SqlCommandType
    {
        //Summary:
        //'  An SQL text command. (Default.)
        Text = 1,

        //' //
        //' // Summary:
        //'  //     The name of a stored procedure.
        StoredProcedure = 4,
        //'  //
        //'// Summary:
        //'//     The name of a table.
        TableDirect = 512
    }

    public class SqlManager
    {

    public const string pVer = "72D.260815";
    public string strType_Connection = "";
    public const string _LocalDB = "FMA_01";
//  public const string _LocalDB = "FMA_01";


    private List<SqlParameter> m_SqlParameters;
    private string m_Commandtext;
	private string m_ConStr;
    private string StrConnectionString="";
    private SqlConnection SqlCon;
    private SqlCommand SqlComm;
    private SqlTransaction SqlTran;
    private Boolean _IsTransaction;
    private string _ErrorDescription=string.Empty;
    public const string pDBError_Prefix = "The system is unable to process the request, please check error: ";


    public string strServerName = "";
    public string strDatabaseName = "";
    public string strUserID = "";
    public string strPassword = "";
    public string strCnType = "1";

    public string pSFUser = "";
    public string pSFUserPassword = "";
    public string pSFUserPassKey = "";
    public string pSFUser_Company = "";

    public string ErrorDescription {
        get { return _ErrorDescription;}
        set { value = _ErrorDescription; }
    }

   

    public Boolean BeginTransaction{
        get{ return _IsTransaction;}
        set { _IsTransaction = value; }
    }


    public void CommitTransaction()
    {
        if (_IsTransaction)
        {
            SqlTran.Commit();
            _IsTransaction = false;
        }
        
    }

    public void Rollbacktransaction()
    {
        if (_IsTransaction)
        {
                SqlTran.Rollback();
                _IsTransaction = false;
        }
    }

    public string Get_ErrorString(string vError)
    {
        return pDBError_Prefix + vError.Replace("'", "`").Replace("(", "[").Replace(")", "]").Replace('\r', '-').Replace('\n', '-');
    }

    public void OpenConnection()
    {
        if (SqlCon == null)
        {
            DataSet tmpDs = new DataSet();
            string _Server = "";

          
            StrConnectionString = ConfigurationManager.ConnectionStrings["MESADO"].ConnectionString;
            SqlCon = new SqlConnection(StrConnectionString);
           

            try
            {
                SqlCon.Open();
            }
            catch (Exception ex)
            {
                _ErrorDescription =Get_ErrorString(ex.Message);
                
               
            }


            if (_IsTransaction)
            {
                try
                {
                    SqlTran = SqlCon.BeginTransaction();

                }
                catch (Exception ex)
                {
                    _ErrorDescription = Get_ErrorString(ex.Message);
                }

            }
        }
    }



    public void CreateCammand(string CmdTxt, SqlCommandType SqlCommandType)
    {
        if (SqlComm == null)
        {
            SqlComm = new SqlCommand();
        }

        if (SqlCon == null)
        {
            OpenConnection();
        }
        else
        {
            if (SqlCon.State == 0)
            {
                SqlCon.Open();
            }
        }

        if (_IsTransaction)
        {
            SqlComm.Transaction = SqlTran;
        }

        
        SqlComm.Connection = SqlCon;
        SqlComm.CommandText = CmdTxt.ToString();

        switch (SqlCommandType){
            case SqlCommandType.StoredProcedure:
                SqlComm.CommandType = CommandType.StoredProcedure;
                break;
            case SqlCommandType.Text:
                SqlComm.CommandType = CommandType.Text;
                break;
            case SqlCommandType.TableDirect:
                SqlComm.CommandType = CommandType.TableDirect;
                break;
        }
   
                
    
    }


    public SqlManager()
	{
        
		m_SqlParameters = new List<SqlParameter>();
	}

     //public static  SqlManager Instance(){
     //    return ObjSqlHelper=new SqlManager();
     //}

	public List<SqlParameter> SqlParameters {
		get { return m_SqlParameters; }
	}

	public string CommandText {
		get { return m_Commandtext; }
		set { value = m_Commandtext; }
	}

    public SqlManager(string con)
	{
		m_ConStr = con;
	}
    /// <summary>
    /// Will return the DataTable 
    /// </summary>
    /// <param name="CmdTxt"></param>
    /// <param name="CmdType"></param>
    /// <param name="tblName"></param>
    /// <param name="CloseConnection"></param>
    /// <returns></returns>
    public DataTable ExecuteDataTable(string CmdTxt, SqlCommandType CmdType, SqlCommand _SqlComm, bool CloseConnection = true)
    {
        DataTable Dt = new DataTable();
        SqlDataAdapter Adapter = new SqlDataAdapter();

        try
        {
            SqlComm = _SqlComm;
            CreateCammand(CmdTxt, CmdType);
            //create a sqlcommand
            //****** parameter building Routine *******
            //SqlComm.Parameters.AddRange(m_SqlParameters.ToArray());
            //**************************************

            Adapter.SelectCommand = SqlComm;
            Adapter.Fill(Dt);


        }
        catch (Exception ex)
        {
            _ErrorDescription = Get_ErrorString(ex.Message);
        }
        finally
        {
            //if ((SqlComm != null))
            //{
            //    SqlComm.Parameters.Clear();
            //}

            if (CloseConnection & SqlComm.Connection.State != ConnectionState.Closed)
            {
                SqlComm.Connection.Close();
                SqlComm.Dispose();
                SqlCon.Dispose();
                SqlCon.Close();
                SqlCon = null;
            }
        }
        return Dt;
    }


        /// <summary>
        /// Will return the Dataset
        /// </summary>
        /// <param name="CmdTxt"></param>
        /// <param name="CmdType"></param>
        /// <param name="tblName"></param>
        /// <param name="CloseConnection"></param>
        /// <returns></returns>
    public DataSet ExecuteDataSet(string CmdTxt, SqlCommandType CmdType, SqlCommand _SqlComm, bool CloseConnection = true)
    {
        DataSet Ds = new DataSet();
        SqlDataAdapter Adapter = new SqlDataAdapter();

        try
        {
            SqlComm = _SqlComm;
            CreateCammand(CmdTxt, CmdType);
            //create a sqlcommand
            //****** parameter building Routine *******
            //SqlComm.Parameters.AddRange(m_SqlParameters.ToArray());
            //**************************************

            Adapter.SelectCommand = SqlComm;
            Adapter.Fill(Ds);


        }
        catch (Exception ex)
        {
            _ErrorDescription = Get_ErrorString(ex.Message);
        }
        finally
        {
            //if ((SqlComm != null))
            //{
            //    SqlComm.Parameters.Clear();
            //}

            if (CloseConnection & SqlComm.Connection.State != ConnectionState.Closed)
            {
                SqlComm.Connection.Close();
                SqlComm.Dispose();
                SqlCon.Dispose();
                SqlCon.Close();
                SqlCon = null;
            }
        }
        return Ds;
    }


    /// <summary>
    /// Return the Records 
    /// </summary>
    /// <param name="CmdTxt"></param>
    /// <param name="CmdType"></param>
    /// <param name="CloseConnection"></param>
    /// <returns></returns>

    public SqlDataReader ExecuteReader(string CmdTxt, SqlCommandType CmdType, SqlCommand _SqlComm, bool CloseConnection = true)
    {

        SqlDataReader SqlReadr = null;

        try
        {
            SqlComm = _SqlComm;
            CreateCammand(CmdTxt, CmdType);
            //create a sqlcommand

            //****** parameter building Routine *******
            //SqlComm.Parameters.AddRange(m_SqlParameters.ToArray());
            //**************************************

            if (CloseConnection)
            {
                SqlReadr = SqlComm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            else
            {
                SqlReadr = SqlComm.ExecuteReader();
            }

        }
        catch (Exception ex)
        {
            _ErrorDescription = Get_ErrorString(ex.Message);
        }
        finally
        {
            //if ((SqlComm != null))
            //{
            //    SqlComm.Parameters.Clear();
            //}
            if (CloseConnection & SqlComm.Connection.State != ConnectionState.Closed)
            {
                SqlComm.Connection.Close();
                SqlComm.Dispose();
                SqlCon.Dispose();
                SqlCon.Close();
                SqlCon = null;
            }

        }
        return SqlReadr;
    }

    
    /// <summary>
    /// will return number of rows effected'
    /// </summary>
    /// <param name="CmdTxt">TEXT Query or procedure name</param>
    /// <param name="CmdType">procedure/text/direct table</param>
    /// <param name="CloseConnection">by default yes , No to stay connected for commit and roll back transaction</param>
    /// <returns></returns>
    public int ExecuteNonQuery(string CmdTxt, SqlCommandType CmdType, SqlCommand _SqlComm, bool CloseConnection = true)
    {
       
        //'
        int val = 0;
        try
        {
            SqlComm = _SqlComm;
            CreateCammand(CmdTxt, CmdType);
            //create a sqlcommand
            //****** parameter building Routine *******
            //SqlComm.Parameters.AddRange(_SqlParameters.ToArray());
            //**************************************
            val = SqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            _ErrorDescription = Get_ErrorString(ex.Message);

        }
        finally
        {
            //if ((SqlComm != null))
            //{
            //    SqlComm.Parameters.Clear();
            //}

            if (CloseConnection & SqlComm.Connection.State != ConnectionState.Closed)
           
            {
                SqlComm.Connection.Close();
                SqlComm.Dispose();
                SqlCon.Dispose();
                SqlCon.Close();
                SqlCon = null;
            }
        }
        return val;

    }


        /// <summary>
        /// will retrun the first column of first row and remaining will be igrnored.'
        /// </summary>
        /// <param name="CmdTxt"></param>
        /// <param name="CmdType"></param>
        /// <param name="CloseConnection"></param>
        /// <returns></returns>
    public object ExecuteScaler(string CmdTxt, SqlCommandType CmdType,SqlCommand _SqlComm, bool CloseConnection = true)
    {
        
        
        object val = new object();
        try
        {
            SqlComm = _SqlComm;
            CreateCammand(CmdTxt, CmdType);
            //create a sqlcommand
            //****** parameter building Routine *******
            SqlComm.Parameters.AddRange(m_SqlParameters.ToArray());
            //**************************************

            val = SqlComm.ExecuteScalar();
        }
        catch (Exception ex)
        {
            _ErrorDescription = Get_ErrorString(ex.Message);
        }
        finally
        {
            //if ((SqlComm != null))
            //{
            //    SqlComm.Parameters.Clear();
            //}

            if (CloseConnection & SqlComm.Connection.State != ConnectionState.Closed)
            {
                SqlComm.Connection.Close();
                SqlComm.Dispose();
                SqlCon.Dispose();
                SqlCon.Close();
                SqlCon = null;
            }
        }
        return val;
    }

    //public void ExecuteNonQuery()
    //{
    //    using (SqlConnection con = new SqlConnection(m_ConStr)) {
    //        using (SqlCommand com = new SqlCommand(m_Commandtext, con)) {
    //            com.Parameters.AddRange(m_SqlParameters.ToArray());

    //            con.Open();
    //            com.ExecuteNonQuery();
    //            con.Close();
    //        }
    //    }
    //}
    }
}
