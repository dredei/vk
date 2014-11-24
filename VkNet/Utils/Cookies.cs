#region Using

using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Reflection;

#endregion

namespace VkNet.Utils
{
    internal sealed class Cookies
    {
        public CookieContainer Container { get; private set; }

        public Cookies()
        {
            this.Container = new CookieContainer();
        }

        public void AddFrom( Uri responseUrl, CookieCollection cookies )
        {
            foreach ( Cookie cookie in cookies )
            {
                this.Container.Add( responseUrl, cookie );
            }

            this.BugFixCookieDomain();
        }

        private void BugFixCookieDomain()
        {
            var table =
                (Hashtable)
                    this.Container.GetType()
                        .InvokeMember( "m_domainTable",
                            BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance, null, this.Container,
                            new object[] { } );

            foreach ( var key in table.Keys.OfType<string>().ToList() )
            {
                if ( key[ 0 ] == '.' )
                {
                    string newKey = key.Remove( 0, 1 );
                    if ( !table.ContainsKey( newKey ) )
                    {
                        table[ newKey ] = table[ key ];
                    }
                }
            }
        }
    }
}