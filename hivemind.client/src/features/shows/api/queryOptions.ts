import { queryOptions } from '@tanstack/react-query';
import { getShows } from './requests';

export const showsQueryOptions = () => {
    return queryOptions({
        queryKey: ['shows'],
        queryFn: getShows
    });
}