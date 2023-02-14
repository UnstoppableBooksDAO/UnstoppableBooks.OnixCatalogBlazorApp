namespace OnixCatalogBlazorApp.Models
{
    public class IpfsCredentialsItem
    {
        public string IpfsUrl { get; set; }

        public string IpfsUsername { get; set; }

        public string IpfsPassword { get; set; }

        public IpfsCredentialsItem() 
        {
            IpfsUrl = "https://ipfs.infura.io:5001";

            IpfsUsername = IpfsPassword = null;
        }
    }
}
