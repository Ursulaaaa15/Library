using System.ComponentModel.DataAnnotations;

public class UpdateUserRequest : IValidatableObject
{
    [Required]
    [MinLength(4)]
    public string Login { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    public List<OrderEntity> Orders { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Orders != null)
        {
            foreach (var order in Orders)
            {
                var orderValidationContext = new ValidationContext(order, validationContext, null);
                var orderValidationResults = new List<ValidationResult>();

                // Вызываем метод валидации для OrderEntity
                Validator.TryValidateObject(order, orderValidationContext, orderValidationResults, true);

                foreach (var validationResult in orderValidationResults)
                {
                    // Если есть ошибки валидации вложенного объекта, добавляем их к результатам текущего объекта
                    yield return new ValidationResult(validationResult.ErrorMessage, new[] { $"Orders[{Orders.IndexOf(order)}].{validationResult.MemberNames.FirstOrDefault()}" });
                }
            }
        }
    }

    public class OrderEntity
    {
        public int OrderId { get; set; }
    }
}
