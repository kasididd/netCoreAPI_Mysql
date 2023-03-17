using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using netCoreAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace netCoreAPI.Controllers;

[Route("api")]
//[Route("api/[controller]")]
public class StudentController : Controller
{
    static Student s1 = new Student { name = "sd", id = 1, grade = 3.3 };
    static Student s2 = new Student { name = "new", id = 2, grade = 2.3 };
    //List<Student> = new List<Student>(){s1 ,s2 };
     List<Student> listStudents = new List<Student>() { s1,s2 };
    // GET: api/values
    static string server = "localhost";
    static string database = "testdb";
    static string username = "root";
    static string password = "";
    static string constShort = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={password}";
    MySqlConnection conn = new MySqlConnection(constShort);
    // GET: api/values
    [HttpGet]
    public List<Student> Get()
    {
        conn.Open();
        List<Student> result = new();
        string query = "select * from students";
        MySqlDataReader cmd = new MySqlCommand(query, conn).ExecuteReader();
        while (cmd.Read())
        {
            result.Add(new Student { id = int.Parse(cmd["id"].ToString()??"0"), name = cmd["name"].ToString() ,grade = double.Parse(cmd["grade"].ToString()??"0") });
            //Student val = new() { name="kasidid"};
        }
        return result;

    }

    // GET api/values/5
    [HttpGet("{id}")]
    public List<Student> Get(int id)
    {
        conn.Open();
        List<Student> result = new();
        string query = $"select * from students where id = {id}";
        MySqlDataReader cmd = new MySqlCommand(query, conn).ExecuteReader();
        while (cmd.Read())
        {
            result.Add(new Student { id = int.Parse(cmd["id"].ToString() ?? "0"), name = cmd["name"].ToString(), grade = double.Parse(cmd["grade"].ToString() ?? "0") });
            //Student val = new() { name="kasidid"};
        }
        return result;

    }

    // POST api/values
    [HttpPost]
    public String Post([FromBody]Student value)
    {
        conn.Open();
        string query = $"insert into   students (`id`, `name`, `grade`) values ({value.id}, '{value.name}', {value.grade})";
        MySqlDataReader cmd = new MySqlCommand(query, conn).ExecuteReader();
        
        return "insert  success";
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public String Put(int id, [FromBody]Student value)
    {
        conn.Open();
        List<Student> result = new();
        string query = $"UPDATE `students` SET id = {value.id}, name = '{value.name}', grade = {value.grade} WHERE id = {id}";
        MySqlDataReader cmd = new MySqlCommand(query, conn).ExecuteReader();
        while (cmd.Read())
        {
            result.Add(new Student { id = int.Parse(cmd["id"].ToString() ?? "0"), name = cmd["name"].ToString(), grade = double.Parse(cmd["grade"].ToString() ?? "0") });
            //Student val = new() { name="kasidid"};
        }
        return $"update success at id = {id}";
    }
    
    // DELETE api/values/5
    [HttpDelete("{id}")]
    public String Delete(int id)
    {
        conn.Open();
        List<Student> result = new();
        string query = $"DELETE FROM `students` WHERE  id = {id}";
        MySqlDataReader cmd = new MySqlCommand(query, conn).ExecuteReader();
        while (cmd.Read())
        {
            result.Add(new Student { id = int.Parse(cmd["id"].ToString() ?? "0"), name = cmd["name"].ToString(), grade = double.Parse(cmd["grade"].ToString() ?? "0") });
            //Student val = new() { name="kasidid"};
        }
        return $"delete at id = {id} sucess";
    }
}

