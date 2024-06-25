using Dapper;
using DapperCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DapperCRUD.DAL
{
    public class DataAccess
    {
        private readonly IDbConnection db = new SqlConnection(CnSetting.CnMain);
        Securty Sc = new Securty();
        public IEnumerable<Employee> GetEmpInfo(int procid, Employee Models)
        {
            var Password = "";
            if (procid==1 || procid == 2)
            {
                Password = Sc.EncodePasswordToBase64(Models.UserPassword).ToString();
            }
            var parameters = new DynamicParameters();
            parameters.Add("@userid", Models.userid == null ? 0 : Models.userid);
            parameters.Add("@UserName", Models.UserName ?? string.Empty);
            parameters.Add("@FathName", Models.FathName ?? string.Empty);
            parameters.Add("@MobileNo", Models.MobileNo ?? string.Empty);
            parameters.Add("@email", Models.email ?? string.Empty);
            parameters.Add("@DOB", DateTime.ParseExact(Models.DOB == null ? "1900-01-01" : Models.DOB, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
            parameters.Add("@UserProfile", Models.UserProfile ?? string.Empty);
            parameters.Add("@UserPassword", Password ?? string.Empty);
            parameters.Add("@RoleId", Models.RoleId ?? string.Empty);
            parameters.Add("@flag", procid);
            var Query = @"Sp_EmployeeReg";
            return db.Query<Employee>(Query, parameters, commandType: CommandType.StoredProcedure).ToList();
        }
        public IEnumerable<UserLogin> GetuserLogin(UserLogin M)
        {
            var Password= Sc.EncodePasswordToBase64(M.password).ToString();
            var Query = @"Select userid, UserName,RoleId from Employee  where userid='" + M.userid + "' and UserPassword='" + Password + "'";
            return db.Query<UserLogin>(Query).ToList();
        }

    }
}