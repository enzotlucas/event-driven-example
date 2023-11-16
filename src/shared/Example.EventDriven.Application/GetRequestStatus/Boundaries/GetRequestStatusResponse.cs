using Example.EventDriven.Domain.Entitites;
using Microsoft.AspNetCore.Http;

namespace Example.EventDriven.Application.GetRequestStatus.Boundaries
{
    public class GetRequestStatusResponse<T>
    {
        public RequestProcessEntity<T> Data { get; set; }
        public int StatusCode { get; set; }

        public GetRequestStatusResponse(RequestProcessEntity<T> data)
        {
            Data = data;

            if(data == null)
            {
                StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            if(!data.Success)
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity;
                return;
            }

            if(!data.IsCompleted)
            {
                StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            StatusCode = StatusCodes.Status200OK;
        }
    }
}
