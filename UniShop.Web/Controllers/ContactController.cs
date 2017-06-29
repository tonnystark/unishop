using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BotDetect.Web.Mvc;
using UniShop.Common;
using UniShop.Model.Models;
using UniShop.Service;
using UniShop.Web.Infrastructure.Extensions;
using UniShop.Web.Models;

namespace UniShop.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactDetailService _contactDetailService;
        private IFeedbackService _feedbackService;

        public ContactController(IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            _contactDetailService = contactDetailService;
            _feedbackService = feedbackService;
        }
        // GET: Contact
        public ActionResult Index()
        {

            FeedBackViewModel viewModel = new FeedBackViewModel();
            viewModel.ContactDetail = GetContactDetailViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "contactCaptcha", "Mã CAPTCHA không đúng!")]
        public ActionResult SendFeedback(FeedBackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                FeedBack newFeedback = new FeedBack();
                newFeedback.UpdateFeedBack(feedbackViewModel);
                _feedbackService.Create(newFeedback);
                _feedbackService.Save();

                ViewData["SuccessMsg"] = "Gửi phản hồi thành công";

                var adminEmail = ConfigHelper.GetValueByKey("AdminEmail");
                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/contact_template.html"));
                content = content.Replace("{{Name}}", feedbackViewModel.Name);
                content = content.Replace("{{Web}}", "http://localhost:11673/");
                content = content.Replace("{{mailTo}}", feedbackViewModel.Email);
                content = content.Replace("{{adminMail}}", adminEmail);
                // gửi mail
                MailHelper.SendMail(feedbackViewModel.Email, "Thông tin liên hệ từ website", content);

                feedbackViewModel.Name = "";
                feedbackViewModel.Message = "";
                feedbackViewModel.Email = "";
            }

            feedbackViewModel.ContactDetail = GetContactDetailViewModel();

            return View("Index", feedbackViewModel);
        }

        public ContactDetailViewModel GetContactDetailViewModel()
        {
            var model = _contactDetailService.GetContactDetail();
            var contactDetailViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);

            return contactDetailViewModel;
        }
    }
}