﻿#region Using

using System.Net;
using System.Text;
using VkNet.Exception;

#endregion

namespace VkNet.Utils
{
    internal sealed class WebCall
    {
        private HttpWebRequest Request { get; set; }

        private WebCallResult Result { get; set; }

        private WebCall( string url, Cookies cookies )
        {
            this.Request = (HttpWebRequest)WebRequest.Create( url );
            this.Request.Accept = "text/html";
            this.Request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
            this.Request.CookieContainer = cookies.Container;

            this.Result = new WebCallResult( url, cookies );
        }

        public static WebCallResult MakeCall( string url )
        {
            var call = new WebCall( url, new Cookies() );

            return call.MakeRequest();
        }

#if false // async version for PostCall
        public static async Task<string> PostCallAsync(string url, string parameters)
        {
            var content = new StringContent(parameters);
            string output = string.Empty;
            using (var client = new HttpClient())
            {   
                HttpResponseMessage response = await client.PostAsync(url, content);
                output = await response.Content.ReadAsStringAsync();
            }

            return output;
        }
#endif

        public static WebCallResult PostCall( string url, string parameters )
        {
            var call = new WebCall( url, new Cookies() );
            call.Request.Method = "POST";
            call.Request.ContentType = "application/x-www-form-urlencoded";
            var data = Encoding.UTF8.GetBytes( parameters );
            call.Request.ContentLength = data.Length;

            using ( var requestStream = call.Request.GetRequestStream() )
            {
                requestStream.Write( data, 0, data.Length );
            }

            return call.MakeRequest();
        }

        public static WebCallResult Post( WebForm form )
        {
            var call = new WebCall( form.ActionUrl, form.Cookies );

            var request = call.Request;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            var formRequest = form.GetRequest();
            request.ContentLength = formRequest.Length;
            request.GetRequestStream().Write( formRequest, 0, formRequest.Length );
            request.AllowAutoRedirect = false;
            request.Referer = form.OriginalUrl;

            return call.MakeRequest();
        }

        private WebCallResult RedirectTo( string url )
        {
            var call = new WebCall( url, this.Result.Cookies );

            var request = call.Request;
            request.Method = "GET";
            request.ContentType = "text/html";
            request.Referer = this.Request.Referer;

            return call.MakeRequest();
        }

        private WebCallResult MakeRequest()
        {
            using ( var response = this.GetResponse() )
            {
                using ( var stream = response.GetResponseStream() )
                {
                    if ( stream == null )
                    {
                        throw new VkApiException( "Response is null." );
                    }

                    var encoding = response.CharacterSet != null
                        ? Encoding.GetEncoding( response.CharacterSet )
                        : Encoding.UTF8;
                    this.Result.SaveResponse( response.ResponseUri, stream, encoding );

                    this.Result.SaveCookies( response.Cookies );

                    if ( response.StatusCode == HttpStatusCode.Redirect )
                    {
                        return this.RedirectTo( response.Headers[ "Location" ] );
                    }

                    return this.Result;
                }
            }
        }

        private HttpWebResponse GetResponse()
        {
            try
            {
                return (HttpWebResponse)this.Request.GetResponse();
            }
            catch ( WebException ex )
            {
                throw new VkApiException( ex.Message, ex );
            }
        }
    }
}