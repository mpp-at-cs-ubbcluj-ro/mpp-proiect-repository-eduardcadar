using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentiiDeTurism.src.domain
{
    interface IIdentifiable<Tid>
    {
        Tid getId();
        void setId(Tid id);
    }
}
