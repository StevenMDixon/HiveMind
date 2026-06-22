import { queryOptions } from '@tanstack/react-query';
import { getProgramStrategy, getProgramStrategies } from './requests';
import { strategyKeys } from './queryKeys';

export const programStrategyQueryOptions = () => {
    return queryOptions({
        queryKey: strategyKeys.all,
        queryFn: getProgramStrategies
    });
}

export const programStrategyDetailQueryOptions = (id: number) => {
    return queryOptions({
        queryKey: strategyKeys.detail(id),
        queryFn: () => getProgramStrategy(id)
    });
}

