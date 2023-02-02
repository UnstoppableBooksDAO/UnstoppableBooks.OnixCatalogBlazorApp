namespace OnixCatalogBlazorApp.Models
{
    public class KeyStoreItem
    {
        public string? PrivateKey { get; set; }

        public string? Password { get; set; }

        public KeyStoreItem() 
        {
            PrivateKey = Password = null;
        }
    }
}
