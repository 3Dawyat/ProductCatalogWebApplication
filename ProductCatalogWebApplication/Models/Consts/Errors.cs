namespace ProductCatalogWebApplication.Models.Consts
{
    public static class Errors
    {
        //add-migration addCategoryTableAndProductTableAndModifyApplicationUser
        public const string MustGreaterThan = "{0} must be greater than 0.";
        public const string NotAllowOldDate = "Not Allow Old Date !";
        public const string SavingData = "An error occurred while saving data";
        public const string RequiredField = "Required field !";
        public const string MaxLength = "Length cannot be more than {1} characters.";
        public const string Duplicated = "Another record with the same {0} is already exists !";
    }
}
