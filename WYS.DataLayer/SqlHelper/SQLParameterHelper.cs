using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace WYS.DataLayer.SqlHelper
{
    ///<summary>
    ///  Author:        Petr Kures
    ///  Created:	   2007-09-05
    ///  Last Modified: 2007-09-05
    ///  This class encapsulates parameter handling for stored procedures and text commands.
    ///  The use and distribution terms for this software are covered by the
    ///  Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
    ///  which can be found in the file CPL.TXT at the root of this distribution.
    ///  By using this software in any fashion, you are agreeing to be bound by
    ///  the terms of this license.
    ///  You must not remove this notice, or any other, from this software.
    ///
    ///  2007-11-23 modified by Joe Audette
    ///</summary>
    public class SqlParameterHelper
    {
        private CommandType _cmdType;
        private string _connectionString = ConfigurationManager.ConnectionStrings["WYSConnection"].ConnectionString;
        private int _index;
        private int _paramCnt;
        private bool _parametersDefined;

        #region Constructors

        public SqlParameterHelper(string commandText, CommandType cmdType, int paramCnt)
        {
            Initialize(_connectionString, commandText, cmdType, paramCnt);
        }

        public SqlParameterHelper(string commandText, int paramCnt)
        {
            Initialize(_connectionString, commandText, CommandType.StoredProcedure, paramCnt);
        }

        #endregion

        public SqlParameter[] Parameters { get; private set; }

        public string CommandText { get; private set; }

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        private void BeginDefineSqlParameters()
        {
            _index = 0;
            InitializeArray();
        }

        private void InitializeArray()
        {
            if (
                (ConfigurationManager.AppSettings["CacheMSSQLParameters"] != null)
                &&
                (string.Equals(ConfigurationManager.AppSettings["CacheMSSQLParameters"], "true",
                               StringComparison.InvariantCultureIgnoreCase))
                && (_cmdType == CommandType.StoredProcedure)
                )
            {
                // this is a slow line of code
                Parameters = SqlHelperParameterCache.GetSpParameterSet(
                    _connectionString, CommandText);
                Debug.Assert(Parameters.Length == _paramCnt, "parameters count doesn't match DB definition");
                _parametersDefined = true;
            }
            else
            {
                Parameters = new SqlParameter[_paramCnt];
                _parametersDefined = false;
            }
        }


        private void Initialize(string pConnectionInfo, string pCommandText, CommandType pCmdType, int pParamCnt)
        {
            if (string.IsNullOrEmpty(pConnectionInfo))
                throw new ArgumentNullException("pConnectionInfo");
            if (string.IsNullOrEmpty(pCommandText)) throw new ArgumentNullException("pCommandText");
            _connectionString = pConnectionInfo;
            _paramCnt = pParamCnt;
            CommandText = pCommandText;
            _cmdType = pCmdType;

            BeginDefineSqlParameters();
        }

        public void DefineSqlParameter(String paramName, SqlDbType type, ParameterDirection dir, object value)
        {
            DefineSqlParameter(paramName, type, 0, dir, value, false);
        }

        public void DefineSqlParameter(String paramName, SqlDbType type, int size, ParameterDirection dir, object value)
        {
            DefineSqlParameter(paramName, type, size, dir, value, true);
        }

        private void DefineSqlParameter(
            String paramName,
            SqlDbType type,
            int size,
            ParameterDirection dir,
            object value,
            bool sizeProvided)
        {
            Debug.Assert(_index < Parameters.Length, "wrong number of parameters");
            Debug.Assert(_index < _paramCnt, "trying to define more parameters then defined count");

            if (!_parametersDefined)
            {
                if (sizeProvided)
                    Parameters[_index] = new SqlParameter(paramName, type, size);
                else
                    Parameters[_index] = new SqlParameter(paramName, type);
                Parameters[_index].Direction = dir;
            }
            Debug.Assert(Parameters[_index].Direction == dir, "parameter's direction doesn't match cached parameters");
            Debug.Assert(
                string.Equals(Parameters[_index].ParameterName, paramName, StringComparison.InvariantCultureIgnoreCase),
                "parameter's name doesn't match cached parameters");
            Debug.Assert(
                ((type != SqlDbType.NText)
                 && (Parameters[_index].SqlDbType == type))
                ||
                ((type == SqlDbType.NText)
                 && (Parameters[_index].SqlDbType == SqlDbType.NVarChar))
                ||
                ((type == SqlDbType.Image)
                 && (Parameters[_index].SqlDbType == SqlDbType.VarBinary))
                , "parameter's type doesn't match cached parameters"
                );

            Parameters[_index].Value = value;
            _index++;
        }

        public IDataReader ExecuteReader()
        {
            Debug.Assert((Parameters.Length == _index) && (_paramCnt == _index), "not all parameters were defined");
            return SqlHelper.ExecuteReader(_connectionString, _cmdType, CommandText, Parameters);
        }

        public int ExecuteNonQuery()
        {
            Debug.Assert((Parameters.Length == _index) && (_paramCnt == _index), "not all parameters were defined");
            return SqlHelper.ExecuteNonQuery(_connectionString, _cmdType, CommandText, Parameters);
        }

        public int ExecuteNonQuery(SqlConnection conn)
        {
            Debug.Assert((Parameters.Length == _index) && (_paramCnt == _index), "not all parameters were defined");
            return SqlHelper.ExecuteNonQuery(conn, _cmdType, CommandText, Parameters);
        }

        public object ExecuteScalar()
        {
            Debug.Assert((Parameters.Length == _index) && (_paramCnt == _index), "not all parameters were defined");
            return SqlHelper.ExecuteScalar(_connectionString, _cmdType, CommandText, Parameters);
        }

        public object ExecuteScalar(SqlConnection conn)
        {
            Debug.Assert((Parameters.Length == _index) && (_paramCnt == _index), "not all parameters were defined");
            return SqlHelper.ExecuteScalar(conn, _cmdType, CommandText, Parameters);
        }

        public DataSet ExecuteDataset()
        {
            Debug.Assert((Parameters.Length == _index) && (_paramCnt == _index), "not all parameters were defined");
            return SqlHelper.ExecuteDataset(_connectionString, _cmdType, CommandText, Parameters);
        }
    }
}