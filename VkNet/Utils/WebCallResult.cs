#region Using

using System;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;

#endregion

namespace VkNet.Utils
{
    internal sealed class WebCallResult
    {
        public Uri RequestUrl { get; private set; }

        public Cookies Cookies { get; private set; }

        public Uri ResponseUrl { get; set; }

        public string Response { get; private set; }

        public WebCallResult( string url, Cookies cookies )
        {
            this.RequestUrl = new Uri( url );
            this.Cookies = cookies;
            this.Response = string.Empty;
        }

        public void SaveCookies( CookieCollection cookies )
        {
            this.Cookies.AddFrom( this.ResponseUrl, cookies );
        }

        public void SaveResponse( Uri responseUrl, Stream stream, Encoding encoding )
        {
            this.ResponseUrl = responseUrl;

            using ( var reader = new StreamReader( stream, encoding ) )
            {
                this.Response = reader.ReadToEnd();
            }
        }

        public void LoadResultTo( HtmlDocument htmlDocument )
        {
            htmlDocument.LoadHtml( this.Response );
        }
    }
}