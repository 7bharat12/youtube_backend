using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApplication2.models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public userController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from userDetails";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("userCon");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);


                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet("{userName}")]
        public JsonResult Get(string userName)
        {
            string query = @"select * from userDetails where userName = '" + userName + "'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("userCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);


                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(user newUser)
        {
            string query = "insert into userDetails values('"+newUser.userName+"','"+newUser.pwd+"','"+newUser.email+"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("userCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    try
                    {
                        myReader = myCommand.ExecuteReader();
                    } catch (Exception e) { return new JsonResult("Username already taken!"); }
                    table.Load(myReader);


                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added succesfully!");
        }
        [HttpPut]
        public JsonResult Put(user newUser)
        {
            string query = "update userDetails set userName ='" + newUser.userName + "',email = '" + newUser.email + "' where id= "+newUser.id+"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("userCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);


                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("{id:'" +newUser.id + "', userName:'" +newUser.userName+"', email:'" + newUser.email+ "'}");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = "delete from userDetails where id= " + id + "";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("userCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);


                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted succesfully!");
        }
    }
}
