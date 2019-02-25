using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Collections.Generic;
using RequestApprovalWorkflow.Host.Models;

namespace RequestApprovalWorkflow.Host.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                List<RequestViewModel> requestViewModelList = new List<RequestViewModel>();

                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    ApplicationUser user = context.Users.Where(u => u.Id == userId).FirstOrDefault();

                    if (user != null && user.DesignationId == 1)
                    {
                        var result = await context.UserRequest.Where(x => x.UserId == userId || x.User.DepartmentId == user.DepartmentId).ToListAsync();

                        if (result != null)
                        {
                            foreach (var item in result)
                            {
                                RequestViewModel requestViewModel = new RequestViewModel();
                                requestViewModel.Id = item.Request.Id;
                                requestViewModel.Name = item.Request.Name;
                                requestViewModel.Description = item.Request.Description;
                                requestViewModel.CreateDate = item.Request.CreateDate;
                                requestViewModel.RequstStatusId = item.Request.RequstStatusId;
                                requestViewModel.RequstStatus = item.Request.Status.Name;
                                requestViewModel.StatusDate = item.Request.StatusDate;
                                requestViewModel.UserId = item.User.Id;
                                requestViewModel.DepartmentId = item.User.DepartmentId;
                                requestViewModel.DesignationId = item.User.DesignationId;
                                requestViewModel.LoggedInUserDesignationId = user.DesignationId;

                                requestViewModelList.Add(requestViewModel);
                            }
                        }
                    }
                    else
                    {
                        var result = await context.UserRequest.Where(x => x.UserId == userId).ToListAsync();

                        if (result != null)
                        {
                            foreach (var item in result)
                            {
                                RequestViewModel requestViewModel = new RequestViewModel();
                                requestViewModel.Id = item.Request.Id;
                                requestViewModel.Name = item.Request.Name;
                                requestViewModel.Description = item.Request.Description;
                                requestViewModel.CreateDate = item.Request.CreateDate;
                                requestViewModel.RequstStatusId = item.Request.RequstStatusId;
                                requestViewModel.RequstStatus = item.Request.Status.Name;
                                requestViewModel.StatusDate = item.Request.StatusDate;
                                requestViewModel.UserId = item.User.Id;
                                requestViewModel.DepartmentId = item.User.DepartmentId;
                                requestViewModel.DesignationId = item.User.DesignationId;
                                requestViewModel.LoggedInUserDesignationId = user.DesignationId;

                                requestViewModelList.Add(requestViewModel);
                            }
                        }
                    }
                }

                return View(requestViewModelList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Requests model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string userId = User.Identity.GetUserId();

                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {

                        ApplicationUser currentUser = context.Users.Where(u => u.Id == userId).FirstOrDefault();

                        model.CreateDate = DateTime.Now;

                        model.RequstStatusId = currentUser.DesignationId == 1 ? 2 : 1;
                        model.Status = context.RequestsStatus.Where(r => r.Id == model.RequstStatusId).FirstOrDefault();

                        model.StatusDate = DateTime.Now;

                        context.Request.Add(model);
                        int requestResult = await context.SaveChangesAsync();

                        if (requestResult > 0)
                        {
                            UserRequests userRequests = new UserRequests();
                            userRequests.UserId = User.Identity.GetUserId();
                            userRequests.RequestId = context.Request.Where(r => r.Name == model.Name && r.Description == model.Description).Select(r => r.Id).FirstOrDefault();

                            context.UserRequest.Add(userRequests);
                            int userRequestResult = await context.SaveChangesAsync();
                            if (userRequestResult > 0)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return View();
        }

        public async Task<ActionResult> Approve(int id)
        {
            if (ModelState.IsValid)
            {
                if (id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var requestModel = context.Request.Where(r => r.Id == id).FirstOrDefault();

                    if (requestModel == null)
                    {
                        return HttpNotFound();
                    }

                    requestModel.RequstStatusId = 2;
                    requestModel.StatusDate = DateTime.Now;

                    int result = await context.SaveChangesAsync();

                    if (result < 1)
                    {
                        ModelState.AddModelError("", "Request Cannot be approved.");
                        return View();
                    }

                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public async Task<ActionResult> Reject(int id)
        {
            if (ModelState.IsValid)
            {
                if (id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var requestModel = context.Request.Where(r => r.Id == id).FirstOrDefault();

                    if (requestModel == null)
                    {
                        return HttpNotFound();
                    }

                    requestModel.RequstStatusId = 3;
                    requestModel.StatusDate = DateTime.Now;

                    int result = await context.SaveChangesAsync();

                    if (result < 1)
                    {
                        ModelState.AddModelError("", "Request Cannot be approved.");
                        return View();
                    }

                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}