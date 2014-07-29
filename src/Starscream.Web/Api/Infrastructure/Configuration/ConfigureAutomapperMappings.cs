using System;
using Autofac;

namespace Starscream.Web.Api.Infrastructure.Configuration
{
    public class ConfigureAutomapperMappings : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get
            {
                return container =>
                           {
                               //Mapper.CreateMap<Company, CompanyModel>().ReverseMap();
                               //Mapper.CreateMap<Market, MarketModel>().ReverseMap();
                               //Mapper.CreateMap<Location, LocationModel>().ReverseMap();
                               //Mapper.CreateMap<Executor, ExecutorDetailModel>().ReverseMap();
                               //Mapper.CreateMap<ITier, TierModel>();
                               //Mapper.CreateMap<Nymex, NymexModel>().ReverseMap();
                               //Mapper.CreateMap<Ask, AskModel>().ReverseMap();
                           };
            }
        }

        #endregion
    }
}