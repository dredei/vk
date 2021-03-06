﻿#region Using

using System;
using VkNet.Utils;

#endregion

namespace VkNet.Model.Attachments
{
    /// <summary>
    /// Фотография.
    /// </summary>
    /// <remarks>
    /// См. описание <see href="http://vk.com/dev/photo"/> и <see href="http://vk.com/dev/attachments_w"/> раздел "Альбом с фотографиями".
    /// </remarks>
    public class Photo : MediaAttachment
    {
        static Photo()
        {
            RegisterType( typeof( Photo ), "photo" );
        }

        /// <summary>
        /// Идентификатор альбома, в котором находится фотография.
        /// </summary>
        public long? AlbumId { get; set; }

        /// <summary>
        /// Url фотографии с максимальным размером 75x75px.
        /// </summary>
        public Uri Photo75 { get; set; }

        /// <summary>
        /// Url фотографии с максимальным размером 130x130px.
        /// </summary>
        public Uri Photo130 { get; set; }

        /// <summary>
        /// Url фотографии с максимальным размером 604x604px.
        /// </summary>
        public Uri Photo604 { get; set; }

        /// <summary>
        /// Url фотографии с максимальным размером 807x807px.
        /// </summary>
        public Uri Photo807 { get; set; }

        /// <summary>
        /// Url фотографии с максимальным размером 1280x1024px. 
        /// </summary>
        public Uri Photo1280 { get; set; }

        /// <summary>
        /// Url фотографии с максимальным размером  2560x2048px.
        /// </summary>
        public Uri Photo2560 { get; set; }

        /// <summary>
        /// Ширина оригинала фотографии в пикселах
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Высота оригинала фотографии в пикселах. 
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Текст описания фотографии. 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата добавления фотографии.
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Ключ доступа.
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// Идентификатор пользователя, загрузившего фото (если фотография размещена в сообществе). Для фотографий, размещенных от имени сообщества.
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Идентификатор записи, у которой данная фотография является прикреплением???
        /// </summary>
        public long? PostId { get; set; }

        /// <summary>
        /// идентификатор пользователя, сделавшего отметку
        /// </summary>
        public long? PlacerId { get; set; }

        /// <summary>
        /// дата создания отметки
        /// </summary>
        public DateTime? TagCreated { get; set; }

        /// <summary>
        /// идентификатор отметки
        /// </summary>
        public long? TagId { get; set; }

        /// <summary>
        /// Лайки
        /// </summary>
        public Likes Likes { get; set; }

        /// <summary>
        /// Возможность комментирования фотографии
        /// </summary>
        public bool? CanComment { get; set; }

        /// <summary>
        /// Комментарии
        /// </summary>
        public Comments Comments { get; set; }

        /// <summary>
        /// Теги
        /// </summary>
        public Tags Tags { get; set; }

        public Uri PhotoSrc { get; set; }
        public Uri PhotoHash { get; set; }

        /// <summary>
        /// Географическая широта отметки, заданная в градусах
        /// </summary>
        public double? Latitude;

        /// <summary>
        /// Географическая долгота отметки, заданная в градусах
        /// </summary>
        public double? Longitude;

        #region Методы

        /// <summary>
        /// Возвращает максимальный размер фото
        /// </summary>
        public Uri GetMaxResolution
        {
            get
            {
                if ( this.Photo2560 != null )
                {
                    return this.Photo2560;
                }
                if ( this.Photo1280 != null )
                {
                    return this.Photo1280;
                }
                if ( this.Photo807 != null )
                {
                    return this.Photo807;
                }
                if ( this.Photo604 != null )
                {
                    return this.Photo604;
                }
                if ( this.Photo130 != null )
                {
                    return this.Photo130;
                }
                if ( this.Photo75 != null )
                {
                    return this.Photo75;
                }
                return null;
            }
        }

        internal static Photo FromJson( VkResponse response )
        {
            var photo = new Photo();

            photo.Id = response[ "id" ];
            photo.AlbumId = response[ "album_id" ] ?? response[ "aid" ];
            photo.OwnerId = response[ "owner_id" ];
            photo.Photo75 = response[ "photo_75" ];
            photo.Photo130 = response[ "photo_130" ];
            photo.Photo604 = response[ "photo_604" ];
            photo.Photo807 = response[ "photo_807" ];
            photo.Photo1280 = response[ "photo_1280" ];
            photo.Photo2560 = response[ "photo_2560" ];
            photo.Width = response[ "width" ];
            photo.Height = response[ "height" ];
            photo.Text = response[ "text" ];
            photo.CreateTime = response[ "date" ];

            photo.UserId = Utilities.GetNullableLongId( response[ "user_id" ] );
            photo.PostId = Utilities.GetNullableLongId( response[ "post_id" ] );

            // из описания альбом с фотографиями
            photo.AccessKey = response[ "access_key" ];

            photo.PlacerId = Utilities.GetNullableLongId( response[ "placer_id" ] );
            photo.TagCreated = response[ "tag_created" ];
            photo.TagId = response[ "tag_id" ];

            photo.Likes = response[ "likes" ];
            photo.Comments = response[ "comments" ];
            photo.CanComment = response[ "can_comment" ];
            photo.Tags = response[ "tags" ];

            photo.PhotoSrc = response[ "photo_src" ];
            photo.PhotoHash = response[ "photo_hash" ];

            photo.Latitude = response[ "lat" ];
            photo.Longitude = response[ "long" ];

            return photo;
        }

        #endregion
    }
}