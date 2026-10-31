import { queryOptions } from '@tanstack/react-query';
import { getShows, getShow, search, getRatingOptions } from './requests';
import { showKeys } from './queryKeys';

export const showsQueryOptions = () => {
    return queryOptions({
        queryKey: ['shows'],
        queryFn: getShows
    });
}

export const showQueryOptions = (id: string) => {
    return queryOptions({
        queryKey: showKeys.detail(Number(id)),
        queryFn: () => getShow(id)
    });
}

export const ratingOptionsQueryOptions = () => {
    return queryOptions({
        queryKey: ['ratingOptions'],
        queryFn: getRatingOptions
    });
}

export const showQuerySearchOptions = (searchTerm: string, page: number, count: number) => {
    return queryOptions({
        queryKey: ['shows', searchTerm, page, count],
        queryFn: () => search(searchTerm, page, count)
    });
}