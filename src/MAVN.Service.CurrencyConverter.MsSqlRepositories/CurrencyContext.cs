﻿using System.Collections.Generic;
using System.Data.Common;
using JetBrains.Annotations;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.CurrencyConverter.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.CurrencyConverter.MsSqlRepositories
{
    public class CurrencyContext : PostgreSQLContext
    {
        private const string SchemaName = "currency";

        public DbSet<CurrencyRateEntity> CurrencyRates { get; set; }
        public DbSet<GlobalCurrencyRateEntity> GlobalCurrencyRates { get; set; }

        /// <summary>Used for EF migrations</summary>
        [UsedImplicitly]
        public CurrencyContext()
            : base(SchemaName)
        {
        }

        public CurrencyContext(DbContextOptions contextOptions)
            : base(SchemaName, contextOptions)
        {
        }

        public CurrencyContext(string connectionString, bool isTraceEnabled)
            : base(SchemaName, connectionString, isTraceEnabled)
        {
        }

        public CurrencyContext(DbConnection dbConnection)
            : base(SchemaName, dbConnection)
        {
        }

        protected override void OnMAVNModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyRateEntity>()
                .HasKey(o => new {o.BaseAsset, o.QuoteAsset});

            modelBuilder.Entity<CurrencyRateEntity>()
                .HasData(new List<CurrencyRateEntity>
                {
                    new CurrencyRateEntity {BaseAsset = "AED", QuoteAsset = "USD", Rate = 0.3061m}
                });
        }
    }
}
