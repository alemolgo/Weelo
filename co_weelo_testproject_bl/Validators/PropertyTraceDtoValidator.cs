using co_weelo_testproject_common.Dto;
using co_weelo_testproject_dal.Interfaces;
using FluentValidation;
using System;
using static co_weelo_testproject_common.Enums.GeneralEnums;

namespace co_weelo_testproject_bl.Validators
{
    public class PropertyTraceDtoValidator : AbstractValidator<PropertyTraceDto>
    {
        readonly IPropertyTraceDal propertyTraceDal;
        readonly IPropertyDal propertyDal;

        public PropertyTraceDtoValidator(IPropertyTraceDal _propertyTraceDal, IPropertyDal _propertyDal)
        {
            propertyTraceDal = _propertyTraceDal;
            propertyDal = _propertyDal;

            RuleFor(elemento => elemento.IdPropertyTrace)
               .Cascade(CascadeMode.Continue)
               .Must((Elemento, IdOwner) => { return ValidateId(Elemento.IdPropertyTrace, Elemento.Action); })
               .WithMessage(d => "El campo {PropertyName}: " + d.IdPropertyTrace + " no se encuentra registrado en la base de datos")
               .OverridePropertyName("IdPropertyTrace");

            RuleFor(elemento => elemento.SaleDate)
                .Cascade(CascadeMode.Continue)
                .Must((Elemento, IdOwner) => { return ValidateSaleDate(Elemento.SaleDate, Elemento.Action); })
                .WithMessage(d => "El campo {PropertyName}: es obligatorio")
                .OverridePropertyName("SaleDate");

            RuleFor(elemento => elemento.Name)
              .Cascade(CascadeMode.Continue)
              .NotEmpty().WithMessage("El campo {PropertyName} es obligatorio")
              .MaximumLength(50).WithMessage("El campo {PropertyName} no puede tener mas de {MaxLength} caracteres")
              .OverridePropertyName("Name");

            RuleFor(elemento => elemento.Value)
                .Cascade(CascadeMode.Continue)
                .Must((Elemento, IdOwner) => { return ValidateValue(Elemento.Value, Elemento.Action); })
                .WithMessage(d => "El campo {PropertyName}: es obligatorio")
                .OverridePropertyName("Value");

            RuleFor(elemento => elemento.IdProperty)
                .Cascade(CascadeMode.Continue)
                .Must((Elemento, IdOwner) => { return ValidateIdProperty(Elemento.IdProperty, Elemento.Action); })
                .WithMessage(d => "El campo {PropertyName}: " + d.IdProperty + " no se encuentra en el sistema")
                .OverridePropertyName("IdProperty");

        }

        /// <summary>
        /// Method for validate existence of PropertyTrace by Id
        /// </summary>
        /// <param name="IdPropertyTrace">Id to validate</param>
        /// <param name="action">Type of action executed</param>
        /// <returns>true if the validation passed, otherwise false</returns>
        private bool ValidateId(int? IdPropertyTrace, ActionType action)
        {
            if (action == ActionType.Consult || action == ActionType.Update)
            {
                if (!IdPropertyTrace.HasValue || IdPropertyTrace == 0)
                    return false;

                PropertyTraceDto propertyTraceDto = propertyTraceDal.SearchById((int)IdPropertyTrace);
                if (propertyTraceDto == null || propertyTraceDto?.IdPropertyTrace == null ||propertyTraceDto?.IdPropertyTrace == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Method to validate a value type decimal
        /// </summary>
        /// <param name="value">value to validate</param>
        /// <param name="action">type of action in execution</param>
        /// <returns>true if the validation passed, otherwise false</returns>
        private bool ValidateValue(decimal value, ActionType action)
        {
            if (action == ActionType.New)
            {
                if (value == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Method to validate Sale Date
        /// </summary>
        /// <param name="saleDate">SaleDate to validate</param>
        /// <param name="action">type of action in execution</param>
        /// <returns>true if the validation passed, otherwise false</returns>
        private bool ValidateSaleDate(DateTime saleDate, ActionType action)
        {
            if (action == ActionType.New)
            {
                if (saleDate == DateTime.MinValue)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Method to validate Id Property
        /// </summary>
        /// <param name="IdProperty"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool ValidateIdProperty(int idProperty, ActionType action)
        {
            if(action==ActionType.New)
            {
                if (idProperty == 0) return false;

                PropertyDto propertyDto = propertyDal.SearchById(idProperty);
                if (propertyDto == null || propertyDto?.IdProperty == null)
                    return false;
            }
            return true;
        }
    }
}
