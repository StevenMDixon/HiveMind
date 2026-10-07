using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class BatchService(SqliteDBContext context) : BaseService<ScheduleBatch>(context)
{
    public IEnumerable<ScheduleBatch> GetAllBatches()
    {
        return _context.ScheduleBatches;
    }

    public IEnumerable<ScheduleBatch> GetUnprocessedBatches()
    {
        return Get().Where(b => b.Status == BatchStatus.New).Include(x => x.ScheduleBatchItems).Include(y => y.ProgramStrategy);
    }

    public IEnumerable<ScheduleBatch> GetBatchesBeforeDate(DateOnly date)
    {
        return Get().Where(b => b.StartDate <= date).Include(x => x.ScheduleBatchItems).Include(y => y.ScheduleBatchItems);
    }

    public void AddBatch(ScheduleBatch batch)
    {
        Create(batch);
    }

    public void CompleteBatch(ScheduleBatch batch)
    {
        batch.Status = BatchStatus.Completed;
        Update(batch);
    }

    public void CancelBatch(ScheduleBatch batch)
    {
        batch.Status = BatchStatus.Canceled;
        Update(batch);
    }

    public void UpdateBatch(ScheduleBatch batch)
    {
        Update(batch);
    }

    public void CreateBatchItem(ScheduleBatch batch, ScheduleBatchItem item)
    {
        item.ScheduleBatchId = batch.Id;
        _context.ScheduleBatchItems.Add(item);
        _context.SaveChanges();
    }

    public void UpdateBatchItem(ScheduleBatchItem item)
    {
        _context.ScheduleBatchItems.Update(item);
        _context.SaveChanges();
    }

    public void DeleteBatchItems(List<ScheduleBatch> items)
    {
        _context.ScheduleBatches.RemoveRange(items);
        _context.SaveChanges();
    }
}
