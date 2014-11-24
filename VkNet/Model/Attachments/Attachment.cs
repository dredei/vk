#region Using

using System;
using VkNet.Exception;
using VkNet.Utils;

#endregion

namespace VkNet.Model.Attachments
{
    /// <summary>
    /// Информация о медиавложении в записи.
    /// См. описание <see href="http://vk.com/dev/attachments_w"/>. 
    /// </summary>
    public class Attachment
    {
        #region Поля

        /// <summary>
        /// Фотография из альбома или фотография, загруженная напрямую с компьютера пользователя.
        /// </summary>
        private Photo Photo { get; set; }

        /// <summary>
        /// Видеозапись.
        /// </summary>
        private Video Video { get; set; }

        /// <summary>
        /// Аудиозапись.
        /// </summary>
        private Audio Audio { get; set; }

        /// <summary>
        /// Документ.
        /// </summary>
        private Document Document { get; set; }

        /// <summary>
        /// Документ.
        /// </summary>
        private Graffiti Graffiti { get; set; }

        /// <summary>
        /// Ссылка на Web-страницу.
        /// </summary>
        private Link Link { get; set; }

        /// <summary>
        /// Заметка.
        /// </summary>
        private Note Note { get; set; }

        /// <summary>
        /// Контент приложения.
        /// </summary>
        private ApplicationContent ApplicationContent { get; set; }

        /// <summary>
        /// Опрос.
        /// </summary>
        private Poll Poll { get; set; }

        /// <summary>
        /// Wiki страница.
        /// </summary>
        private Page Page { get; set; }

        /// <summary>
        /// Альбом с фотографиями.
        /// </summary>
        private Album Album { get; set; }

        private PhotosList PhotosList;

        private Wall Wall { get; set; }

        private Sticker Sticker { get; set; }

        #endregion

        /// <summary>
        /// Экземпляр самого прикрепления.
        /// </summary>
        public object Instance
        {
            get
            {
                if ( this.Type == typeof( Photo ) )
                {
                    return this.Photo;
                }
                if ( this.Type == typeof( Video ) )
                {
                    return this.Video;
                }
                if ( this.Type == typeof( Audio ) )
                {
                    return this.Audio;
                }
                if ( this.Type == typeof( Document ) )
                {
                    return this.Document;
                }
                if ( this.Type == typeof( Graffiti ) )
                {
                    return this.Graffiti;
                }
                if ( this.Type == typeof( Link ) )
                {
                    return this.Link;
                }
                if ( this.Type == typeof( Note ) )
                {
                    return this.Note;
                }
                if ( this.Type == typeof( ApplicationContent ) )
                {
                    return this.ApplicationContent;
                }
                if ( this.Type == typeof( Poll ) )
                {
                    return this.Poll;
                }
                if ( this.Type == typeof( Page ) )
                {
                    return this.Page;
                }
                if ( this.Type == typeof( Album ) )
                {
                    return this.Album;
                }
                if ( this.Type == typeof( PhotosList ) )
                {
                    return this.PhotosList;
                }
                if ( this.Type == typeof( Wall ) )
                {
                    return this.Wall;
                }
                if ( this.Type == typeof( Sticker ) )
                {
                    return this.Sticker;
                }

                return null;
            }
        }

        /// <summary>
        /// Информация о типе вложения.
        /// </summary>
        public Type Type { get; set; }

        #region Методы

        internal static Attachment FromJson( VkResponse response )
        {
            // TODO: Complete it later
            var attachment = new Attachment();

            string type = response[ "type" ];
            switch ( type )
            {
                case "photo":
                case "posted_photo":
                    attachment.Type = typeof( Photo );
                    attachment.Photo = response[ type ];
                    break;

                case "video":
                    attachment.Type = typeof( Video );
                    attachment.Video = response[ "video" ];
                    break;

                case "audio":
                    attachment.Type = typeof( Audio );
                    attachment.Audio = response[ "audio" ];
                    break;

                case "doc":
                    attachment.Type = typeof( Document );
                    attachment.Document = response[ "doc" ];
                    break;

                case "graffiti":
                    attachment.Type = typeof( Graffiti );
                    attachment.Graffiti = response[ "graffiti" ];
                    break;

                case "link":
                    attachment.Type = typeof( Link );
                    attachment.Link = response[ "link" ];
                    break;

                case "note":
                    attachment.Type = typeof( Note );
                    attachment.Note = response[ "note" ];
                    break;

                case "app":
                    attachment.Type = typeof( ApplicationContent );
                    attachment.ApplicationContent = response[ "app" ];
                    break;

                case "poll":
                    attachment.Type = typeof( Poll );
                    attachment.Poll = response[ "poll" ];
                    break;

                case "page":
                    attachment.Type = typeof( Page );
                    attachment.Page = response[ "page" ];
                    break;

                case "album":
                    attachment.Type = typeof( Album );
                    attachment.Album = response[ "album" ];
                    break;

                case "photos_list":
                    attachment.Type = typeof( PhotosList );
                    attachment.PhotosList = response[ "photos_list" ];
                    break;

                case "wall":
                    attachment.Type = typeof( Wall );
                    attachment.Wall = response[ "wall" ];
                    break;

                case "sticker":
                    attachment.Type = typeof( Sticker );
                    attachment.Sticker = response[ "sticker" ];
                    break;

                default:
                    throw new InvalidParameterException( string.Format( "The type '{0}' of attachment is not defined.",
                        type ) );
            }

            return attachment;
        }

        #endregion
    }
}