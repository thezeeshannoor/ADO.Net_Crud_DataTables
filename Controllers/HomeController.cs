using CrudWithDataTablesUsingAdo.net.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CrudWithDataTablesUsingAdo.net.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        string query;
        public SqlDataAdapter adapter;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public  JsonResult Indexx()
        {
            try
            {
                var start = Convert.ToInt32(Request.Form["start"]);
                var pageSize = Convert.ToInt32(Request.Form["length"]);
                var pageNumber = start / pageSize + 1; ;
                string searchKeyword = Request.Form["search[value]"];

                //sorting
                string sortDirection = Request.Form["order[0][dir]"];
                string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"] + "][data]"];
                //sortDirection ??= "asc";
                //sortColumn ??= "fname";
                query = "GetStudentS";
                adapter = new SqlDataAdapter(query, DbCon.GetCon());
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@PageSize", pageSize);
                adapter.SelectCommand.Parameters.AddWithValue("@PageNumber", pageNumber);
                adapter.SelectCommand.Parameters.AddWithValue("@SearchKeyword", searchKeyword);
                adapter.SelectCommand.Parameters.AddWithValue("@sortColumn", sortColumn);
                adapter.SelectCommand.Parameters.AddWithValue("@sortDirection", sortDirection);


                DataSet ds = new DataSet();
                adapter.Fill(ds);

                List<Student> dataa = new List<Student>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Student std = new Student();
                    std.id = Convert.ToInt32(row[0]);
                    std.fname = row[1].ToString();
                    std.lname = row[5].ToString();
                    std.clas = row[2].ToString();
                    std.contact = row[4].ToString();
                    std.address = row[3].ToString();

                    dataa.Add(std);
                }

                return Json(new
                {
                    draw = Convert.ToInt32(Request.Form["draw"]), 
                    recordsTotal = dataa.Count,
                    recordsFiltered = dataa.Count,
                    data = dataa
                });
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [HttpPost]
        public IActionResult UpdateField(string val, string fieldName,int id)
        {
            query = "UpdateField";
            adapter = new SqlDataAdapter(query, DbCon.GetCon());
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@val", val);
            adapter.SelectCommand.Parameters.AddWithValue("@fieldName", fieldName);
            adapter.SelectCommand.Parameters.AddWithValue("@id", id);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return Json(new {success=true});

        }
        [HttpPost]
        public IActionResult DeleteField(int id)
        {
            query = "DeleteField";
            adapter = new SqlDataAdapter(query, DbCon.GetCon());
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@id", id);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return Json(new { success = true });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
