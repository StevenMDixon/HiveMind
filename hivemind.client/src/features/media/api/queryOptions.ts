import { queryOptions } from '@tanstack/react-query';
import { getMedia } from './requests';

export const mediaQueryOptions = () => {
    return queryOptions({
        queryKey: ['media'],
        queryFn: getMedia
    });
}