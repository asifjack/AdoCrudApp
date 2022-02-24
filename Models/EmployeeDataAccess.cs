using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
using System.Data;

namespace AdoCrudApp.Models
{
    public class EmployeeDataAccess
    {
        DBConnection DbConnection;
        public EmployeeDataAccess()
        {
            DbConnection = new DBConnection();
        }
        public List<Employees> GetEmployees()
        {
            string sp = "SP_Employee";
            SqlCommand sql = new SqlCommand(sp,DbConnection.Connection);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@action", "SELECT_JOIN");
            if(DbConnection.Connection.State==ConnectionState.Closed)
            {   
                DbConnection.Connection.Open();
            }
            SqlDataReader dr = sql.ExecuteReader();
            List<Employees> employees = new List<Employees>();
            while (dr.Read())
            {
                Employees Emp = new Employees();
                Emp.Id =(int)dr["id"];
                Emp.Name = dr["Name"].ToString();
                Emp.Email = dr["Email"].ToString();
                Emp.Mobile = dr["Mobile"].ToString();
                Emp.Gender = dr["Gender"].ToString();
                Emp.DName = dr["Department"].ToString();
                employees.Add(Emp);
            }
            DbConnection.Connection.Close();
            return employees;


        }

    }
}
