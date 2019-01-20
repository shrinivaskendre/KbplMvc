using KbplMvc.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace KbplMvc.Controllers
{
    public class MaterialIssueSlipController : Controller
    {
        //
        // GET: /MaterialIssueSlip/

        string strConnection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
      
        DataTable dt = new DataTable();
        OracleCommand cmd = new OracleCommand();

        public ActionResult Index()
        {
            // ViewBag.userdetails = BindInitialData();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            Materialissueslip materialisueslips = new Materialissueslip();
            materialisueslips.ListFactory = BindFactory();
            // materialisueslips.ListCostCenter = BindCostCenter();
            return View(materialisueslips);
        }

        [HttpPost]
        public ActionResult Create(FormCollection objForm)
        {
            Materialissueslip materialisuslip = new Materialissueslip();
            materialisuslip.IssueNo = Convert.ToInt32(objForm["name"]);
            materialisuslip.IssueDate = Convert.ToDateTime(objForm["address"]);
            materialisuslip.ListFactory = BindFactory();
            //materialisuslip.ListCostCenter = BindCostCenter();
            materialisuslip.PersonName = Convert.ToString(objForm["name"]);
            materialisuslip.Remark = Convert.ToString(objForm["name"]);
            // res = edu.InsertEducationDAL(edu);
            return View();
        }

        [NonAction]
        public List<SelectListItem> BindFactory()
        {
            List<SelectListItem> factory = new List<SelectListItem>(){ 
            //new SelectListItem { Text="Select", Value="0", Selected=true},
            new SelectListItem {Text="SINNAR", Value="1", Selected=false},
            new SelectListItem {Text="DIWAS", Value="2", Selected=false}
            };

            // ViewBag.userdetails = BindInitialData();
            return factory;
        }


        public JsonResult BindCostCenter(string schemeId)
        {
            List<SelectListItem> costcenter = new List<SelectListItem>(){ 
            //new SelectListItem { Text="Select", Value="0", Selected=true},
            new SelectListItem {Text="KBPL BESAN(SINNER)", Value="1", Selected=false},
            new SelectListItem {Text="KBPL COMMON(SINNER)", Value="2", Selected=false},
            new SelectListItem {Text="KBPL FLOUR(SINNER)", Value="2", Selected=false}
            };
            return Json(costcenter, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult BindItemDetails(string schemeId)
        //{
        //    List<Materialissueslip> items = new List<Materialissueslip>(){ 
        //           new Materialissueslip {itemDesc="KBPL BESAN", itemCode="1"},
        //    new Materialissueslip {itemDesc="KBPL COMMON", itemCode="2"},
        //    new Materialissueslip {itemDesc="KBPL FLOUR", itemCode="3"} 
        //    };

        //    ViewBag.userdetails = BindInitialData();
        //    return Json(items, JsonRequestBehavior.AllowGet);
        //}

        private JsonResult BindInitialData()
        {
            List<DTMaterialIssueSlipEntity> objDTMaterialIssueEntity = new List<DTMaterialIssueSlipEntity>();
            for (int i = 0; i < 5; i++)
                objDTMaterialIssueEntity.Add(new DTMaterialIssueSlipEntity
                {
                    Itemcode = "001",
                    Itemdesc = "abc",
                    Unit = "0",
                    AvailableQty = 0,
                    IssuedQty = 0,
                    IssuePurpose = "Testing",
                });
            //ViewBag.userdetails = objDTMaterialIssueEntity;

            return Json(objDTMaterialIssueEntity, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetFactory()
        {
            OracleConnection cn = new OracleConnection(strConnection);
            cmd.CommandText = "makess.PKG_USERMASTER.GetLocationByUserComp";
            #region Parameters
            cmd.Parameters.Add("CompCode", OracleDbType.Varchar2).Value = "01";
            cmd.Parameters.Add("useCode", OracleDbType.Varchar2, ParameterDirection.Input).Value = "44";
            cmd.Parameters.Add("refcursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            if (cn.State == ConnectionState.Open) { cn.Close(); } if (cn.State == ConnectionState.Open) { cn.Close(); } cn.Open();
                da.Fill(dt);
                cn.Close();
            #endregion

                List<Factory> listfactory = new List<Factory>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Factory factory = new Factory();
                    factory.fId = Convert.ToInt32(dt.Rows[i]["VC_COMP_CODE"]);
                    factory.fName = Convert.ToString(dt.Rows[i]["VC_MAIN_LOC_NAME"]);
                    listfactory.Add(factory);
                }

            //List<Factory> listfactory = new List<Factory>()
            //{ 
            //new Factory {fName="SINNAR", fId=1},
            //new Factory {fName="DIWAS", fId=2}
            //};
            return Json(listfactory, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCostCenter(string fid)
        {
            OracleConnection cn = new OracleConnection(strConnection);
            cmd.CommandText = "Finance.Pkg_Costcenter.getallcostcenterbyLocCompCode";
            #region Parameters
            cmd.Parameters.Add("compcode", OracleDbType.Varchar2).Value = "01";
            cmd.Parameters.Add("mainloccode", OracleDbType.Varchar2).Value = "1";
            cmd.Parameters.Add("refcursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            if (cn.State == ConnectionState.Open) { cn.Close(); } if (cn.State == ConnectionState.Open) { cn.Close(); } cn.Open();
            da.Fill(dt);
            cn.Close();
            #endregion

            List<COstCenter> costcenter = new List<COstCenter>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                COstCenter costName = new COstCenter();
                costName.cId = Convert.ToInt32(dt.Rows[i]["VC_COMP_CODE"]);
                costName.cName = Convert.ToString(dt.Rows[i]["VC_DESCRIPTION"]);
                costcenter.Add(costName);
            }

            //List<COstCenter> costcenter = new List<COstCenter>(){ 
            //new COstCenter {cName="KBPL BESAN(SINNER)", cId=1},
            //new COstCenter {cName="KBPL COMMON(SINNER)", cId=2},
            //new COstCenter {cName="KBPL FLOUR(SINNER)", cId=1}
            //};
            return Json(costcenter, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindDataTable()
        {
            List<ItemDetails> listdt = new List<ItemDetails>();
            for (int i = 0; i < 5; i++)
                listdt.Add(new ItemDetails
                {
                    ItemName = string.Empty,
                    itemCode = string.Empty,
                    ItemDescription = "abc",
                    unit = string.Empty,
                    availQty = 0,
                    issueQty = 0,
                    issuePurpose = string.Empty,
                });
            return Json(listdt, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddItem(Materialissueslip person)        //(string name,string Address,string Salary,string Dept)
        {

            //Education person = new Education
            //{
            //    name = name,
            //    address=Address,
            //    salary = Salary,
            //    dept=Dept
            //   //DateTime DateTime = DateTime.Now.ToString()
            //};
           // person.InsertEducationDAL(person);
            return Json(person);
        }

        public JsonResult GetAllItemsListByFactory()
        {
            OracleConnection cn = new OracleConnection(strConnection);
            string SPName = "invent.Pkg_Materialissueslip.getallitembycompcodemainloc";
            cmd.CommandText = SPName;
            #region Parameters
            cmd.Parameters.Add("compcode", OracleDbType.NVarchar2).Value = "01";
            cmd.Parameters.Add("subloccode", OracleDbType.NVarchar2).Value = "1";
            cmd.Parameters.Add("refcursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            if (cn.State == ConnectionState.Open) { cn.Close(); } if (cn.State == ConnectionState.Open) { cn.Close(); } cn.Open();
            da.Fill(dt);
            cn.Close();
            #endregion

            List<ItemDetails> lstItem = new List<ItemDetails>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ItemDetails item = new ItemDetails();
                item.itemCode = Convert.ToString(dt.Rows[i]["VC_ITEM_CODE"]);
                item.ItemName = Convert.ToString(dt.Rows[i]["VC_ITEM_DESC"]);
                item.unit = Convert.ToString(dt.Rows[i]["VC_UNIT"]);
                item.availQty = Convert.ToDouble(dt.Rows[i]["NU_QTY_AVAILABLE"]);
                item.issueQty = 0.00;
                item.issuePurpose = string.Empty;
                lstItem.Add(item);
            }
            return Json(lstItem, JsonRequestBehavior.AllowGet);
        }

    }
}
