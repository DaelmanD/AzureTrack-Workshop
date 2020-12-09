using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMotownFestival.Api.Common
{
    public class BlobUtility
    {
        private StorageSharedKeyCredential Credential {get;}
        private BlobServiceClient Client { get; }
        private BlobSettingsOptions Options { get; }

        public BlobUtility(StorageSharedKeyCredential cred, BlobServiceClient client, IOptions<BlobSettingsOptions> opts)
        {
            Credential = cred;
            Client = client;
            Options = opts.Value;
        }

        public BlobContainerClient GetPicturesContainer()
        {
            return Client.GetBlobContainerClient(Options.PicturesContainer);
        }

        public BlobContainerClient GetThumbsContainer()
        {
            return Client.GetBlobContainerClient(Options.ThumbsContainer);
        }

        public string GetSasUri(BlobContainerClient container, string blobName)
        {
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = container.Name,
                BlobName = blobName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow.AddMinutes(-1),
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(2)
            };

            sasBuilder.SetPermissions(BlobAccountSasPermissions.Read);

            string token = sasBuilder.ToSasQueryParameters(Credential).ToString();
            return $"{container.Uri.AbsoluteUri}/{blobName}?{token}";
        }
    }
}
