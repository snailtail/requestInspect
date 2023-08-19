using Microsoft.AspNetCore.Mvc;
using requestInspect.Model;

public interface IRequestLoggerService
{
       Task<RequestEntity?> GetRequestEntityByID(string ID);
       Task<IEnumerable<RequestEntity>> GetAllRequestEntities();

       Task<RequestEntity> AddRequestEntity(RequestEntity requestEntity);

       Task<RequestEntity> RemoveRequestEntity(string ID);


}