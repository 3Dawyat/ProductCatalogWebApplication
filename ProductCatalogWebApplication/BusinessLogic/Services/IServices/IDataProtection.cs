namespace ProductCatalogWebApplication.BusinessLogic.Services.IServices
{
    public interface IDataProtection
    {
        public int Decode(string value);
        public string Encode(int id);
    }
}
