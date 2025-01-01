using System;
using System.Data;
using Calendar.IService;
using Calendar.Models;
using Dapper;

namespace Calendar.Service;

public class DayEventService : IDayEventService
{
    DayEvent _oDayEvent = new DayEvent();
    List<DayEvent> _oDayEvents = new List<DayEvent>();

    private readonly IConfiguration _configuration;

    public DayEventService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DayEvent GetEvent(DateTime eventDate)
    {
        throw new NotImplementedException();
    }

    public List<DayEvent> GetEvents(DateTime fromDate, DateTime toDate)
    {
        throw new NotImplementedException();
    }

    public DayEvent SaveOrUpdate(DayEvent oDayEvent)
    {
        _oDayEvent = new DayEvent();
        try 
        {
            int operationType = Convert.ToInt32(oDayEvent.DayEventId == 0? OperationType.Insert : OperationType.Update);
            using (IDbConnection con = new SqlConnection(IConfiguration.GetConnenctionString("DefaultConnection")))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                
                var oDayEvents = con.Query<DayEvent>("SP_DayEvent",
                    this.SetParameters(oDayEvent, operationType),
                    commandType: CommandType.StoredProcedure);
            }
        }
        catch (Exception ex)
        {
            _oDayEvent.Message = ex.Message;
        }
        return _oDayEvent;
    }

    private DynamicParameters SetParameters(DayEvent oDayEvent, int operationType)
    {
        DynamicParameters parameters = new DynamicParameters();

        parameters.Add("@DayEventId", oDayEvent.DayEventId);
        parameters.Add("@Note", oDayEvent.Note);
        parameters.Add("@EventDate", oDayEvent.EventDate);
        parameters.Add("@OperationType", operationType);

        return parameters;
    }
}
