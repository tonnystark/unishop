using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using UniShop.Common.Exceptions;
using UniShop.Model.Models;
using UniShop.Service;
using UniShop.Web.App_Start;
using UniShop.Web.Infrastructure.Core;
using UniShop.Web.Infrastructure.Extensions;
using UniShop.Web.Models;

namespace UniShop.Web.Api
{
    [Authorize]
    [RoutePrefix("api/applicationUser")]
    public class ApplicationUserController : ApiControllerBase
    {
        private readonly IApplicationGroupService _appGroupService;
        private readonly IApplicationRoleService _appRoleService;
        private readonly ApplicationUserManager _userManager;

        public ApplicationUserController(
            IApplicationGroupService appGroupService,
            IApplicationRoleService appRoleService,
            ApplicationUserManager userManager,
            IErrorService errorService)
            : base(errorService)
        {
            _appRoleService = appRoleService;
            _appGroupService = appGroupService;
            _userManager = userManager;
        }

        [Route("getlistpaging")]
        [HttpGet]
        [Authorize(Roles = "ViewUser")]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize,
            string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var totalRow = 0;
                var model = _userManager.Users;
                var modelVm = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(model);

                var pagedSet = new PaginationSet<ApplicationUserViewModel>
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int) Math.Ceiling((decimal) totalRow/pageSize),
                    Items = modelVm
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [Route("detail/{id}")]
        [HttpGet]
        [Authorize(Roles = "ViewUser")]
        public HttpResponseMessage Details(HttpRequestMessage request, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có giá trị.");
            }
            var user = _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu");
            }
            var applicationUserViewModel = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user.Result);
            var listGroup = _appGroupService.GetListGroupByUserId(applicationUserViewModel.Id);
            applicationUserViewModel.Groups =
                Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(listGroup);
            return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "AddUser")]
        public async Task<HttpResponseMessage> Create(HttpRequestMessage request,
            ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppUser = new ApplicationUser();
                newAppUser.UpdateUser(applicationUserViewModel);
                try
                {
                    newAppUser.Id = Guid.NewGuid().ToString();
                    var result = await _userManager.CreateAsync(newAppUser, applicationUserViewModel.Password);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<ApplicationUserGroup>();
                        foreach (var group in applicationUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new ApplicationUserGroup
                            {
                                GroupId = group.ID,
                                UserId = newAppUser.Id
                            });
                            //add role to user
                            var listRole = _appRoleService.GetListRoleByGroupId(group.ID);

                            foreach (var role in listRole)
                            {
                                await _userManager.RemoveFromRoleAsync(newAppUser.Id, role.Name);
                                await _userManager.AddToRoleAsync(newAppUser.Id, role.Name);
                            }
                        }
                        _appGroupService.AddUserToGroups(listAppUserGroup, newAppUser.Id);
                        _appGroupService.Save();


                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
                    }
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "UpdateUser")]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request,
            ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByIdAsync(applicationUserViewModel.Id);
                try
                {
                    appUser.UpdateUser(applicationUserViewModel);
                    var result = await _userManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<ApplicationUserGroup>();

                        //remove all old role 
                        var roles = await _userManager.GetRolesAsync(appUser.Id);
                        if (roles.Count > 0)
                            await _userManager.RemoveFromRolesAsync(appUser.Id, roles.ToArray());

                        foreach (var group in applicationUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new ApplicationUserGroup
                            {
                                GroupId = group.ID,
                                UserId = applicationUserViewModel.Id
                            });
                            //add role to user
                            var listRole = _appRoleService.GetListRoleByGroupId(group.ID);
                            foreach (var role in listRole)
                            {
                                await _userManager.RemoveFromRoleAsync(appUser.Id, role.Name);
                                await _userManager.AddToRoleAsync(appUser.Id, role.Name);
                            }
                        }
                        _appGroupService.AddUserToGroups(listAppUserGroup, applicationUserViewModel.Id);
                        _appGroupService.Save();
                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
                    }
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "DeleteUser")]
        public async Task<HttpResponseMessage> Delete(HttpRequestMessage request, string id)
        {
            try
            {
                //remove all old role from user
                var roles = await _userManager.GetRolesAsync(id);
                if (roles.Count > 0)
                    await _userManager.RemoveFromRolesAsync(id, roles.ToArray());

                var appUser = await _userManager.FindByIdAsync(id);
                var result = await _userManager.DeleteAsync(appUser);
                if (result.Succeeded)
                    return request.CreateResponse(HttpStatusCode.OK, id);
                return request.CreateErrorResponse(HttpStatusCode.OK, string.Join(",", result.Errors));
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}