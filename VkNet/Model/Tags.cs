#region Using

using VkNet.Utils;

#endregion

namespace VkNet.Model
{
    public class Tags
    {
        public int Count { get; set; }

        #region Internal methods

        internal static Tags FromJson( VkResponse response )
        {
            var tags = new Tags();

            tags.Count = response[ "count" ];

            return tags;
        }

        #endregion
    }
}