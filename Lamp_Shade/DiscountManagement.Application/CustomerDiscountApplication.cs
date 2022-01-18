using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;


namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            OperationResult operation = new OperationResult();

            if (command == null)
                operation.Failed(ApplicationMessages.NullRecord);

            if (_customerDiscountRepository.Exists(c => c.ProductId == command.ProductId && c.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            if(command.StartDate == command.EndDate)
                return operation.Failed("زمان اتمام تخفیف باید بعد از روز ایجاد آن باشد");

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            var discount = new CustomerDiscount(command.ProductId, command.DiscountRate,
                startDate, endDate, command.Reason);
            _customerDiscountRepository.Create(discount);
            _customerDiscountRepository.SaveChanges();
            return operation.Succeed();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            OperationResult operation = new OperationResult();

            var discount = _customerDiscountRepository.Get(command.Id);
            
            if (_customerDiscountRepository.Exists(c => c.ProductId == command.ProductId && c.DiscountRate == command.DiscountRate && c.Id != command.Id))
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            discount.Edit(command.Id,command.DiscountRate,startDate,endDate,command.Reason);
            _customerDiscountRepository.SaveChanges();

            return operation.Succeed();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}