using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ModelsDTO
{
    public class BaseAuthUserData
    {
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }
}
