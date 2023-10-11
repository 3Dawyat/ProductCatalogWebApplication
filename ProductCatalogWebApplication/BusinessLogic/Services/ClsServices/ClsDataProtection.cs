
namespace ProductCatalogWebApplication.BusinessLogic.Services.ClsServices
{
    public class ClsDataProtection : IDataProtection
    {
        private readonly IHashids _hashids;
        public ClsDataProtection(IHashids hashids)
        {
            _hashids = hashids;
        }
        public string Encode(int id)
        {
            try
            {
                return _hashids.Encode(id);

            }
            catch
            {
                return "0";
            }

        }
        public int Decode(string value)
        {
            try
            {
                return _hashids.Decode(value)[0];
            }
            catch
            {
                return 0;
            }
        }

    }
}
