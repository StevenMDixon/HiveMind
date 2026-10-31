import { queryOptions } from '@tanstack/react-query';
import { getMedia, search } from './requests';

export const mediaQueryOptions = () => {
    return queryOptions({
        queryKey: ['media'],
        queryFn: getMedia
    });
}


export const mediaQuerySearchOptions = (searchTerm: string, page: number, count: number) => {
    return queryOptions({
        queryKey: ['media', searchTerm, page, count],
        queryFn: () => search(searchTerm, page, count)
    });
}



