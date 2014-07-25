using NHibernate.Dialect;

namespace IvoryTower.Data
{
    public class MsSqlAzureDialect : MsSql2008Dialect
    {
        public override string PrimaryKeyString
        {
            get
            {
                return "primary key CLUSTERED";
            }
        }
    }
}