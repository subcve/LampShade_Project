using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            OperationResult operation = new OperationResult();
            if (command == null)
                return operation.Failed(ApplicationMessages.NullRecord);
            if (_colleagueDiscountRepository.Exists(c => c.ProductId == command.ProductId /* && c.DiscountRate == command.DiscountRate*/))
                operation.Failed(ApplicationMessages.DuplicatedRecord);

            var discount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(discount);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeed();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            OperationResult operation = new OperationResult();

            if (command == null)
                return operation.Failed(ApplicationMessages.NullRecord);

            if (_colleagueDiscountRepository.Exists(c => c.DiscountRate == command.DiscountRate && c.ProductId == command.ProductId && c.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var discount = _colleagueDiscountRepository.Get(command.Id);
            if (discount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            discount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeed();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            OperationResult operation = new OperationResult();

            var discount = _colleagueDiscountRepository.Get(id);

            if (discount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            discount.Remove();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeed();
        }

        public OperationResult Restore(long id)
        {
            OperationResult operation = new OperationResult();

            var discount = _colleagueDiscountRepository.Get(id);

            if (discount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            discount.Restore();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeed();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}
