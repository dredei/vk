#region Using

using System.Collections.Generic;

#endregion

namespace VkNet.Model
{
    /// <summary>
    /// Ответ при поиске диалогов по строке поиска.
    /// См. описание <see href="http://vk.com/dev/messages.searchDialogs"/>.
    /// </summary>
    public class SearchDialogsResponse
    {
        /// <summary>
        /// Список найденных пользователей.
        /// </summary>
        public IList<User> Users { get; private set; }

        /// <summary>
        /// Список найденных бесед.
        /// </summary>
        public IList<Chat> Chats { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SearchDialogsResponse"/>.
        /// </summary>
        public SearchDialogsResponse()
        {
            this.Users = new List<User>();
            this.Chats = new List<Chat>();
        }
    }
}