namespace products.Domain.Omie.Shared
{
    public class OmieRequest
    {
        public OmieRequest(string call, string app_key, string app_secrets, List<object> param)
        {
            this.call = call;
            this.app_key = app_key;
            this.app_secret = app_secrets;
            this.param = param;
        }

        public string call { get; private set; }
        public string app_key { get; private set; }
        public string app_secret { get; private set; }
        public List<object> param { get; private set; }

    }
}