﻿using System.Data;
using Amai.Core.Abstraction;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Amai.Core.DataBase;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private const string Database = "Database";
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection Create() =>
        new NpgsqlConnection(_configuration.GetConnectionString(Database));
}