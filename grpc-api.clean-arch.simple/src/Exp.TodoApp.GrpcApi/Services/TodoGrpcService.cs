namespace Exp.TodoApp.GrpcApi.Services;

public class TodoGrpcService(ITodoService todoService, IMapper mapper, ILogger<TodoGrpcService> logger) : TodoService.TodoServiceBase
{

    public override async Task<TodoResponse> GetById(GetTodoByIdRequest request, ServerCallContext context)
    {
        TodoResponse responseBody = new();
        StatusData statusData = new() { Success = true, Code=200, Message="Record found." };
        logger.LogInformation("Attempting to Get TodoById: {Id}", request.Id);

        try
        {
            var todo = await todoService.GetByIdAsync(request.Id, context.CancellationToken);
           
            if (todo == null)
            {
                statusData.Code = 404;
                statusData.Message = "No record found";
                responseBody.Status = statusData;
                return responseBody;
            }
            else
            {
                TodoData todoData = mapper.Map<TodoData>(todo);              
                responseBody.Data = todoData;
                responseBody.Status = statusData;
                return responseBody;
            }
        }
     
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }


    public override async Task<TodoListResponse> GetAll(Empty request, ServerCallContext context)
    {
        TodoListResponse responseBody = new();
        StatusData statusData = new() { Success = true, Code = 200, Message = "Record(s) found." };
        logger.LogInformation("Attempting to Get All Todos");

        try
        {
            var todos = await todoService.GetAllAsync(context.CancellationToken);

            if (todos == null || todos.Count == 0)
            { 
                statusData.Message = "No records found.";
                responseBody.Status = statusData;
                return responseBody;
            }
            else
            {
                responseBody.Data.AddRange(mapper.Map<IEnumerable<TodoData>>(todos));
                responseBody.Status = statusData;
                return responseBody;
            }
        }

        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }

    public override async Task<CreateTodoResponse> Create(CreateTodoRequest request, ServerCallContext context)
    {
        CreateTodoResponse responseBody = new();
        StatusData statusData = new() { Success = true, Code = 201, Message = "Record created successfully." };
        logger.LogInformation("Attempting to Create a Todo");

        try
        {
            var todo = mapper.Map<CreateTodoDto>(request);
            var created = await todoService.CreateAsync(todo, context.CancellationToken);

            if (created == 0)
            {
                statusData.Code = 422;
                statusData.Message = "Unable to create.";
                responseBody.Status = statusData;
                return responseBody;
            }
            else
            {
                responseBody.Status = statusData;
                return responseBody;
            }
        }

        catch (AppValidationException ex)
        {
            responseBody.Status = ServiceHelper.CreateFailureMeta(400, ex.Errors);
            return responseBody;
        }

        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }

    public override async Task<UpdateTodoResponse> Update(UpdateTodoRequest request, ServerCallContext context)
    {
        UpdateTodoResponse responseBody = new();
        StatusData statusData = new() { Success = true, Code = 200, Message = "Record updated successfully." };
        logger.LogInformation("Attempting to update a Todo");

        try
        {
            var todo = mapper.Map<UpdateTodoDto>(request);
            var updated = await todoService.UpdateAsync(todo, context.CancellationToken);

            if (!updated)
            {
                statusData.Code = 422;
                statusData.Success = false;
                statusData.Message = "Unable to update.";
                responseBody.Status = statusData;
                return responseBody;
            }
            else
            {  
                responseBody.Status = statusData;
                return responseBody;
            }
        }

        catch (AppValidationException ex)
        {
            responseBody.Status = ServiceHelper.CreateFailureMeta(400, ex.Errors);
            return responseBody;
        }

        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }

    public override async Task<DeleteTodoResponse> Delete(DeleteTodoRequest request, ServerCallContext context)
    {
        DeleteTodoResponse responseBody = new();
        StatusData statusData = new() { Success = true, Code = 200, Message = "Record deleted successfully." };
        logger.LogInformation("Attempting to delete a Todo");

        try
        {
            var success = await todoService.DeleteAsync(request.Id, context.CancellationToken);
            statusData.Success = success;

            if (!success)
            {
                statusData.Code = 422;
                statusData.Success = false;
                statusData.Message = "Unable to delete.";
                responseBody.Status = statusData;
                return responseBody;
            }
            else
            {  
                responseBody.Status = statusData;
                return responseBody;
            }

        }

        catch (AppValidationException ex)
        {
            responseBody.Status = ServiceHelper.CreateFailureMeta(400, ex.Errors);
            return responseBody;
        }

        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }
    
}

