using co_weelo_testproject_common.Dto;
using co_weelo_testproject_dal.Interfaces;
using FluentValidation;
using static co_weelo_testproject_common.Enums.GeneralEnums;

namespace co_weelo_testproject_bl.Validators
{
    public class PropertyImageDtoValidator : AbstractValidator<PropertyImageDto>
    {
        readonly IPropertyImageDal propertyImageDal;
        readonly IPropertyDal propertyDal;

        public PropertyImageDtoValidator(IPropertyImageDal _propertyImageDal, IPropertyDal _propertyDal)
        {
            propertyImageDal = _propertyImageDal;
            propertyDal = _propertyDal;

            RuleFor(elemento => elemento.IdPropertyImage)
               .Cascade(CascadeMode.Continue)
               .Must((Elemento, IdOwner) => { return ValidateIdPropertyImage(Elemento.IdPropertyImage, Elemento.Action); })
               .WithMessage(d => "El campo {PropertyName}: " + d.IdPropertyImage + " no se encuentra registrado en la base de datos")
               .OverridePropertyName("IdPropertyImage");

            RuleFor(elemento => elemento.IdProperty)
                .Cascade(CascadeMode.Continue)
                .Must((Elemento, IdOwner) => { return ValidateProperty(Elemento.IdProperty, Elemento.Action); })
                .WithMessage(d => "El campo {PropertyName}: " + d.IdProperty + " no existe en el sistema")
                .OverridePropertyName("IdProperty");

        }

        /// <summary>
        /// Method for validate existence of a IdPropertyImage
        /// </summary>
        /// <param name="IdPropertyImage">Id to validate</param>
        /// <param name="action">tye of action executed</param>
        /// <returns>true if the validation passed, otherwise false</returns>
        private bool ValidateIdPropertyImage(int? IdPropertyImage, ActionType action)
        {
            if (action == ActionType.Consult || action == ActionType.Update)
            {
                if (!IdPropertyImage.HasValue || IdPropertyImage == 0)
                    return false;

                PropertyImageDto propertyImageDto = propertyImageDal.SearchById((int)IdPropertyImage);
                if (propertyImageDto == null || propertyImageDto?.IdPropertyImage == 0)
                    return false;
            }

            return true;
        }


        /// <summary>
        /// Method to validate the owner
        /// </summary>
        /// <param name="IdProperty">IdProperty to validate existence</param>
        /// <param name="action">type of action in execution</param>
        /// <returns>true if the validation passed, otherwise false</returns>
        private bool ValidateProperty(int idProperty, ActionType action)
        {
            if (action == ActionType.New || action == ActionType.Update)
            {
                if (idProperty == 0) return false;

                PropertyDto property = propertyDal.SearchById(idProperty);
                if (property == null || property?.IdProperty == null)
                    return false;
            }
            return true;
        }
    }
}
