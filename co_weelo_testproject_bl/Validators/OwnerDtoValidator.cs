using co_weelo_testproject_common.Dto;
using co_weelo_testproject_dal.Interfaces;
using FluentValidation;
using System;
using static co_weelo_testproject_common.Enums.GeneralEnums;

namespace co_weelo_testproject_bl.Validators
{
    public class OwnerDtoValidator : AbstractValidator<OwnerDto>
    {
        readonly IOwnerDal ownerDal;

        public OwnerDtoValidator(IOwnerDal _OwnerDal)
        {
            ownerDal = _OwnerDal;

            RuleFor(elemento => elemento.IdOwner)
                .Cascade(CascadeMode.Continue)
                .Must((Elemento, IdOwner) => { return ValidateId(Elemento.IdOwner, Elemento.Action); })
                .WithMessage(d => "El campo {PropertyName}: "+d.IdOwner+" no se encuentra registrado en la base de datos")
                .OverridePropertyName("IdOwner");

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

            RuleFor(elemento => elemento.Birthday)
               .Cascade(CascadeMode.Continue)
               .Must((Elemento, IdOwner) => { return ValidateBirthDay(Elemento.Birthday, Elemento.Action); })
               .WithMessage(d => "El vaor del campo {PropertyName} no es valido")
               .OverridePropertyName("Birthday");

        }

        /// <summary>
        /// Method for validate existence of Owner by Id
        /// </summary>
        /// <param name="idOwner">Id to validate</param>
        /// <param name="action">Type of action executed</param>
        /// <returns>true if the validation passed, otherwise false</returns>
        private bool ValidateId(int? idOwner, ActionType action)
        {
            if (action == ActionType.Consult || action == ActionType.Update)
            {
                if (!idOwner.HasValue || idOwner==0)
                    return false;

                OwnerDto ownerDto = ownerDal.SearchById((int)idOwner);
                if (ownerDto == null || ownerDto?.IdOwner == 0)
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Method for validate existence of Owner by Name
        /// </summary>
        /// <param name="idOwner">Name to validate</param>
        /// <param name="action">tye of action executed</param>
        /// <returns>true if the validation passed, otherwise false</returns>
        private bool ValidateBirthDay(DateTime birthDay, ActionType action)
        {
            if (action == ActionType.New)
            {
                if (birthDay == DateTime.MinValue)
                    return false;
            }
            return true;
        }
    }
}
