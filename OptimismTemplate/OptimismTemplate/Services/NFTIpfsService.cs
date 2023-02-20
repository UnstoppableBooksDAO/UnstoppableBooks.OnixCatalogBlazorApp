using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Ipfs;
using Ipfs.Http;
using Nethereum.Contracts.Standards.ERC721;
using Nethereum.Web3;
using Newtonsoft.Json;

namespace OptimismTemplate.Services
{
    public class NFTIpfsService
    {
        private readonly string _userName;
        private readonly string _password;
        private readonly string _ipfsUrl;

        public NFTIpfsService(string ipfsUrl)
        {
            _ipfsUrl = ipfsUrl;
        }

        public NFTIpfsService(string ipfsUrl, string userName, string password) : this(ipfsUrl)
        {
            _userName = userName;
            _password = password;
        }

        public Task<IPFSFileInfo> AddNftsMetadataToIpfsAsync<T>(T metadata, string fileName) where T : NftMetadata
        {
            var ipfsClient = GetSimpleHttpIpfs();
            return ipfsClient.AddObjectAsJson(metadata, fileName);
        }

        public async Task<IPFSFileInfo> AddStringToIpfsAsync(string fileContents, string fileName)
        {
            var ipfsClient = GetSimpleHttpIpfs();
            var node = await ipfsClient.AddAsync(UTF8Encoding.UTF8.GetBytes(fileContents), fileName);
            return node;
        }

        public async Task<IPFSFileInfo> AddFileToIpfsAsync(string path)
        {
            var file = new FileInfo(path);
            var ipfsClient = GetSimpleHttpIpfs();
            var node = await ipfsClient.AddAsync(File.ReadAllBytes(path), file.Name);
            return node;
        }

        static public async Task<string> GetStringFromIpfsGateway(string relativePath, bool addIpfsSuffix = true, string ipfsGateway = "https://gateway.ipfs.io/")
        {
            var uri = new Uri(ipfsGateway);

            if (addIpfsSuffix) uri = new Uri(uri, "ipfs");

            var fullUriPath = uri.AbsoluteUri + Path.DirectorySeparatorChar + relativePath;

            var fullUri = new Uri(fullUriPath);
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(fullUri).ConfigureAwait(false);
            }
        }

        static public async Task<T> GetJsonObjectFromIpfsGateway<T>(string relativePath, bool addIpfsSuffix = true, string ipfsGateway = "https://gateway.ipfs.io/")
        {            
            var uri = new Uri(ipfsGateway);

            if (addIpfsSuffix) uri = new Uri(uri, "ipfs");

            var fullUriPath = uri.AbsoluteUri + Path.DirectorySeparatorChar + relativePath;

            var fullUri = new Uri(fullUriPath);
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(fullUri).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        private IpfsHttpService GetSimpleHttpIpfs()
        {
            if (_userName == null) return new IpfsHttpService(_ipfsUrl);
            return new IpfsHttpService(_ipfsUrl, _userName, _password);
        }
    }
}
