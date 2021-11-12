using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;
        public SlideApplication(ISlideRepository slideRepository)
        {
                _slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide command)
        {
            OperationResult operation = new OperationResult();
            var slide = new Slide(command.Picture, command.PictureTitle, command.PictureAlt, command.Heading,
                command.Title, command.Text, command.BtnText,command.Link);
            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return operation.Succeed();
        }

        public OperationResult Edit(EditSlide command)
        {
            OperationResult operation = new OperationResult();
            var slide = _slideRepository.Get(command.Id);

            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Edit(command.Picture, command.PictureTitle, command.PictureAlt, command.Heading,
                command.Title, command.Text, command.BtnText, command.Link);
            _slideRepository.SaveChanges();
            return operation.Succeed();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }

        public OperationResult Remove(long id)
        {
            OperationResult operation = new OperationResult();
            var slide = _slideRepository.Get(id);

            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Remove();
            _slideRepository.SaveChanges();
            return operation.Succeed();
        }

        public OperationResult Restore(long id)
        {
            OperationResult operation = new OperationResult();
            var slide = _slideRepository.Get(id);

            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Restore();
            _slideRepository.SaveChanges();
            return operation.Succeed();
        }
    }
}
