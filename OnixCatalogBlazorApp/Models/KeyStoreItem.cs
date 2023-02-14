namespace OnixCatalogBlazorApp.Models
{
    public class KeyStoreItem
    {
        public string? Password { get; set; }

        public string? EthereumPrivateKey { get; set; }

        public KeyStoreItem() 
        {
            EthereumPrivateKey = Password =  null;
        }
    }
}
