using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Example.EventDriven.Application.GetRequestStatus.Boundaries
{
    public class GetRequestStatusResponse<T>
    {
        public RequestEntity<T> Data { get; set; }
        public int StatusCode { get; set; }

        public GetRequestStatusResponse(RequestEntity<T> data)
        {
            Data = data;

            if(data == null)
            {
                StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            if(data.Status == RequestStatus.InvalidInformation)
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity;
                return;
            }

            if(data.Status == RequestStatus.NotStarted)
            {
                StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            StatusCode = StatusCodes.Status200OK;
        }
    }
}
