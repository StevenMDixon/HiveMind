import { queryOptions } from '@tanstack/react-query';
import { getTransition, getTransitions, search, getSlotTypes} from '../api/requests'; 
import { transitionKeys } from './queryKeys';

export const transitionQueryOptions = (id: string) => {
    return queryOptions({
        queryKey: [...transitionKeys.all, id],
        queryFn: () => getTransition(id)
    });
}

export const transitionsQueryOptions = () => {
    return queryOptions({
        queryKey: [...transitionKeys.all],
        queryFn: getTransitions
    });
}

export const transitionQuerySearchOptions = (searchTerm: string, page: number, count: number) => {
    return queryOptions({
        queryKey: [...transitionKeys.all, searchTerm, page, count],
        queryFn: () => search(searchTerm, page, count)
    });
}

export const slotTypeQueryOptions = () => {
    return queryOptions({
        queryKey: [...transitionKeys.all],
        queryFn: getSlotTypes
    });
}