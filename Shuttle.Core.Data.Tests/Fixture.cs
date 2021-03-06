﻿using System.Data;
using Moq;
using NUnit.Framework;

namespace Shuttle.Core.Data.Tests
{
    [TestFixture]
    public abstract class Fixture
    {
	    protected static string DefaultConnectionStringName = "Shuttle";
		protected static string DefaultProviderName = "System.Data.SqlClient";
		protected static string DefaultConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=Shuttle;Integrated Security=SSPI";

	    protected IDatabaseContext GetDatabaseContext()
		{
			return new DatabaseContextFactory(new DbConnectionFactory(), new DbCommandFactory(), new ThreadStaticDatabaseContextCache()).Create(DefaultConnectionStringName);
		}

	    protected IDatabaseContext GetDatabaseContext(Mock<IDbCommand> command)
	    {
		    var commandFactory = new Mock<IDbCommandFactory>();

		    commandFactory.Setup(m => m.CreateCommandUsing(It.IsAny<IDbConnection>(), It.IsAny<IQuery>())).Returns(command.Object);

			return new DatabaseContextFactory(new DbConnectionFactory(), commandFactory.Object, new ThreadStaticDatabaseContextCache()).Create(DefaultConnectionStringName);
	    }
    }
}
