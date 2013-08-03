using System.Collections.Generic;
using System.Dynamic;

namespace Jumbleblocks.Net.Core.Dynamic
{
    public class Expando : DynamicObject
    {
        private readonly Dictionary<string, object> _members = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!_members.ContainsKey(binder.Name))
                _members.Add(binder.Name, value);
            else
                _members[binder.Name] = value;

            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_members.ContainsKey(binder.Name))
            {
                result = _members[binder.Name];
                return true;
            }
            
            return base.TryGetMember(binder, out result);
        }
    }
}
