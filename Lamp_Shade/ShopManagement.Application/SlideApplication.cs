using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IFileUpload _fileUpload;
		public SlideApplication(ISlideRepository slideRepository, IFileUpload fileUpload)
		{
			_slideRepository = slideRepository;
			_fileUpload = fileUpload;
		}

		public OperationResult Create(CreateSlide command)
        {
            OperationResult operation = new OperationResult();
            var fileName = _fileUpload.Upload(command.Picture, "Sildes");
            var slide = new Slide(fileName, command.PictureTitle, command.PictureAlt, command.Heading,
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
            var fileName = _fileUpload.Upload(command.Picture, "Sildes");
            slide.Edit(fileName, command.PictureTitle, command.PictureAlt, command.Heading,
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
