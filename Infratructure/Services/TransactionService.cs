using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Responses;
using Transaction = System.Transactions.Transaction;

namespace Infratructure.Services;

public class TransactionService(IContext context):IGenericService<Transaction>
{
    public async Task<ApiResponse<List<Transaction>>> GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from transactions;";
        var res = (await connection.QueryAsync<Transaction>(sql)).ToList();
        return new ApiResponse<List<Transaction>>(res);
    }

    public async Task<ApiResponse<Transaction>> GetById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * from transactions where transactionid=@ID;";
        var res = await connection.QueryFirstOrDefaultAsync<Transaction>(sql, new { ID = id });
        return new ApiResponse<Transaction>(res);
    }

    public async Task<ApiResponse<bool>> Add(Transaction data)
    {
        using var connection = context.Connection();
        var sql = "insert into transactions(transactionstatus,dateissued,amount,createdat,deletedat,fromaccountid,toaccountid) values(@transactionstatus,@dateissued,@amount,@createdat,@deletedat,@fromaccountid,@toaccountid);";
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0)
        {
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        }

        return new ApiResponse<bool>(res != 0);
    }

    public async Task<ApiResponse<bool>> Update(Transaction data)
    {
        using var connection = context.Connection();
        var sql = "update transactions set transactionstatus=@transactionstatus,dateissued=@dateissued,amount=@amount,createdat=@createdat,deletedat=@deletedat,fromaccountid=@fromaccountid,toaccountid=@toaccountid where transactionid=@transactionid;";
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0)
        {
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError,"Internal server error");
        }

        return new ApiResponse<bool>(res != 0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection();
        var sql = "delete from transactions where transactionid=@ID";
        var res = await connection.ExecuteAsync(sql, new { ID = id });
        if (res == 0)
        {
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        }

        return new ApiResponse<bool>(res != 0);
    }
}