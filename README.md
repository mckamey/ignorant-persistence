# .NET Unit of Work Library for Persistence Ignorance

Uses C#/LINQ to create a basic framework for building a persistence ignorant Repository. Respositories can then be unit tested via dependency injection of an in-memory unit-of-work.

Defines the patterns for a persistence ignorant unit-of-work (IUnitOfWork) and corresponding table (`ITable<T>`). Since `ITable<T>` implements `IQueryable<T>` it may be used directly in LINQ queries.

Implements a Linq-to-Sql implementation and a unit-testable in-memory implementation. This mechanism allows for the query logic to be directly tested independently of a persistence implementation.


---


To use, a repository should be created for the application. Using Dependency Injection, a repository can accept an `IUnitOfWork` and remain completely ignorant of the underlying persistence storage mechanism:

```

public class FooRepository
{
	private readonly IUnitOfWork UnitOfWork;
	private ITable<Foo> foos;

	public CatalogRepository(IUnitOfWork unitOfWork)
	{
		if (unitOfWork == null)
		{
			throw new ArgumentNullException("unitOfWork");
		}

		this.UnitOfWork = unitOfWork;
	}

	public ITable<Foo> Foos
	{
		get
		{
			if (this.foos == null)
			{
				this.foos = this.UnitOfWork.GetTable<Foo>();
			}
			return this.foos;
		}
	}
}

```

The repository gets a reference to the unit of work via dependency injection (DI) which is populated via any inverson of control (IoC) container you like.

And since `ITable<T>` implements `IQueryable<T>`, the usage of the repository is powerful being able to fully leverage LINQ while being ignorant of the underlying storage.

```

FooRepository repos = ServiceLocator.Current.GetInstance<FooRepository>();

var query =
	from foo in repos.Foos
	where foo.Bar > 42
	orderby foo.Name ascending
	select new
	{
		ID = foo.ID,
		Name = foo.Name
	};

if (!query.Any())
{
	repos.Foos.Add(
		new Foo {
			Name = "ABC",
			Bar = 123
		});

	repos.Save();
}

```

The entity classes themselves can simply be POCO or generated from SQL via DBML. When specifying the DataContext for populating a LINQ-to-SQL IUnitOfWork, you can specify an XML mapping.
