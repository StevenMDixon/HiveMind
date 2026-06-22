import { queryOptions } from '@tanstack/react-query';
import { getQueries, getTestQueryResults, getQuery, getOptions, getSettings } from './requests';
import { queryKeys } from './queryKeys';

export const queriesQueryOptions = () => {
    return queryOptions({
        queryKey: queryKeys.all,
        queryFn: getQueries
    });
}

export const queryQueryOptions = (id: number) => {
    return queryOptions({
        queryKey: queryKeys.detail(id),
        queryFn: () => getQuery(id)
    });
}

export const testQueryOptions = (id: number) => {
    return queryOptions({
        queryKey: queryKeys.tests(),
        queryFn: () => getTestQueryResults(id)
    });
}

export const settingsQueryOptions = () => {
    return queryOptions({
        queryKey: ['settings'],
        queryFn: getSettings
    });
}

export const optionsQueryOptions = () => {
    return queryOptions({
        queryKey: ['options'],
        queryFn: getOptions
    });
}

