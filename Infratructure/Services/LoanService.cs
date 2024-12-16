using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Responses;

namespace Infratructure.Services;

public class LoanService(IContext context):IGenericService<Loan>
{
    public async Task<ApiResponse<List<Loan>>> GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from loans;";
        var res = (await connection.QueryAsync<Loan>(sql)).ToList();
        return new ApiResponse<List<Loan>>(res);
    }

    public async Task<ApiResponse<Loan>> GetById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * form loans where loanid=@ID;";
        var res = await connection.QueryFirstOrDefaultAsync<Loan>(sql,new{ID=id});
        return new ApiResponse<Loan>(res);
    }

    public async Task<ApiResponse<bool>> Add(Loan data)
    {
        using var connection = context.Connection();
        var sql = "insert into loans(loanamount,dateissued,createdat,deletedat,customerid,branchid) values(@loanamount,@dateissued,@createdat,@deletedat,@customerid,@branchid);";
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0)
        {
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        }

        return new ApiResponse<bool>(res != 0);
    }

    public async Task<ApiResponse<bool>> Update(Loan data)
    {
        using var connection = context.Connection();
        var sql = "update loans set loanamount=@loanamount,dateissued=@dateissued,createdat=@createdat,deletedat=@deletedat,customerid=@customerid,branchid=@branchid loanid=@loanid;";
        var res = await connection.ExecuteAsync(sql,data);
        if (res == 0)
        {
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        }

        return new ApiResponse<bool>(res != 0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection();
        var sql = "delete from loans where loanid=@ID";
        var res = await connection.ExecuteAsync(sql, new { ID = id });
        if (res == 0)
        {
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        }

        return new ApiResponse<bool>(res != 0);
    }
}