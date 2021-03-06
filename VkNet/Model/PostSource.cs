﻿#region Using

using VkNet.Utils;

#endregion

namespace VkNet.Model
{
    /// <summary>
    /// Информация информацию о том, каким образом (через интерфейс сайта, виджет и т.п.) была создана запись на стене. 
    /// Используя данные из этого поля, разработчик может вывести уточняющую информацию о том, как была создана запись на стене 
    /// в своем приложении. 
    /// См. описание <see href="http://vk.com/dev/post_source"/>.
    /// </summary>
    public class PostSource
    {
        /// <summary>
        /// На данный момент поддерживаются следующие типы источников записи на стене, значение которых указываются в поле type: 
        /// - vk - запись создана через основной интерфейс сайта (http://vk.com/); 
        /// - widget - запись создана через виджет на стороннем сайте; 
        /// - api - запись создана приложением через API; 
        /// - rss - запись создана посредством импорта RSS-ленты со стороннего сайта; 
        /// - sms - запись создана посредством отправки SMS-сообщения на специальный номер. 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Поле data является опциональным и содержит следующие данные в зависимости от значения поля type: 
        /// - vk - содержит тип действия, из-за которого была создана запись: 
        ///     - profile_activity - изменение статуса под именем пользователя; 
        ///     - profile_photo - изменение профильной фотографии пользователя; 
        /// - widget - содержит тип виджета, через который была создана запись: 
        ///     - comments - виджет комментариев; 
        ///     - like - виджет «Мне нравится»; 
        ///     - poll - виджет опросов; 
        /// </summary>
        public string Data { get; set; }

        #region Методы

        internal static PostSource FromJson( VkResponse response )
        {
            var postSource = new PostSource();

            postSource.Type = response[ "type" ];
            postSource.Data = response[ "data" ];

            return postSource;
        }

        #endregion
    }
}