using Common.Gen;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTM.Teste.Gen
{
    public class ConfigContext
    {
        #region Config Contexts

        private Context ConfigContextCore()
        {
            return new Context
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["Core"].ConnectionString,

                Namespace = "LTM.Teste",
                ContextName = "Core",
                ShowKeysInFront = true,
                LengthBigField = 250,

                OutputClassDomain = ConfigurationManager.AppSettings[string.Format("outputClassDomain")],
                OutputClassInfra = ConfigurationManager.AppSettings[string.Format("outputClassInfra")],
                OutputClassDto = ConfigurationManager.AppSettings[string.Format("outputClassDto")],
                OutputClassApp = ConfigurationManager.AppSettings[string.Format("outputClassApp")],
                OutputClassApi = ConfigurationManager.AppSettings[string.Format("outputClassApi")],
                OutputClassFilter = ConfigurationManager.AppSettings[string.Format("outputClassFilter")],
                OutputClassSummary = ConfigurationManager.AppSettings[string.Format("outputClassSummary")],
                OutputClassSso = ConfigurationManager.AppSettings["OutputClassSso"],

                Arquiteture = ArquitetureType.DDD,
                CamelCasing = true,

                TableInfo = new UniqueListTableInfo
                {

                   new TableInfo { TableName = "Produto", MakeDomain = true, MakeApp = true, MakeDto = true, MakeCrud = true, MakeApi= true, MakeSummary = true},

                }
            };
        }
        private Context ConfigContextAngular()
        {
            return new Context
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["Core"].ConnectionString,

                Namespace = "ltm.teste",
                ContextName = "core",
                ShowKeysInFront = true,
                LengthBigField = 250,

                OutputAngular = ConfigurationManager.AppSettings["OutputClassAngular"],

                Arquiteture = ArquitetureType.DDD,
                CamelCasing = true,
                MakeFront = true,

                TableInfo = new UniqueListTableInfo
                {

                   new TableInfo { TableName = "Produto", MakeFront = true },

                }
            };
        }

        public IEnumerable<Context> GetConfigContext()
        {

            return new List<Context>
            {
                ConfigContextCore(),
                ConfigContextAngular()
            };

        }

        #endregion
    }
}
