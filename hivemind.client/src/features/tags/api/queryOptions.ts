import { queryOptions } from '@tanstack/react-query';
import { getTag, getTags, search, getCategoryOptions } from './requests';
import { tagKeys } from './queryKeys';

export const tagsQueryOptions = () => {
    return queryOptions({
        queryKey: [...tagKeys.all],
        queryFn: getTags
    });
}

export const tagQueryOptions = (id: string) => {
    return queryOptions({
        queryKey: tagKeys.detail(Number(id)),
        queryFn: () => getTag(id)
    });
}

export const categoryQueryOptions = () => {
    return queryOptions({
        queryKey: ['category'],
        queryFn: getCategoryOptions
    });
}

export const tagQuerySearchOptions = (searchTerm: string, page: number, count: number) => {
    return queryOptions({
        queryKey: [...tagKeys.all, searchTerm, page, count],
        queryFn: () => search(searchTerm, page, count)
    });
}

