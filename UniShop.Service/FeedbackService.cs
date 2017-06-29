using UniShop.Data.Infrastructure;
using UniShop.Data.Repositories;
using UniShop.Model.Models;

namespace UniShop.Service
{
    public interface IFeedbackService
    {
        FeedBack Create(FeedBack feedBack);
        void Save();
    }
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackService(IFeedBackRepository feedBackRepository, IUnitOfWork unitOfWork)
        {
            _feedBackRepository = feedBackRepository;
            _unitOfWork = unitOfWork;
        }

        public FeedBack Create(FeedBack feedBack)
        {
            return _feedBackRepository.Add(feedBack);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
