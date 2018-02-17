﻿using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface ISiteSettingsRepository : IRepository<SiteSetting>
    {
    }

    public class SiteSettingsRepository : MathSiteEfCoreRepositoryBase<SiteSetting>, ISiteSettingsRepository
    {
        public SiteSettingsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}