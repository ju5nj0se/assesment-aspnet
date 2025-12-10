namespace JuanJoseHernandez.Constants
{
    /// <summary>
    /// Centralized validation messages for use throughout the application
    /// </summary>
    public static class ValidationMessages
    {
        // General required field messages
        public const string Required = "This field is required";
        public const string NameRequired = "The name is required";
        public const string DocumentRequired = "The document is required";
        public const string EmailRequired = "The email is required";
        public const string PhoneRequired = "The phone number is required";
        public const string AddressRequired = "The address is required";
        public const string DateRequired = "The date is required";
        public const string StatusRequired = "The status is required";
        public const string DepartmentRequired = "The department is required";
        public const string DegreeRequired = "The academic degree is required";
        public const string EducationLevelRequired = "The education level is required";
        public const string ProfileRequired = "The profile is required";
        public const string SalaryRequired = "The salary is required";

        // Maximum length messages
        public const string MaxLength20 = "This field cannot exceed 20 characters";
        public const string MaxLength50 = "This field cannot exceed 50 characters";
        public const string MaxLength100 = "This field cannot exceed 100 characters";
        public const string MaxLength200 = "This field cannot exceed 200 characters";

        // Format messages
        public const string InvalidEmailFormat = "The email format is not valid";
        public const string InvalidPhoneFormat = "The phone number format is not valid";

        // Range messages
        public const string PositiveValueRequired = "The value must be positive";
        public const string InvalidRange = "The value is out of the allowed range";
        
        public static class ValidationDegree
        {
            public const string InvalidDegree = "Invalid degree. Allowed: Ingeniero, Soporte Técnico, Analista, Coordinador, Desarrollador, Auxiliar";
        }

        public static class ValidationStatus
        {
            public const string InvalidStatus = "Invalid status. Allowed: Vacaciones, Activo, Inactivo";
        }

        public static class ValidationEducationLevel
        {
            public const string InvalidEducationLevel = "Invalid education level. Allowed: Profesional, Tecnólogo, Maestría, Especialización";
        }

        public static class ValidationDepartment
        {
            public const string InvalidDepartment = "Invalid department. Allowed: Logística, Marketing, Recursos Humanos, Operaciones, Tecnología, Contabilidad, Ventas";
        }
    }
}
