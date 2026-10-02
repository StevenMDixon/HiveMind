import { queryOptions } from '@tanstack/react-query';
import { getProgramStrategy, getProgramStrategies, getLineupSelectionTypes, getLineups } from './requests';
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

export const lineupSelectionTypesQueryOptions = () => {
    return queryOptions({
        queryKey: ['selectionTypes'],
        queryFn: getLineupSelectionTypes
    });
}

export const lineupsQueryOptions = () => {
    return queryOptions({
        queryKey: ['lineups'],
        queryFn: getLineups     
    });
}