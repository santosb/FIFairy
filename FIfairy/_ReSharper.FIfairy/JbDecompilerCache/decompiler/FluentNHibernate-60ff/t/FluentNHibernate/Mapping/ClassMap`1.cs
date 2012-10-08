// Type: FluentNHibernate.Mapping.ClassMap`1
// Assembly: FluentNHibernate, Version=1.3.0.717, Culture=neutral, PublicKeyToken=8aa435e3cb308880
// Assembly location: C:\FI fairy\FIfairy\FIfairy\FIfairy\packages\FluentNHibernate.1.3.0.717\lib\FluentNHibernate.dll

using FluentNHibernate;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Utils;
using NHibernate.Persister.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentNHibernate.Mapping
{
  public class ClassMap<T> : ClasslikeMapBase<T>, IMappingProvider
  {
    private readonly AttributeStore<ClassMapping> attributes;
    private readonly MappingProviderStore providers;
    private readonly OptimisticLockBuilder<ClassMap<T>> optimisticLock;
    private readonly IList<ImportPart> imports;
    private bool nextBool;
    private readonly HibernateMappingPart hibernateMappingPart;
    private readonly PolymorphismBuilder<ClassMap<T>> polymorphism;
    private readonly SchemaActionBuilder<ClassMap<T>> schemaAction;

    public CachePart Cache { get; private set; }

    public HibernateMappingPart HibernateMapping
    {
      get
      {
        return this.hibernateMappingPart;
      }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public ClassMap<T> Not
    {
      get
      {
        this.nextBool = !this.nextBool;
        return this;
      }
    }

    public OptimisticLockBuilder<ClassMap<T>> OptimisticLock
    {
      get
      {
        return this.optimisticLock;
      }
    }

    public PolymorphismBuilder<ClassMap<T>> Polymorphism
    {
      get
      {
        return this.polymorphism;
      }
    }

    public SchemaActionBuilder<ClassMap<T>> SchemaAction
    {
      get
      {
        return this.schemaAction;
      }
    }

    public ClassMap()
      : this(new AttributeStore<ClassMapping>(), new MappingProviderStore())
    {
    }

    protected ClassMap(AttributeStore<ClassMapping> attributes, MappingProviderStore providers)
      : base(providers)
    {
      this.attributes = attributes;
      this.providers = providers;
      this.optimisticLock = new OptimisticLockBuilder<ClassMap<T>>(this, (Action<string>) (value =>
      {
        AttributeStore<ClassMapping> temp_48 = attributes;
        ParameterExpression local_0 = Expression.Parameter(typeof (ClassMapping), "x");
        // ISSUE: method reference
        Expression<Func<ClassMapping, string>> temp_64 = Expression.Lambda<Func<ClassMapping, string>>((Expression) Expression.Property((Expression) local_0, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_OptimisticLock))), new ParameterExpression[1]
        {
          local_0
        });
        string temp_65 = value;
        temp_48.Set<string>(temp_64, temp_65);
      }));
      this.polymorphism = new PolymorphismBuilder<ClassMap<T>>(this, (Action<string>) (value =>
      {
        AttributeStore<ClassMapping> temp_67 = attributes;
        ParameterExpression local_0 = Expression.Parameter(typeof (ClassMapping), "x");
        // ISSUE: method reference
        Expression<Func<ClassMapping, string>> temp_83 = Expression.Lambda<Func<ClassMapping, string>>((Expression) Expression.Property((Expression) local_0, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_Polymorphism))), new ParameterExpression[1]
        {
          local_0
        });
        string temp_84 = value;
        temp_67.Set<string>(temp_83, temp_84);
      }));
      this.schemaAction = new SchemaActionBuilder<ClassMap<T>>(this, (Action<string>) (value =>
      {
        AttributeStore<ClassMapping> temp_86 = attributes;
        ParameterExpression local_0 = Expression.Parameter(typeof (ClassMapping), "x");
        // ISSUE: method reference
        Expression<Func<ClassMapping, string>> temp_102 = Expression.Lambda<Func<ClassMapping, string>>((Expression) Expression.Property((Expression) local_0, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_SchemaAction))), new ParameterExpression[1]
        {
          local_0
        });
        string temp_103 = value;
        temp_86.Set<string>(temp_102, temp_103);
      }));
      this.Cache = new CachePart(typeof (T));
    }

    public virtual IdentityPart Id(Expression<Func<T, object>> memberExpression)
    {
      return this.Id(memberExpression, (string) null);
    }

    public virtual IdentityPart Id(Expression<Func<T, object>> memberExpression, string column)
    {
      IdentityPart identityPart = new IdentityPart(this.EntityType, ReflectionExtensions.ToMember<T, object>(memberExpression));
      if (column != null)
        identityPart.Column(column);
      this.providers.Id = (IIdentityMappingProvider) identityPart;
      return identityPart;
    }

    public IdentityPart Id()
    {
      return this.Id<int>((string) null).GeneratedBy.Increment();
    }

    public IdentityPart Id<TId>()
    {
      return this.Id<TId>((string) null);
    }

    public IdentityPart Id<TId>(string column)
    {
      IdentityPart identityPart = new IdentityPart(typeof (T), typeof (TId));
      if (column != null)
      {
        identityPart.SetName(column);
        identityPart.Column(column);
      }
      this.providers.Id = (IIdentityMappingProvider) identityPart;
      return identityPart;
    }

    public virtual NaturalIdPart<T> NaturalId()
    {
      NaturalIdPart<T> naturalIdPart = new NaturalIdPart<T>();
      this.providers.NaturalId = (INaturalIdMappingProvider) naturalIdPart;
      return naturalIdPart;
    }

    public virtual CompositeIdentityPart<T> CompositeId()
    {
      CompositeIdentityPart<T> compositeIdentityPart = new CompositeIdentityPart<T>();
      this.providers.CompositeId = (ICompositeIdMappingProvider) compositeIdentityPart;
      return compositeIdentityPart;
    }

    public virtual CompositeIdentityPart<TId> CompositeId<TId>(Expression<Func<T, TId>> memberExpression)
    {
      CompositeIdentityPart<TId> compositeIdentityPart = new CompositeIdentityPart<TId>(ReflectionExtensions.ToMember<T, TId>(memberExpression).Name);
      this.providers.CompositeId = (ICompositeIdMappingProvider) compositeIdentityPart;
      return compositeIdentityPart;
    }

    public VersionPart Version(Expression<Func<T, object>> memberExpression)
    {
      return this.Version(ReflectionExtensions.ToMember<T, object>(memberExpression));
    }

    protected virtual VersionPart Version(Member property)
    {
      VersionPart versionPart = new VersionPart(typeof (T), property);
      this.providers.Version = (IVersionMappingProvider) versionPart;
      return versionPart;
    }

    public virtual DiscriminatorPart DiscriminateSubClassesOnColumn<TDiscriminator>(string columnName, TDiscriminator baseClassDiscriminator)
    {
      DiscriminatorPart discriminatorPart = new DiscriminatorPart(columnName, typeof (T), new Action<Type, ISubclassMappingProvider>(this.providers.Subclasses.Add), new TypeReference(typeof (TDiscriminator)));
      this.providers.Discriminator = (IDiscriminatorMappingProvider) discriminatorPart;
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, object>> exp = Expression.Lambda<Func<ClassMapping, object>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_DiscriminatorValue))), new ParameterExpression[1]
      {
        parameterExpression
      });
      // ISSUE: variable of a boxed type
      __Boxed<TDiscriminator> local = (object) baseClassDiscriminator;
      attributeStore.Set<object>(exp, (object) local);
      return discriminatorPart;
    }

    public virtual DiscriminatorPart DiscriminateSubClassesOnColumn<TDiscriminator>(string columnName)
    {
      DiscriminatorPart discriminatorPart = new DiscriminatorPart(columnName, typeof (T), new Action<Type, ISubclassMappingProvider>(this.providers.Subclasses.Add), new TypeReference(typeof (TDiscriminator)));
      this.providers.Discriminator = (IDiscriminatorMappingProvider) discriminatorPart;
      return discriminatorPart;
    }

    public virtual DiscriminatorPart DiscriminateSubClassesOnColumn(string columnName)
    {
      return this.DiscriminateSubClassesOnColumn<string>(columnName);
    }

    public virtual void UseUnionSubclassForInheritanceMapping()
    {
      AttributeStore<ClassMapping> attributeStore1 = this.attributes;
      ParameterExpression parameterExpression1 = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, bool>> exp1 = Expression.Lambda<Func<ClassMapping, bool>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_Abstract))), new ParameterExpression[1]
      {
        parameterExpression1
      });
      int num1 = 1;
      attributeStore1.Set<bool>(exp1, num1 != 0);
      AttributeStore<ClassMapping> attributeStore2 = this.attributes;
      ParameterExpression parameterExpression2 = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, bool>> exp2 = Expression.Lambda<Func<ClassMapping, bool>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_IsUnionSubclass))), new ParameterExpression[1]
      {
        parameterExpression2
      });
      int num2 = 1;
      attributeStore2.Set<bool>(exp2, num2 != 0);
    }

    [Obsolete("Inline definitions of subclasses are depreciated. Please create a derived class from SubclassMap in the same way you do with ClassMap.")]
    public virtual void JoinedSubClass<TSubclass>(string keyColumn, Action<JoinedSubClassPart<TSubclass>> action) where TSubclass : T
    {
      JoinedSubClassPart<TSubclass> joinedSubClassPart = new JoinedSubClassPart<TSubclass>(keyColumn);
      action(joinedSubClassPart);
      this.providers.Subclasses[typeof (TSubclass)] = (ISubclassMappingProvider) joinedSubClassPart;
    }

    public void Schema(string schema)
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, string>> exp = Expression.Lambda<Func<ClassMapping, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_Schema))), new ParameterExpression[1]
      {
        parameterExpression
      });
      string str = schema;
      attributeStore.Set<string>(exp, str);
    }

    public void Table(string tableName)
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, string>> exp = Expression.Lambda<Func<ClassMapping, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_TableName))), new ParameterExpression[1]
      {
        parameterExpression
      });
      string str = tableName;
      attributeStore.Set<string>(exp, str);
    }

    public void LazyLoad()
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, bool>> exp = Expression.Lambda<Func<ClassMapping, bool>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_Lazy))), new ParameterExpression[1]
      {
        parameterExpression
      });
      int num = this.nextBool ? 1 : 0;
      attributeStore.Set<bool>(exp, num != 0);
      this.nextBool = true;
    }

    public virtual void Join(string tableName, Action<JoinPart<T>> action)
    {
      JoinPart<T> joinPart = new JoinPart<T>(tableName);
      action(joinPart);
      this.providers.Joins.Add((IJoinMappingProvider) joinPart);
    }

    public virtual ImportPart ImportType<TImport>()
    {
      ImportPart importPart = new ImportPart(typeof (TImport));
      this.imports.Add(importPart);
      return importPart;
    }

    public void ReadOnly()
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, bool>> exp = Expression.Lambda<Func<ClassMapping, bool>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_Mutable))), new ParameterExpression[1]
      {
        parameterExpression
      });
      int num = !this.nextBool ? 1 : 0;
      attributeStore.Set<bool>(exp, num != 0);
      this.nextBool = true;
    }

    public void DynamicUpdate()
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, bool>> exp = Expression.Lambda<Func<ClassMapping, bool>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_DynamicUpdate))), new ParameterExpression[1]
      {
        parameterExpression
      });
      int num = this.nextBool ? 1 : 0;
      attributeStore.Set<bool>(exp, num != 0);
      this.nextBool = true;
    }

    public void DynamicInsert()
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, bool>> exp = Expression.Lambda<Func<ClassMapping, bool>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_DynamicInsert))), new ParameterExpression[1]
      {
        parameterExpression
      });
      int num = this.nextBool ? 1 : 0;
      attributeStore.Set<bool>(exp, num != 0);
      this.nextBool = true;
    }

    public ClassMap<T> BatchSize(int size)
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, int>> exp = Expression.Lambda<Func<ClassMapping, int>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_BatchSize))), new ParameterExpression[1]
      {
        parameterExpression
      });
      int num = size;
      attributeStore.Set<int>(exp, num);
      return this;
    }

    public void CheckConstraint(string constraint)
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, string>> exp = Expression.Lambda<Func<ClassMapping, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_Check))), new ParameterExpression[1]
      {
        parameterExpression
      });
      string str = constraint;
      attributeStore.Set<string>(exp, str);
    }

    public void Persister<TPersister>() where TPersister : IEntityPersister
    {
      this.Persister(typeof (TPersister));
    }

    private void Persister(Type type)
    {
      this.Persister(type.AssemblyQualifiedName);
    }

    public void Persister(string type)
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, string>> exp = Expression.Lambda<Func<ClassMapping, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_Persister))), new ParameterExpression[1]
      {
        parameterExpression
      });
      string str = type;
      attributeStore.Set<string>(exp, str);
    }

    public void Proxy<TProxy>()
    {
      this.Proxy(typeof (TProxy));
    }

    public void Proxy(Type type)
    {
      this.Proxy(type.AssemblyQualifiedName);
    }

    public void Proxy(string type)
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, string>> exp = Expression.Lambda<Func<ClassMapping, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_Proxy))), new ParameterExpression[1]
      {
        parameterExpression
      });
      string str = type;
      attributeStore.Set<string>(exp, str);
    }

    public void SelectBeforeUpdate()
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, bool>> exp = Expression.Lambda<Func<ClassMapping, bool>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_SelectBeforeUpdate))), new ParameterExpression[1]
      {
        parameterExpression
      });
      int num = this.nextBool ? 1 : 0;
      attributeStore.Set<bool>(exp, num != 0);
      this.nextBool = true;
    }

    public void Where(string where)
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, string>> exp = Expression.Lambda<Func<ClassMapping, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_Where))), new ParameterExpression[1]
      {
        parameterExpression
      });
      string str = where;
      attributeStore.Set<string>(exp, str);
    }

    public void Subselect(string subselectSql)
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, string>> exp = Expression.Lambda<Func<ClassMapping, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_Subselect))), new ParameterExpression[1]
      {
        parameterExpression
      });
      string str = subselectSql;
      attributeStore.Set<string>(exp, str);
    }

    public void EntityName(string entityName)
    {
      AttributeStore<ClassMapping> attributeStore = this.attributes;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
      // ISSUE: method reference
      Expression<Func<ClassMapping, string>> exp = Expression.Lambda<Func<ClassMapping, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_EntityName))), new ParameterExpression[1]
      {
        parameterExpression
      });
      string str = entityName;
      attributeStore.Set<string>(exp, str);
    }

    public ClassMap<T> ApplyFilter(string name, string condition)
    {
      this.providers.Filters.Add((IFilterMappingProvider) new FilterPart(name, condition));
      return this;
    }

    public ClassMap<T> ApplyFilter(string name)
    {
      return this.ApplyFilter(name, (string) null);
    }

    public ClassMap<T> ApplyFilter<TFilter>(string condition) where TFilter : new(), FilterDefinition
    {
      return this.ApplyFilter(Activator.CreateInstance<TFilter>().Name, condition);
    }

    public ClassMap<T> ApplyFilter<TFilter>() where TFilter : new(), FilterDefinition
    {
      return this.ApplyFilter<TFilter>((string) null);
    }

    public TuplizerPart Tuplizer(TuplizerMode mode, Type tuplizerType)
    {
      this.providers.TuplizerMapping = new TuplizerMapping();
      this.providers.TuplizerMapping.Mode = mode;
      this.providers.TuplizerMapping.Type = new TypeReference(tuplizerType);
      return new TuplizerPart(this.providers.TuplizerMapping).Type(tuplizerType).Mode(mode);
    }

    ClassMapping IMappingProvider.GetClassMapping()
    {
      ClassMapping classMapping1 = new ClassMapping(this.attributes.CloneInner());
      classMapping1.Type = typeof (T);
      classMapping1.Name = typeof (T).AssemblyQualifiedName;
      foreach (IPropertyMappingProvider propertyMappingProvider in (IEnumerable<IPropertyMappingProvider>) this.providers.Properties)
        classMapping1.AddProperty(propertyMappingProvider.GetPropertyMapping());
      foreach (IComponentMappingProvider componentMappingProvider in (IEnumerable<IComponentMappingProvider>) this.providers.Components)
        classMapping1.AddComponent(componentMappingProvider.GetComponentMapping());
      if (this.providers.Version != null)
        classMapping1.Version = this.providers.Version.GetVersionMapping();
      foreach (IOneToOneMappingProvider oneMappingProvider in (IEnumerable<IOneToOneMappingProvider>) this.providers.OneToOnes)
        classMapping1.AddOneToOne(oneMappingProvider.GetOneToOneMapping());
      foreach (ICollectionMappingProvider collectionMappingProvider in (IEnumerable<ICollectionMappingProvider>) this.providers.Collections)
        classMapping1.AddCollection(collectionMappingProvider.GetCollectionMapping());
      foreach (IManyToOneMappingProvider oneMappingProvider in (IEnumerable<IManyToOneMappingProvider>) this.providers.References)
        classMapping1.AddReference(oneMappingProvider.GetManyToOneMapping());
      foreach (IAnyMappingProvider anyMappingProvider in (IEnumerable<IAnyMappingProvider>) this.providers.Anys)
        classMapping1.AddAny(anyMappingProvider.GetAnyMapping());
      foreach (ISubclassMappingProvider subclassMappingProvider in this.providers.Subclasses.Values)
        classMapping1.AddSubclass(subclassMappingProvider.GetSubclassMapping());
      foreach (IJoinMappingProvider joinMappingProvider in (IEnumerable<IJoinMappingProvider>) this.providers.Joins)
        classMapping1.AddJoin(joinMappingProvider.GetJoinMapping());
      if (this.providers.Discriminator != null)
        classMapping1.Discriminator = this.providers.Discriminator.GetDiscriminatorMapping();
      if (this.Cache.IsDirty)
        classMapping1.Cache = this.Cache.GetCacheMapping();
      if (this.providers.Id != null)
        classMapping1.Id = (IIdentityMapping) this.providers.Id.GetIdentityMapping();
      if (this.providers.CompositeId != null)
        classMapping1.Id = (IIdentityMapping) this.providers.CompositeId.GetCompositeIdMapping();
      if (this.providers.NaturalId != null)
        classMapping1.NaturalId = this.providers.NaturalId.GetNaturalIdMapping();
      if (!classMapping1.IsSpecified("TableName"))
      {
        ClassMapping classMapping2 = classMapping1;
        ParameterExpression parameterExpression = Expression.Parameter(typeof (ClassMapping), "x");
        // ISSUE: method reference
        Expression<Func<ClassMapping, string>> property = Expression.Lambda<Func<ClassMapping, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ClassMapping.get_TableName))), new ParameterExpression[1]
        {
          parameterExpression
        });
        string defaultTableName = this.GetDefaultTableName();
        classMapping2.SetDefaultValue<string>(property, defaultTableName);
      }
      foreach (IFilterMappingProvider filterMappingProvider in (IEnumerable<IFilterMappingProvider>) this.providers.Filters)
        classMapping1.AddFilter(filterMappingProvider.GetFilterMapping());
      foreach (IStoredProcedureMappingProvider procedureMappingProvider in (IEnumerable<IStoredProcedureMappingProvider>) this.providers.StoredProcedures)
        classMapping1.AddStoredProcedure(procedureMappingProvider.GetStoredProcedureMapping());
      classMapping1.Tuplizer = this.providers.TuplizerMapping;
      return classMapping1;
    }

    HibernateMapping IMappingProvider.GetHibernateMapping()
    {
      HibernateMapping hibernateMapping = this.hibernateMappingPart.GetHibernateMapping();
      foreach (ImportPart importPart in (IEnumerable<ImportPart>) this.imports)
        hibernateMapping.AddImport(importPart.GetImportMapping());
      return hibernateMapping;
    }

    private string GetDefaultTableName()
    {
      string str = this.EntityType.Name;
      if (this.EntityType.IsGenericType)
      {
        str = this.EntityType.Name.Substring(0, this.EntityType.Name.IndexOf('`'));
        foreach (Type type in this.EntityType.GetGenericArguments())
          str = str + "_" + type.Name;
      }
      return "`" + str + "`";
    }

    IEnumerable<Member> IMappingProvider.GetIgnoredProperties()
    {
      return (IEnumerable<Member>) new Member[0];
    }
  }
}
