using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using requestInspect.Model;

namespace requestInspect.Services;

public class RequestLoggerService : IRequestLoggerService
{
    public List<RequestEntity> requestEntities = new();



    public RequestLoggerService()
    {
        // Mock data
        //MockData();
        
    }

    private void MockData()
    {
        requestEntities.Add(new RequestEntity()
        {
            RequestId = Guid.NewGuid().ToString(),
            Body = "A test body 1..."
        });
        requestEntities.Add(new RequestEntity()
        {
            RequestId = Guid.NewGuid().ToString(),
            Body = "A test body 2..."
        });
    }

    public async Task<RequestEntity> AddRequestEntity(RequestEntity requestEntity)
    {
        requestEntities.Add(requestEntity);
        return requestEntity;
    }

    public async Task<IEnumerable<RequestEntity>> GetAllRequestEntities()
    {
        return requestEntities.ToArray();
    }

    public async Task<RequestEntity?> GetRequestEntityByID(string ID)
    {
        var req = requestEntities.FirstOrDefault(x => x.RequestId == ID);
        return req;
    }

    public async Task<RequestEntity> RemoveRequestEntity(string ID)
    {
        var ent = requestEntities.FirstOrDefault(x => x.RequestId==ID);
        if(ent == null)
        {
            return null;
        }
        
        requestEntities.Remove(ent);
        return ent;
    }
}

