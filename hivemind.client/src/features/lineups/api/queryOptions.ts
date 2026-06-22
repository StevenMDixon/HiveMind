import { queryOptions } from '@tanstack/react-query';
import { getLineups, getLineup, getLineupItem, getQueryTypes, getPadToOptions, getPlayoutTypes } from './requests';
import { queryKeys } from './queryKeys';

export const lineupsQueryOptions = () => {
    return queryOptions({
        queryKey: queryKeys.all,
        queryFn: getLineups
    });
}

export const lineupQueryOptions = (id: number) => {
    return queryOptions({
        queryKey: queryKeys.detail(id),
        queryFn: () => getLineup(id)
    });
}

export const lineupItemQueryOptions = (id: number) => {
    return queryOptions({
        queryKey: queryKeys.lineItem(id),
        queryFn: () => getLineupItem(id)
    });
}

export const queryTypesQueryOptions = () => {
    return queryOptions({
        queryKey: ['queryTypes'],
        queryFn: getQueryTypes
    });
}

export const padtoQueryOptions = () => {
    return queryOptions({
        queryKey: ['padtoOptions'],
        queryFn: getPadToOptions
    });
}

export const playoutTypesQueryOptions = () => {
    return queryOptions({
        queryKey: ['playoutTypes'],
        queryFn: getPlayoutTypes
    });
}

