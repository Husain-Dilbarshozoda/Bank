using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Responses;

namespace Infratructure.Services;

public class BranchService(IContext context):IGenericService<Branch>
{
    public async Task<ApiResponse<List<Branch>>>GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from branches;";
        var res = (await connection.QueryAsync<Branch>(sql)).ToList();
        return new ApiResponse<List<Branch>>(res);
    }

    public async Task<ApiResponse<Branch>> GetById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * from branches where branchid=@ID";
        var res = await connection.QueryFirstOrDefaultAsync<Branch>(sql,new{ID=id});
        return new ApiResponse<Branch>(res);
    }

    public async Task<ApiResponse<bool>> Add(Branch data)
    {
        using var connection = context.Connection();
        var sql = "insert into branches(branchname,branchlocation,createdat,deletedat) values(@branchname,@branchlocation,@createdat,@deletedat)";
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0)
        {
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        }

        return new ApiResponse<bool>(res != 0);
    }

    public async Task<ApiResponse<bool>> Update(Branch data)
    {
        using var connection = context.Connection();
        var sql =
            "update branches set branchname=@branchname,branchlocation=@branchlocation,createdat=@createdat,deletedat=@deletedat where branchid=@branchid;";
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0)
        {
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        }
        return new ApiResponse<bool>(res!=0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection();
        var sql = "delete from  branches where branchid=@ID;";
        var res = await connection.ExecuteAsync(sql, new { ID = id });
        if (res == 0)
        {
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        }

        return new ApiResponse<bool>(res != 0);
    }
}