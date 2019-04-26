using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormUtilityLibrary.DataUtilities
{
    /// <summary>
    /// Generates sql queries for select, insert, update, delete DML
    /// </summary>
    public sealed class SQLRequstGenerator
    {
        /// <summary>
        /// Generates an insert request
        /// </summary>
        /// <param name="TableName">The name of the table in which data has to be inserted</param>
        /// <param name="Parameters">The parameters used for the insertion</param>
        /// <returns>A string request</returns>
        public static string Insert(string TableName, Parameter[] Parameters)
        {
            try
            {
                string request = $"INSERT INTO  {TableName} (";
                for (int i = 0; i < Parameters?.Length; i++)
                {
                    request += (i != (Parameters?.Length - 1) ? $"`{Parameters?[i].Name}`, " : $"`{Parameters?[i].Name}`)");
                }
                request += "VALUES (";
                for (int i = 0; i < Parameters?.Length; i++)
                {
                    if (Parameters?[i].ParamType != typeof(char) && Parameters?[i].ParamType != typeof(string))
                    {
                        if (Parameters?[i].Value == null)
                        {
                            request += (i != (Parameters?.Length - 1) ? $" null, " : $"null)");
                        }
                        else
                        {
                            request += (i != (Parameters?.Length - 1) ? $"{Parameters?[i].Value}, " : $"{Parameters?[i].Value})");
                        }
                    }
                    else
                        request += (i != (Parameters?.Length - 1) ? $"'{Parameters?[i].Value}', " : $"'{Parameters?[i].Value}')");
                }
                return request;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Generates a delete query
        /// </summary>
        /// <param name="TableName">The name of the table in which data has to be inserted</param>
        /// <param name="WhereClauseParam">The parameters to be introduced in the where clause</param>
        /// <param name="Id">The id of the element to delete</param>
        /// <returns>A string request</returns>
        public static string Delete(string TableName, Parameter[] WhereClauseParam = null, int Id = 0)
        {
            try
            {
                string request = $"DELETE FROM {TableName}";
                if (Id != 0) request += $" WHERE (Id = {Id})";
                if (WhereClauseParam != null && WhereClauseParam?.Length != 0) request += " WHERE (";
                for (int i = 0; i < WhereClauseParam?.Length; i++)
                {
                    if (WhereClauseParam?[i].ParamType != typeof(char) && WhereClauseParam?[i].ParamType != typeof(string))
                        request += (i != (WhereClauseParam?.Length - 1) ? $" `{WhereClauseParam?[i].Name}` = {WhereClauseParam?[i].Value}, " : $"`{WhereClauseParam?[i].Name}` = {WhereClauseParam?[i].Value})");
                    else if ((string)WhereClauseParam?[i].Value == "not null")
                        request += (i != (WhereClauseParam?.Length - 1) ? $"`{WhereClauseParam?[i].Name}` {WhereClauseParam?[i].Value}, " : $"`{WhereClauseParam?[i].Name}` {WhereClauseParam?[i].Value})");
                    else
                        request += (i != (WhereClauseParam?.Length - 1) ? $"`{WhereClauseParam?[i].Name}` = '{WhereClauseParam?[i].Value}', " : $"`{WhereClauseParam?[i].Name}` ='{WhereClauseParam?[i].Value}')");
                }
               return (request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Generates a select query
        /// </summary>
        /// <param name="TableName">The name of the table in which data has to be inserted</param>
        /// <param name="Parameters">The names of the columns to select</param>
        /// <param name="WhereClauseParam">The parameters to be introduced in the where clause</param>
        /// <returns>A string request</returns>
        public static string Select(string TableName, Parameter[] Parameters = null, Parameter[] WhereClauseParam = null)
        {
            try
            {
                string request = (Parameters == null || Parameters.Length == 0) ? "SELECT * " : "SELECT ";
                for (int i = 0; i < Parameters?.Length; i++)
                {
                    request += (i != (Parameters.Length - 1) ? $"`{Parameters[i].Name}`, " : $"`{Parameters[i].Name}`");
                }
                request += $"FROM `{TableName}`";
                if (WhereClauseParam != null && WhereClauseParam.Length != 0) request += " WHERE (";
                for (int i = 0; i < WhereClauseParam?.Length; i++)
                {
                    if ((string)WhereClauseParam?[i].Value == "not null")
                        request += (i != (WhereClauseParam?.Length - 1) ? $"`{WhereClauseParam?[i].Name}` <> null, " : $"`{WhereClauseParam?[i].Name}` <> null)");
                    else if (WhereClauseParam?[i].ParamType != typeof(char) && WhereClauseParam?[i].ParamType != typeof(string))
                        request += (i != (WhereClauseParam?.Length - 1) ? $" `{WhereClauseParam?[i].Name}` = {WhereClauseParam?[i].Value}, " : $"`{WhereClauseParam?[i].Name}` = {WhereClauseParam?[i].Value})");
                    else
                        request += (i != (WhereClauseParam?.Length - 1) ? $"`{WhereClauseParam?[i].Name}` = '{WhereClauseParam?[i].Value}', " : $"`{WhereClauseParam?[i].Name}` ='{WhereClauseParam?[i].Value}')");
                }
                //request += $" ORDERBY {(OrderAsc ? 1 : 0)}" ;
                return request;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Generates an update query
        /// </summary>
        /// <param name="TableName">The name of the table in which data has to be inserted</param>
        /// <param name="Parameters">The names of the columns to select</param>
        /// <param name="WhereClauseParam">The parameters to be introduced in the where clause</param>
        /// <param name="Id">The id of the element to update</param>
        /// <returns></returns>
        public static string Update(string TableName, Parameter[] Parameters, Parameter[] WhereClauseParam = null, int Id = 0)
        {
            try
            {
                string request = $"UPDATE {TableName} SET ";
                for (int i = 0; i < Parameters?.Length; i++)
                {
                    if (Parameters?[i].ParamType != typeof(char) && Parameters?[i].ParamType != typeof(string))
                    {

                        if (Parameters?[i].Value == null)
                        {
                            request += (i != (Parameters?.Length - 1) ? $" `{Parameters?[i].Name}` = null, " : $"`{Parameters?[i].Name}` = null)");
                        }
                        else
                        {
                            request += (i != (Parameters?.Length - 1) ? $"`{Parameters?[i].Name}` = {Parameters?[i].Value}, " : $"`{Parameters?[i].Name}` = {Parameters?[i].Value})");
                        }
                    }
                    else
                        request += (i != (Parameters?.Length - 1) ? $"`{Parameters?[i].Name}` = '{Parameters?[i].Value}', " : $"`{Parameters?[i].Name}` = '{Parameters?[i].Value}'");
                }
                if (Id != 0) request += $" WHERE (Id = {Id})";
                if (WhereClauseParam != null && WhereClauseParam?.Length != 0) request += " WHERE (";
                for (int i = 0; i < WhereClauseParam?.Length; i++)
                {
                    if (WhereClauseParam?[i].ParamType != typeof(char) && WhereClauseParam?[i].ParamType != typeof(string))
                        request += (i != (WhereClauseParam?.Length - 1) ? $" `{WhereClauseParam?[i].Name}` = {WhereClauseParam?[i].Value}, " : $"`{WhereClauseParam?[i].Name}` = {WhereClauseParam?[i].Value})");
                    else if ((string)WhereClauseParam?[i].Value == "not null")
                        request += (i != (WhereClauseParam?.Length - 1) ? $"`{WhereClauseParam?[i].Name}` {WhereClauseParam?[i].Value}, " : $"`{WhereClauseParam?[i].Name}` {WhereClauseParam?[i].Value})");
                    else
                        request += (i != (WhereClauseParam?.Length - 1) ? $"`{WhereClauseParam?[i].Name}` = '{WhereClauseParam?[i].Value}', " : $"`{WhereClauseParam?[i].Name}` ='{WhereClauseParam?[i].Value}')");
                }
                //request += $" ORDERBY {(OrderAsc ? 1 : 0)}";
                return (request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Parameter structure for SQLRequestGenerator
        /// </summary>
        public struct Parameter
        {
            /// <summary>
            /// Name of parameter
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Value of parameter
            /// </summary>
            public object Value { get; set; }
            /// <summary>
            /// Type of paramter
            /// </summary>
            public Type ParamType { get; set; }
        }
    }
}
