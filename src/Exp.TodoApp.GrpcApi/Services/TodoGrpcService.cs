using Exp.TodoApp.Domain.Entities;
using static Google.Rpc.Context.AttributeContext.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Exp.TodoApp.GrpcApi.Services;

public class TodoGrpcService(ISender mediator, IMapper mapper, ILogger<TodoGrpcService> logger) : TodoService.TodoServiceBase
{
   
    public override async Task<TodoResponse> GetById(GetTodoByIdRequest request, ServerCallContext context)
    {
        TodoResponse responseBody = new();
        StatusData statusData = new();
        logger.LogInformation("Attempting to Get TodoById: {Id}", request.Id);

        try
        {

            var todo = await mediator.Send(new GetByIdQuery(request.Id));
            if (todo == null)
            {
                statusData.Code = 404;
                statusData.Description = "Todo not found";
                responseBody.Status = statusData;
                return responseBody;
            }
            else
            {
                TodoData todoData = mapper.Map<TodoData>(todo);
                statusData.Code = 200;
                statusData.Description = "Todo record found";
                responseBody.Data = todoData;
                responseBody.Status = statusData;
                return responseBody;
            }
        }
             

        catch (Exception ex)
        {
            //var metadata = new Metadata { { "x-request-id", requestId }, { "x-correlation-id", correlationId } };
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }


    public override async Task<TodoListResponse> GetAll(Empty request, ServerCallContext context)
    {
        TodoListResponse responseBody = new();
        StatusData statusData = new();
        logger.LogInformation("Attempting to Get All Todos");

        try
        {

            var todos = await mediator.Send(new GetAllQuery());

            if (todos == null)
            {
                statusData.Code = 200;
                statusData.Description = "No records found.";
                responseBody.Status = statusData;
                return responseBody;
            }
            else
            {
                statusData.Code = 200;
                statusData.Description = "Todo record(s) found";
                responseBody.Data.AddRange(mapper.Map<IEnumerable<TodoData>>(todos));
                responseBody.Status = statusData;
                return responseBody;
            }
        }


        catch (Exception ex)
        {
            //var metadata = new Metadata { { "x-request-id", requestId }, { "x-correlation-id", correlationId } };
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }

    public override async Task<CreateTodoResponse> Create(CreateTodoRequest request, ServerCallContext context)
    {
        CreateTodoResponse responseBody = new();
        StatusData statusData = new();
        logger.LogInformation("Attempting to Create a Todo");

        try
        {

            var todo = mapper.Map<CreateTodoDto>(request);           
            var created = await mediator.Send(new CreateTodoCommand(todo));

            if (created ==0 )
            {
                statusData.Code = 422;
                statusData.Description = "Unable to create.";
                responseBody.Status = statusData;
                return responseBody;
            }
            else
            {
                statusData.Code = 200;
                statusData.Description = "Todo record created successfully.";               
                responseBody.Status = statusData;
                return responseBody;
            }
        }


        catch (Exception ex)
        {
            //var metadata = new Metadata { { "x-request-id", requestId }, { "x-correlation-id", correlationId } };
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }

    public override async Task<UpdateTodoResponse> Update(UpdateTodoRequest request, ServerCallContext context)
    {
        UpdateTodoResponse responseBody = new();
        StatusData statusData = new();
        logger.LogInformation("Attempting to update a Todo");

        try
        {

            var todo = mapper.Map<UpdateTodoDto>(request);
            var updated = await mediator.Send(new UpdateTodoCommand(todo));

            if (!updated)
            {
                statusData.Code = 422;
                statusData.Description = "Unable to update.";
                responseBody.Status = statusData;
                return responseBody;
            }
            else
            {
                statusData.Code = 200;
                statusData.Description = "Todo record updated successfully.";
                responseBody.Status = statusData;
                return responseBody;
            }
        }


        catch (Exception ex)
        {
            //var metadata = new Metadata { { "x-request-id", requestId }, { "x-correlation-id", correlationId } };
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }

    public override async Task<DeleteTodoResponse> Delete(DeleteTodoRequest request, ServerCallContext context)
    {
        DeleteTodoResponse responseBody = new();
        StatusData statusData = new();
        logger.LogInformation("Attempting to delete a Todo");

        try
        {

            var success = await mediator.Send(new DeleteTodoCommand(request.Id));

            if (!success)
            {
                statusData.Code = 422;
                statusData.Description = "Unable to delete.";
                responseBody.Status = statusData;
                return responseBody;
            }
            else
            {
                statusData.Code = 200;
                statusData.Description = "Todo record deleted successfully.";
                responseBody.Status = statusData;
                return responseBody;
            }
                       
        }


        catch (Exception ex)
        {
            //var metadata = new Metadata { { "x-request-id", requestId }, { "x-correlation-id", correlationId } };
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }
}

