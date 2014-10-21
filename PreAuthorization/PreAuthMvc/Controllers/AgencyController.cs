using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PreAuthMvc.Models;
using Newtonsoft.Json;
using Ralph.CommonLib.NHibernate;
using Ralph.CommonLib.Dao;
using PreAuthMvc.Common;
using log4net;
using PreAuthEntityLib.Vo;

namespace PreAuthMvc.Controllers
{
   
    /// <summary>
    /// 机构信息
    /// </summary>
    public class AgencyController : BaseController
    {

        private int totalCount = 0;

        private static ILog _log = LogManager.GetLogger(ConstValues.LogName);
        // GET: Agency
        public ActionResult Index(int pageNo = 1)
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var dao = new BaseDao<AgencyVo, long>(sessionHelper);
                var rVoList = dao.LoadPage(pageNo, ConstValues.PageSize, ref totalCount);
                var rList = (from p in rVoList
                             select new AgencyViewModel
                             {
                                 Id = p.Id,
                                 AgencyCode = p.AgencyCode,
                                 AgencyName = p.AgencyName
                             }).ToList();
                PreAuthUtil.SetPageInfo(ViewData, pageNo, totalCount);
                return View(rList);
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }

        }
        /// <summary>
        /// 跳转到增加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View(new AgencyViewModel());
        }
        /// <summary>
        /// 增加页面提交
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitAdd()
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                AgencyVo vo;
                var dao = new BaseDao<AgencyVo, long>(sessionHelper);
                var recordId = Request.Params["recordId"];
                if (!string.IsNullOrEmpty(recordId) && !"0".Equals(recordId))
                {
                    vo = dao.LoadById(Convert.ToInt64(recordId));
                }
                else
                {
                    vo = new AgencyVo();
                }

                vo.AgencyCode = Request.Params["AgencyCode"];
                vo.AgencyName = Request.Params["AgencyName"];
                
                dao.SaveOrUpdateIndTrans(vo);

                ViewData["RedirectAction"] = "Index";
                ViewData["Controller"] = "Agency";
                return PartialView("~/Views/Shared/CommonSuccess.cshtml");
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }

        }
        public string Del()
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var dao = new BaseDao<AgencyVo, long>(sessionHelper);
                if (!string.IsNullOrEmpty(Request.Params["recordId"]))
                {
                    var recordId = Convert.ToInt64(Request.Params["recordId"]);
                    dao.DeleteByIdIndTrans(recordId);
                }
                var r = new
                {
                    jsonFlag = true,
                    jsonMsg = "Del Success"

                };
                return JsonConvert.SerializeObject(r);
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }
        }
        public ActionResult Modify()
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var recordId = Request.Params["recordId"];
                var dao = new BaseDao<AgencyVo, long>(sessionHelper);
                var rVo = dao.LoadById(Convert.ToInt64(recordId));
                var rVm = new AgencyViewModel()
                {
                    Id = rVo.Id,
                    AgencyCode = rVo.AgencyCode,
                    AgencyName = rVo.AgencyName
                };
                ViewData["listHeader"] = "修改机构";
                return View("Add", rVm);
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }
        }

        public ActionResult DictIndex(int pageNo=1)
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var dao = new BaseDao<DictItemVo, long>(sessionHelper);
                var rVoList = dao.LoadPage(pageNo, ConstValues.PageSize, ref totalCount);
                var rList = (from p in rVoList
                             select new DictItemViewModel
                             {
                                 Id = p.Id,
                                 ItemCode = p.ItemCode,
                                 ItemName = p.ItemName,
                                 ItemType = p.ItemType,
                                 ParentCode = p.ParentCode
                             }).ToList();
                PreAuthUtil.SetPageInfo(ViewData, pageNo, totalCount);
                return View(rList);
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }
        }
        /// <summary>
        /// 跳转到增加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DictAdd()
        {
            
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var dao = new BaseDao<DictItemVo, long>(sessionHelper);

                var parentList = (from p in dao.LoadAll()
                                  select new SelectListItem()
                                  {
                                      Value = p.ItemCode,
                                      Text = p.ItemName
                                  }).ToList();
                parentList.Insert(0, new SelectListItem()
                {
                    Selected = true,
                    Value = "0",
                    Text = @"---请选择---"
                });

                ViewData["parentList"] = parentList;
                return View(new DictItemViewModel());
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }
        }
        /// <summary>
        /// 增加页面提交
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitDictAdd()
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                DictItemVo vo;
                var dao = new BaseDao<DictItemVo, long>(sessionHelper);
                var recordId = Request.Params["recordId"];
                if (!string.IsNullOrEmpty(recordId) && !"0".Equals(recordId))
                {
                    vo = dao.LoadById(Convert.ToInt64(recordId));
                }
                else
                {
                    vo = new DictItemVo();
                }

                vo.ItemCode = Request.Params["ItemCode"];
                vo.ItemName = Request.Params["ItemName"];
                vo.ItemType = Request.Params["ItemType"];
                vo.ParentCode = Request.Params["ParentCode"];
                vo.Remark = Request.Params["Remark"];
                dao.SaveOrUpdateIndTrans(vo);

                ViewData["RedirectAction"] = "DictIndex";
                ViewData["Controller"] = "Agency";
                return PartialView("~/Views/Shared/CommonSuccess.cshtml");
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }

        }
        public string DictDel()
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var dao = new BaseDao<DictItemVo, long>(sessionHelper);
                if (!string.IsNullOrEmpty(Request.Params["recordId"]))
                {
                    var recordId = Convert.ToInt64(Request.Params["recordId"]);
                    dao.DeleteByIdIndTrans(recordId);
                }
                var r = new
                {
                    jsonFlag = true,
                    jsonMsg = "Del Success"

                };
                return JsonConvert.SerializeObject(r);
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }
        }
        public ActionResult DictModify()
        {
            SessionHelper sessionHelper = new SessionHelper();
            try
            {
                var recordId = Request.Params["recordId"];
                var dao = new BaseDao<DictItemVo, long>(sessionHelper);
                var rVo = dao.LoadById(Convert.ToInt64(recordId));
                var rVm = new DictItemViewModel()
                {
                    Id = rVo.Id,
                    ItemCode = rVo.ItemCode,
                    ItemName = rVo.ItemName,
                    ItemType=rVo.ItemType,
                    ParentCode = rVo.ParentCode
                };

                var dictDao = new BaseDao<DictItemVo, long>(sessionHelper);
                var parentList = (from p in dictDao.LoadAll()
                    select new SelectListItem()
                    {
                        Value = p.ItemCode,
                        Text = p.ItemName
                    }).ToList();
                parentList.Insert(0,new SelectListItem()
                {
                    Selected = true,
                    Value="0",
                    Text = @"---请选择---"
                });

                ViewData["parentList"] = parentList;
                return View("DictAdd", rVm);
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
                throw ex;
            }
            finally
            {
                sessionHelper.CloseSession();
            }
        }
    }
}