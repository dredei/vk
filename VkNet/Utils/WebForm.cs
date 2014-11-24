#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using VkNet.Exception;

#endregion

namespace VkNet.Utils
{
    internal sealed class WebForm
    {
        private readonly HtmlDocument _html;

        private readonly Dictionary<string, string> _inputs;

        private string _lastName;

        private readonly string _originalUrl;

        public Cookies Cookies { get; private set; }

        private WebForm( WebCallResult result )
        {
            this.Cookies = result.Cookies;
            this._originalUrl = result.RequestUrl.OriginalString;

            this._html = new HtmlDocument();
            result.LoadResultTo( this._html );

            this._inputs = this.ParseInputs();
        }

        public static WebForm From( WebCallResult result )
        {
            return new WebForm( result );
        }

        public WebForm And()
        {
            return this;
        }

        public WebForm WithField( string name )
        {
            this._lastName = name;

            return this;
        }

        public WebForm FilledWith( string value )
        {
            if ( string.IsNullOrEmpty( this._lastName ) )
            {
                throw new InvalidOperationException( "Field name not set!" );
            }

            string encodedValue = HttpUtility.UrlEncode( value );
            if ( this._inputs.ContainsKey( this._lastName ) )
            {
                this._inputs[ this._lastName ] = encodedValue;
            }
            else
            {
                this._inputs.Add( this._lastName, encodedValue );
            }

            return this;
        }

        public string ActionUrl
        {
            get
            {
                var formNode = this.GetFormNode();
                return formNode.Attributes[ "action" ] != null
                    ? formNode.Attributes[ "action" ].Value
                    : this.OriginalUrl;
            }
        }

        public string OriginalUrl
        {
            get { return this._originalUrl; }
        }

        public byte[] GetRequest()
        {
            string uri = this._inputs.Select( x => string.Format( "{0}={1}", x.Key, x.Value ) ).JoinNonEmpty( "&" );
            return Encoding.UTF8.GetBytes( uri );
        }

        private Dictionary<string, string> ParseInputs()
        {
            var inputs = new Dictionary<string, string>();

            var form = this.GetFormNode();
            foreach ( var node in form.SelectNodes( "//input" ) )
            {
                var nameAttribute = node.Attributes[ "name" ];
                var valueAttribute = node.Attributes[ "value" ];

                string name = nameAttribute != null ? nameAttribute.Value : string.Empty;
                string value = valueAttribute != null ? valueAttribute.Value : string.Empty;

                if ( string.IsNullOrEmpty( name ) )
                {
                    continue;
                }

                inputs.Add( name, HttpUtility.UrlEncode( value ) );
            }

            return inputs;
        }

        private HtmlNode GetFormNode()
        {
            HtmlNode.ElementsFlags.Remove( "form" );
            HtmlNode form = this._html.DocumentNode.SelectSingleNode( "//form" );
            if ( form == null )
            {
                throw new VkApiException( "Form element not found." );
            }

            return form;
        }
    }
}