using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Domain.Scheduler.SlotStrategy;

public static class SlotStrategyResolver
{
    public static ISlotStrategy Resolve(TransitionSlot slot)
    {
        return slot switch
        {
            TransitionSlot.Promo => new PromoStrategy(),
            TransitionSlot.Ident => new IdentStrategy(),
            TransitionSlot.OutBump => new OutBumpStrategy(),
            TransitionSlot.Commercial => new CommercialStrategy(),
            TransitionSlot.Filler => new FillerStrategy(),
            TransitionSlot.CutFiller => new CutFillerStrategy(),
            TransitionSlot.InBump    => new InBumpStrategy(),
            _ => new DefaultStrategy()
        };
    }
}
