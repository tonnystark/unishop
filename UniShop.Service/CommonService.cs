using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniShop.Common;
using UniShop.Data.Infrastructure;
using UniShop.Data.Repositories;
using UniShop.Model.Models;

namespace UniShop.Service
{
    public interface ICommonService
    {
        Footer GetFooter();
        IEnumerable<Slide> GetSlides();
        SystemConfig GetSystemConfig(string code);
    }
    public class CommonService : ICommonService
    {
        private readonly IFooterRepository _footerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private ISlideRepository _slideRepository;
        private ISystemConfigRepository _systemConfigRepository;
        public CommonService(IFooterRepository footerRepository, IUnitOfWork unitOfWork, ISlideRepository slideRepository, ISystemConfigRepository systemConfigRepository)
        {
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
            _slideRepository = slideRepository;
            _systemConfigRepository = systemConfigRepository;
        }
        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.DefaultFooterId);
        }

        public IEnumerable<Slide> GetSlides()
        {
            return _slideRepository.GetMulti(x => x.Status);
        }

        public SystemConfig GetSystemConfig(string code)
        {
            return _systemConfigRepository.GetSingleByCondition(x => x.Code == code);
        }
    }
}
