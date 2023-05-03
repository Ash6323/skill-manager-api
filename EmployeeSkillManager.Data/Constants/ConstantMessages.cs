namespace EmployeeSkillManager.Data.Constants
{
    public static class ConstantMessages
    {
        public static string DataRetrievedSuccessfully = "Data Retrieval Successful";
        public static string DataAddedSuccessfully = "Data Added Successfully";
        public static string DataUpdatedSuccessfully = "Data Updated Successfully";
        public static string DataDeletedSuccessfully = "Data Deleted Successfully";
        public static string UserAlreadyExists = "User Already Exists!!";
        public static string UserCreationFailed = "User Creation Failed! Please Check User Details and Try Again.";
        public static string UserCreated = "User Creation Successful";

        public static string UserNotFound = "User does not Exist";
        public static string SkillNotFound = "Skill does not Exist";
        public static string ErrorOccurred = "An Error Occurred during the Operation";
        public static string DuplicateSkill = "Employee already has this skill";

        public static string MatterDoesNotExist = "Matter with this ID does not Exist";

        public static string ClientDoesNotExist = "Client with this ID does not Exist";
        public static string ClientAlreadyExists = "Client with the same ID already Exists";
        public static string NoMatchingJurisdiction = "This Attorney is invalid for this Matter's Jurisdiction";
        public static string MattersByClientsNotFound = "Clients don't have any previous Matters";
        public static string MattersByClientNotFound = "This Client doesn't have any previous Matters";
        public static string InvoiceDoesNotExist = "Invoice with this ID does not Exist";
        public static string InvoicesByMattersNotFound = "Matters don't have any previous Invoices";
        public static string InvoicesByMatterNotFound = "This Matter doesn't have any previous Invoices";
        public static string InvalidAttorney = "Invalid Attorney selected for this Matter";
    }
}
