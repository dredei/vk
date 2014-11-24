﻿#region Using

using VkNet.Utils;

#endregion

namespace VkNet.Model.Attachments
{
    public class PhotosList : MediaAttachment
    {
        static PhotosList()
        {
            RegisterType( typeof( PhotosList ), "photos_list" );
        }

        #region Private Methods

        internal static PhotosList FromJson( VkResponse response )
        {
            var list = new PhotosList();

            return list;
        }

        #endregion
    }
}