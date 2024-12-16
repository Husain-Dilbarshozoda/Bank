using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Responses;

namespace Infratructure.Services;

public class AccountService(IContext context):IGenericService<Account>
{
    public async Task<ApiResponse<List<Account>>> GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from accounts;";
        var res = (await connection.QueryAsync<Account>(sql)).ToList();
        return new ApiResponse<List<Account>>(res);
    }

    public async Task<ApiResponse<Account>> GetById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * from accounts where accountid=@ID;";
        var res = await connection.QueryFirstOrDefaultAsync<Account>(sql,new{ID=id});
        return new ApiResponse<Account>(res);
    }

    public async Task<ApiResponse<bool>> Add(Account data)
    {
        using var connection = context.Connection();
        var sql = "insert into accounts(balance,accountstatus,accounttype,currency,createdat,deletedat) values(@balance,@accountstatus,@accounttype,@currency,@createdat,@deletedat);";
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0)
        {
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        }

        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Update(Account data)
    {
        using var connection = context.Connection();
        var sql = "update accounts set balance=@balance,accountstatus=@accountstatus,accounttype=@accounttype,currency=@currency,createdat=@createdat,deletedat=@deletedat where accountid=@accountid;";
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0)
        {
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        }

        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection();
        var sql = "delete from accounts where accountid=@ID;";
        var res = await connection.ExecuteAsync(sql, new { ID = id });
        if (res==0)
        {
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        }

        return new ApiResponse<bool>(res != 0);
    }
}