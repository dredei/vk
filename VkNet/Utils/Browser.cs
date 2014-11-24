﻿#region Using

using System.Net;
using System.Text;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;

#endregion

namespace VkNet.Utils
{
    /// <summary>
    /// Браузер, через который производится сетевое взаимодействие с ВКонтакте.
    /// Сетевое взаимодействие выполняется с помощью <see cref="HttpWebRequest"/>. 
    /// </summary>
    public class Browser : IBrowser
    {
        /// <summary>
        /// Получение json по url-адресу
        /// </summary>
        /// <param name="url">Адрес получения json</param>
        /// <returns>Строка в формате json</returns>
        public string GetJson( string url )
        {
            var separatorPosition = url.IndexOf( '?' );
            string methodUrl = separatorPosition < 0 ? url : url.Substring( 0, separatorPosition );
            string parameters = separatorPosition < 0 ? string.Empty : url.Substring( separatorPosition + 1 );

            return WebCall.PostCall( methodUrl, parameters ).Response;
        }

#if false
    /// <summary>
    /// Асинхронное получение json по url-адресу
    /// </summary>
    /// <param name="url">Адрес получения json</param>
    /// <returns>Строка в формате json</returns>
        public async Task<string> GetJsonAsync(string url)
        {
            // todo refactor this shit
            var separatorPosition = url.IndexOf('?');
            string methodUrl = separatorPosition < 0 ? url : url.Substring(0, separatorPosition);
            string parameters = separatorPosition < 0 ? string.Empty : url.Substring(separatorPosition + 1);

            return await WebCall.PostCallAsync(url, parameters);
        }
#endif

        /// <summary>
        /// Авторизация на сервере ВК
        /// </summary>
        /// <param name="appId">Идентификатор приложения</param>
        /// <param name="email">Логин - телефон или эл. почта</param>
        /// <param name="password">Пароль</param>
        /// <param name="settings">Уровень доступа приложения</param>
        /// <returns>Информация об авторизации приложения</returns>
        public VkAuthorization Authorize( int appId, string email, string password, Settings settings )
        {
            var authorizeUrl = CreateAuthorizeUrlFor( appId, settings, Display.Wap );
            var authorizeUrlResult = WebCall.MakeCall( authorizeUrl );

            var loginForm =
                WebForm.From( authorizeUrlResult )
                    .WithField( "email" )
                    .FilledWith( email )
                    .And()
                    .WithField( "pass" )
                    .FilledWith( password );
            var loginFormPostResult = WebCall.Post( loginForm );

            var authorization = VkAuthorization.From( loginFormPostResult.ResponseUrl );
            if ( !authorization.IsAuthorizationRequired )
            {
                return authorization;
            }

            var authorizationForm = WebForm.From( loginFormPostResult );
            var authorizationFormPostResult = WebCall.Post( authorizationForm );
            return VkAuthorization.From( authorizationFormPostResult.ResponseUrl );
        }

        internal static string CreateAuthorizeUrlFor( int appId, Settings settings, Display display )
        {
            var builder = new StringBuilder( "https://oauth.vk.com/authorize?" );

            builder.AppendFormat( "client_id={0}&", appId );
            builder.AppendFormat( "scope={0}&", settings );
            builder.Append( "redirect_uri=https://oauth.vk.com/blank.html&" );
            builder.AppendFormat( "display={0}&", display );
            builder.Append( "response_type=token" );

            return builder.ToString();
        }
    }
}