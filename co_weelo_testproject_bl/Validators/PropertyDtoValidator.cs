using co_weelo_testproject_common.Dto;
using co_weelo_testproject_dal.Interfaces;
using FluentValidation;
using System;
using static co_weelo_testproject_common.Enums.GeneralEnums;

namespace co_weelo_testproject_bl.Validators
{
    public class PropertyDtoValidator : AbstractValidator<PropertyDto>
    {
        readonly IPropertyDal propertyDal;
        readonly IOwnerDal ownerDal;

        public PropertyDtoValidator(IPropertyDal _PropertyDal, IOwnerDal _ownerDal)
        {
            propertyDal = _PropertyDal;
            ownerDal = _ownerDal;

            RuleFor(elemento => elemento.IdProperty)
                .Cascade(CascadeMode.Continue)
                .Must((Elemento, IdOwner) => { return ValidateId(Elemento.IdProperty, Elemento.Action); })
                .WithMessage(d => "El campo {PropertyName}: " + d.IdOwner + " no se encuentra registrado en la base de datos")
                .OverridePropertyName("IdProperty");

            RuleFor(elemento => elemento.Name)
                .Cascade(CascadeMode.Continue)
                .NotEmpty().WithMessage("El campo {PropertyName} es obligatorio")
                .MaximumLength(50).WithMessage("El campo {PropertyName} no puede tener mas de {MaxLength} caracteres")
                .OverridePropertyName("Name");

            RuleFor(elemento => elemento.Address)
                .Cascade(CascadeMode.Continue)
                .NotEmpty().WithMessage("El campo {PropertyName} es obligatorio")
                .MaximumLength(30).WithMessage("El campo {PropertyName} no puede tener mas de {MaxLength} caracteres")
                .OverridePropertyName("Address");

            RuleFor(elemento => elemento.InternalCode)
                .Cascade(CascadeMode.Continue)
                .Must((Elemento, IdOwner) => { return Validatenumerics(Elemento.InternalCode, Elemento.Action); })
                .WithMessage(d => "El campo {PropertyName}: es obligatorio")
                .OverridePropertyName("InternalCode");

            RuleFor(elemento => elemento.Year)
                .Cascade(CascadeMode.Continue)
                .Must((Elemento, IdOwner) => { return Validatenumerics(Elemento.Year, Elemento.Action); })
                .WithMessage(d => "El campo {PropertyName}: es obligatorio")
                .OverridePropertyName("Year");

            RuleFor(elemento => elemento.IdOwner)
               .Cascade(CascadeMode.Continue)
               .Must((Elemento, IdOwner) => { return ValidateOwner(Elemento.IdOwner, Elemento.Action); })
               .WithMessage(d => "El campo {PropertyName}: " + d.IdOwner + " no existe en el sistema")
               .OverridePropertyName("IdOwner");

        }

        /// <summary>
        /// Method for validate existence of Property by Id
        /// </summary>
        /// <param name="idProperty">Id to validate</param>
        /// <param name="action">tye of action executed</param>
        /// <returns>true if the validation passed, otherwise false</returns>
        private bool ValidateId(int? idProperty, ActionType action)
        {
            if (action == ActionType.Consult || action == ActionType.Update)
            {
                if (!idProperty.HasValue || idProperty == 0)
                    return false;

                PropertyDto propertyDto = propertyDal.SearchById((int)idProperty);
                if (propertyDto == null || propertyDto?.IdProperty == 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Method to validate the internal code
        /// </summary>
        /// <param name="number">number to validate</param>
        /// <param name="action">type of action in execution</param>
        /// <returns>true if the validation passed, otherwise false</returns>
        private bool Validatenumerics(int number, ActionType action)
        {
            if (action == ActionType.New)
            {
                if (number == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Method to validate the owner
        /// </summary>
        /// <param name="id">IdOwner to validate existence</param>
        /// <param name="action">type of action in execution</param>
        /// <returns>true if the validation passed, otherwise false</returns>
        private bool ValidateOwner(int idOwner, ActionType action)
        {
            if (action == ActionType.New || action == ActionType.Update)
            {
                if (idOwner == 0) return false;

                OwnerDto owner = ownerDal.SearchById(idOwner);
                if (owner == null || owner?.IdOwner == null)
                    return false;
            }
            return true;
        }

    }
}
