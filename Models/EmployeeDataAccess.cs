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

        public Employees CreateEmployee(Employees employee)
        {
            string sp = "SP_Employees";
            SqlCommand sql = new SqlCommand(sp, DbConnection.Connection);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@action", "CREATE");
            sql.Parameters.AddWithValue("@Name", employee.Name);
            sql.Parameters.AddWithValue("@Email", employee.Email);
            sql.Parameters.AddWithValue("@Mobile", employee.Mobile);
            sql.Parameters.AddWithValue("@Gender", employee.Gender);
            if (DbConnection.Connection.State == ConnectionState.Closed)
            {
                DbConnection.Connection.Open();
            }
            var result =(int) sql.ExecuteScalar();
            DbConnection.Connection.Close();
            if (result == 1)
            {
                return employee;
            }
            else 
            {
                return null;
            }
        }
        public int DeleteEmployee(int id)
        {
            string sp = "SP_Employees";
            SqlCommand sql = new SqlCommand(sp, DbConnection.Connection);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@action", "DELETE");

            sql.Parameters.AddWithValue("@id", id);
            if (DbConnection.Connection.State == ConnectionState.Closed)
            {
                DbConnection.Connection.Open();
            }
            var result = (int)sql.ExecuteScalar();
            DbConnection.Connection.Close();
            if (result == 1)
            {
                return result;
            }
            else
            {
                return 0;
            }
        }


        public void UpdateEmployee(Employees emp)
        {
            string sp = "SP_Employees";
            SqlCommand sql = new SqlCommand(sp, DbConnection.Connection);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@action", "UPDATE");
            sql.Parameters.AddWithValue("@id",emp.Id);
            sql.Parameters.AddWithValue("@Name", emp.Name);
            sql.Parameters.AddWithValue("@Email", emp.Email);
            sql.Parameters.AddWithValue("@Mobile", emp.Mobile);
            sql.Parameters.AddWithValue("@Gender", emp.Gender);
            if (DbConnection.Connection.State == ConnectionState.Closed)
            {
                DbConnection.Connection.Open();
            }
            sql.ExecuteNonQuery();
            DbConnection.Connection.Close();
        }


        public Employees GetEmployeeById(int id)
        {
            string sp = "SP_Employees";
            SqlCommand sql = new SqlCommand(sp, DbConnection.Connection);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@action", "SELECT_SINGLE");
            sql.Parameters.AddWithValue("@id", id);
            if (DbConnection.Connection.State == ConnectionState.Closed)
            {
                DbConnection.Connection.Open();
            }
            SqlDataReader dr = sql.ExecuteReader();
                Employees Emp = new Employees();
            while (dr.Read()) { 
                Emp.Id = (int)dr["id"];
                Emp.Name = dr["Name"].ToString();
                Emp.Email = dr["Email"].ToString();
                Emp.Mobile = dr["Mobile"].ToString();
                Emp.Gender = dr["Gender"].ToString();
            }
            DbConnection.Connection.Close();
            return Emp;
        }




        public List<Employees> GetEmployees()
        {
            string sp = "SP_Employees";
            SqlCommand sql = new SqlCommand(sp,DbConnection.Connection);
            sql.CommandType = CommandType.StoredProcedure;
            //sql.Parameters.AddWithValue("@action", "SELECT_JOIN");
            sql.Parameters.AddWithValue("@action", "SELECT");
            if (DbConnection.Connection.State==ConnectionState.Closed)
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
                employees.Add(Emp);
            }
            DbConnection.Connection.Close();
            return employees;
        }
        
    }
}
