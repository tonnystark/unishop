using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniShop.Data.Infrastructure;
using UniShop.Data.Repositories;
using UniShop.Model.Models;

namespace UniShop.Service
{
    public interface IErrorService
    {
        Error CreateError(Error error);
        void Save();
    }
    class ErrorService : IErrorService
    {

        private readonly IErrorRepository _errorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;
        }

        public Error CreateError(Error error)
        {
            return _errorRepository.Add(error);
        }
       

        public void Save()
        {
           _unitOfWork.Commit();
        }
    }
}
