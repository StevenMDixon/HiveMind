using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class BatchService : BaseService
{
    public BatchService(sqliteDBContext context) : base(context) { }

    public IEnumerable<ScheduleBatch> GetAllBatches()
    {
        return _context.ScheduleBatches;
    }

    public IEnumerable<ScheduleBatch> GetUnprocessedBatches()
    {
        return _context.ScheduleBatches.Where(b => b.Status == BatchStatus.New).Include(x => x.ScheduleBatchItems).Include(y => y.ProgramStrategy);
    }

    public void AddBatch(ScheduleBatch batch)
    {
        _context.ScheduleBatches.Add(batch);
        _context.SaveChanges();
    }

    public void CompleteBatch(ScheduleBatch batch)
    {
        batch.Status = BatchStatus.Completed;
        _context.ScheduleBatches.Update(batch);
        _context.SaveChanges();
    }

    public void CancelBatch(ScheduleBatch batch)
    {
        batch.Status = BatchStatus.Canceled;
        _context.ScheduleBatches.Update(batch);
        _context.SaveChanges();
    }

    public void UpdateBatch(ScheduleBatch batch)
    {
        _context.ScheduleBatches.Update(batch);
        _context.SaveChanges();
    }

    public void CreateBatchItem(ScheduleBatch batch, ScheduleBatchItem item)
    {
        item.ScheduleBatchId = batch.ScheduleBatchId;
        _context.ScheduleBatchItems.Add(item);
        _context.SaveChanges();
    }

    public void UpdateBatchItem(ScheduleBatchItem item)
    {
        _context.ScheduleBatchItems.Update(item);
        _context.SaveChanges();
    }
}
