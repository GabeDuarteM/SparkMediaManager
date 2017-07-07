// Developed by: Gabriel Duarte
// 
// Created at: 14/05/2016 16:41

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace SparkMediaManager.Models
{
    public interface IContext
    {
        DbSet<Feed> Feed { get; set; }

        DbSet<Serie> Serie { get; set; }

        DbSet<T> Set<T>() where T : class;

        DbSet Set(Type entityType);

        int SaveChanges();

        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbEntityEntry Entry(object entity);
    }
}
