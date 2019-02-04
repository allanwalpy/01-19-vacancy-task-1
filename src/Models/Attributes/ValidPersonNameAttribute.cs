namespace App.Server.Models.Attributes
{
    //? applied on Name, Surname, MiddleName - string values;
    public class ValidPersonNameAttribute : ValidStringTypePropertyAttribute
    {
        public const string ValidRegex = "^[А-яA-z-]*$";
        public const int MaxLength = 20;
        public const int MinLength = 3;

        public ValidPersonNameAttribute()
            : base(ValidRegex, MinLength, MaxLength)
        { }
    }
}