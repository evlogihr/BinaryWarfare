using System;
using System.IO;
using System.Diagnostics;

using Spring.Social.OAuth1;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using Spring.IO;

namespace BinaryWarfare.Client
{
    class DropboxImageManager
    {
        private const string DropboxAppKey = "36onnirryzekygf";
        private const string DropboxAppSecret = "u9spl1eitw8v49s";
        private const string OAuthTokenFileName = "OAuthTokenFileName.txt";

        public static void Start()
        {
            DropboxServiceProvider dropboxServiceProvider =
                new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);

            if (!File.Exists(OAuthTokenFileName))
            {
                AuthorizeAppOAuth(dropboxServiceProvider);
            }
            OAuthToken oauthAccessToken = LoadOAuthToken();

            // Login in Dropbox
            IDropbox dropbox = dropboxServiceProvider.GetApi(oauthAccessToken.Value, oauthAccessToken.Secret);

            string unitPictures = "Unit-Pics";
            string buildingPictures = "Building-Pics";

            CreateDropboxFolder(dropbox, unitPictures);
            CreateDropboxFolder(dropbox, buildingPictures);

            string picturePath = "../../pic.jpg";
            string uploadName = "pic.jpg";

            UploadPicture(dropbox, picturePath, unitPictures, uploadName);
        }

        // Uploads picture to Dropbox and adds the media url to the SQL DB
        private static void UploadPicture(IDropbox dropbox, string picturePath, string uploadFolder, string uploadName)
        {
            string uploadPath = string.Format("/{0}/{1}", uploadFolder, uploadName);
            Entry uploadFileEntry =
                dropbox.UploadFileAsync(new FileResource(picturePath), uploadPath).Result;

            DropboxLink mediaUrl = dropbox.GetMediaLinkAsync(uploadFileEntry.Path).Result;

            // TODO: Upload mediaUrl to SQL DB

            //mediaUrl.Url;
        }

        private static void CreateDropboxFolder(IDropbox dropbox, string unitPictures)
        {
            try
            {
                Entry createFolderEntry = dropbox.CreateFolderAsync(unitPictures).Result;
            }
            catch (Exception)
            {
            }
        }

        private static OAuthToken LoadOAuthToken()
        {
            string[] lines = File.ReadAllLines(OAuthTokenFileName);
            OAuthToken oauthAccessToken = new OAuthToken(lines[0], lines[1]);
            return oauthAccessToken;
        }

        private static void AuthorizeAppOAuth(DropboxServiceProvider dropboxServiceProvider)
        {
            OAuthToken oauthToken = dropboxServiceProvider.OAuthOperations.FetchRequestTokenAsync(null, null).Result;
            OAuth1Parameters parameters = new OAuth1Parameters();
            string authenticateUrl = dropboxServiceProvider.OAuthOperations.BuildAuthorizeUrl(oauthToken.Value, parameters);
            Process.Start(authenticateUrl);

            AuthorizedRequestToken requestToken = new AuthorizedRequestToken(oauthToken, null);
            OAuthToken oauthAccessToken = dropboxServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null).Result;

            string[] oauthData = new string[] { oauthAccessToken.Value, oauthAccessToken.Secret };
            File.WriteAllLines(OAuthTokenFileName, oauthData);
        }
    }
}
