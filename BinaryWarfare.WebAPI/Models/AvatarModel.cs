using System;
using System.IO;
using System.Diagnostics;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using Spring.IO;
using System.Threading;

namespace BinaryWarfare.WebAPI.Models
{
    public class AvatarModel
    {
        private const string DropboxAppKey = "36onnirryzekygf";
        private const string DropboxAppSecret = "u9spl1eitw8v49s";
        private const string oauthAccessKey = "ygdb4ycebeetmk90";
        private const string oauthAccessSecter = "reo69n8mjzi1a21";

        public static string GetMediaLink(string filePath, string username)
        {
            DropboxServiceProvider dropboxServiceProvider = new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);
            IDropbox dropbox = dropboxServiceProvider.GetApi(oauthAccessKey, oauthAccessSecter);

            string userAvatarFolderName = "User-Avatars";
            try
            {
                Entry createFolderEntry = dropbox.CreateFolder(userAvatarFolderName);
            }
            catch (Exception)
            {
            }

            //string picturePath = "../../pic.jpg";
            //string uploadName = "pic.jpg";

            string uploadPath = string.Format("/{0}/{1}", userAvatarFolderName, username);
            Entry uploadFileEntry = dropbox.UploadFile(new FileResource(filePath), uploadPath + ".jpg");
            DropboxLink mediaUrl = dropbox.GetMediaLink(uploadFileEntry.Path);

            return mediaUrl.Url;
        }
    }
}
