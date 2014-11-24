#region Using

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

#endregion

namespace VkNet.Utils
{
    internal sealed class VkResponseArray : IEnumerable<VkResponse>
    {
        private readonly JArray _array;

        public VkResponseArray( JArray array )
        {
            this._array = array;
        }

        public VkResponse this[ object key ]
        {
            get
            {
                var token = this._array[ key ];
                return new VkResponse( token );
            }
        }

        public int Count
        {
            get { return this._array.Count; }
        }

        public IEnumerator<VkResponse> GetEnumerator()
        {
            return this._array.Select( i => new VkResponse( i ) ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}