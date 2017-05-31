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

    public class ErrorService : IErrorService
    {
        private readonly IErrorRepository _errorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            _errorRepository = errorRepository;
            _unitOfWork = unitOfWork;
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