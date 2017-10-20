using Common.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTM.Teste.Gen
{
    public class HelperSysObjects : HelperSysObjectsDDD
    {

        public HelperSysObjects(IEnumerable<Context> contexts)
            : base(contexts)
        {

        }

        public override HelperSysObjectsBase DefineFrontTemplateClass(Context config)
        {
            return new HelperSysObjectsAngular20(config);
        }
    }
}
